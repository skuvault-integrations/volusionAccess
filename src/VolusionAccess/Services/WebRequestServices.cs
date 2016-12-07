using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using VolusionAccess.Misc;
using VolusionAccess.Models.Configuration;

namespace VolusionAccess.Services
{
	internal class WebRequestServices
	{
		private readonly int RequestTimeoutMs = ( int )TimeSpan.FromMinutes( 30 ).TotalMilliseconds;
		private readonly VolusionConfig _config;

		public WebRequestServices( VolusionConfig config )
		{
			this._config = config;
		}

		public T GetResponse< T >( string commandParams, string marker )
		{
			var url = commandParams.GetFullEndpointWithAuth( this._config );
			var result = this.GetResponseForSpecificUrl< T >( url, marker );
			return result;
		}

		public async Task< T > GetResponseAsync< T >( string commandParams, string marker )
		{
			var url = commandParams.GetFullEndpointWithAuth( this._config );
			var result = await this.GetResponseForSpecificUrlAsync< T >( url, marker );
			return result;
		}

		public T GetResponseForSpecificUrl< T >( string url, string marker )
		{
			try
			{
				this.LogGetRequest( url, marker );

				T result;
				var request = this.CreateGetServiceGetRequest( url );
				using( var response = request.GetResponse() )
					result = this.ParseResponse< T >( response, marker );

				return result;
			}
			catch( Exception ex )
			{
				throw this.GetException( url, ex, marker );
			}
		}

		public async Task< T > GetResponseForSpecificUrlAsync< T >( string url, string marker )
		{
			try
			{
				this.LogGetRequest( url, marker );

				T result;
				var request = this.CreateGetServiceGetRequest( url );
				var timeoutToken = this.GetTimeoutToken();
				using( timeoutToken.Register( request.Abort ) )
				using( var response = await request.GetResponseAsync() )
				{
					timeoutToken.ThrowIfCancellationRequested();
					result = this.ParseResponse< T >( response, marker );
				}

				return result;
			}
			catch( Exception ex )
			{
				throw this.GetException( url, ex, marker );
			}
		}

		public void PostData( string endpoint, string xmlContent, string marker )
		{
			var request = this.CreateServicePostRequest( endpoint, xmlContent );
			try
			{
				this.LogUpdateRequest( request.Address.OriginalString, xmlContent, marker );

				using( var response = ( HttpWebResponse )request.GetResponse() )
					this.LogUpdateResponse( request.Address.OriginalString, response.StatusCode, marker );
			}
			catch( Exception ex )
			{
				throw this.UpdateException( request.Address.OriginalString, ex, marker );
			}
		}

		public async Task PostDataAsync( string endpoint, string xmlContent, string marker )
		{
			var request = this.CreateServicePostRequest( endpoint, xmlContent );
			try
			{
				this.LogUpdateRequest( request.Address.OriginalString, xmlContent, marker );

				var timeoutToken = this.GetTimeoutToken();
				using( timeoutToken.Register( request.Abort ) )
				using( var response = await request.GetResponseAsync() )
				{
					timeoutToken.ThrowIfCancellationRequested();
					this.LogUpdateResponse( request.Address.OriginalString, ( ( HttpWebResponse )response ).StatusCode, marker );
				}
			}
			catch( Exception ex )
			{
				throw this.UpdateException( request.Address.OriginalString, ex, marker );
			}
		}

		#region WebRequest configuration
		private HttpWebRequest CreateGetServiceGetRequest( string url )
		{
			this.AllowInvalidCertificate();

			var uri = new Uri( url );
			var request = ( HttpWebRequest )WebRequest.Create( uri );
			request.Method = WebRequestMethods.Http.Get;
			request.Timeout = this.RequestTimeoutMs;
			request.ReadWriteTimeout = this.RequestTimeoutMs;

			return request;
		}

		private HttpWebRequest CreateServicePostRequest( string endpoint, string content )
		{
			this.AllowInvalidCertificate();

			var uri = new Uri( endpoint.GetFullEndpointWithAuth( this._config ) );
			var request = ( HttpWebRequest )WebRequest.Create( uri );

			request.Method = WebRequestMethods.Http.Post;
			request.Timeout = this.RequestTimeoutMs;
			request.ReadWriteTimeout = this.RequestTimeoutMs;
			request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
			request.Headers[ "Content-Action" ] = "Volusion_API";

			using( var writer = new StreamWriter( request.GetRequestStream() ) )
				writer.Write( content );

			return request;
		}
		#endregion

		#region Misc
		private T ParseResponse< T >( WebResponse response, string marker )
		{
			using( var stream = response.GetResponseStream() )
			{
				var reader = new StreamReader( stream );
				var xmlResponse = reader.ReadToEnd();

				var urlWithoutPass = this.LogGetResponse( response.ResponseUri.ToString(), xmlResponse, marker );

				if( string.IsNullOrEmpty( xmlResponse ) )
					throw new Exception( "Marker:" + marker + " Volusion returned empty result for " + urlWithoutPass + ". One of possible problems is incorrect credentials." );

				try
				{
					var result = XmlSerializeHelpers.Deserialize< T >( xmlResponse );
					return result;
				}
				catch( Exception ex )
				{
					throw new Exception( "Marker:" + marker + " Can't to deserialize response for " + urlWithoutPass, ex );
				}
			}
		}

		private CancellationToken GetTimeoutToken()
		{
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.CancelAfter( this.RequestTimeoutMs );
			return cancellationTokenSource.Token;
		}

		private void LogGetRequest( string url, string marker )
		{
			var urlWithoutPass = this.GetUrlWithoutPassword( url );
			VolusionLogger.Log.Trace( "Marker:{0}\tGet Request for url '{1}'", marker, urlWithoutPass );
		}

		private string LogGetResponse( string originalUrl, string xmlResponse, string marker )
		{
			var urlWithoutPass = this.GetUrlWithoutPassword( originalUrl );
			VolusionLogger.Log.Trace( "Marker:{0}\tGet Response for url '{1}'\n{2}", marker, urlWithoutPass, xmlResponse );
			return urlWithoutPass;
		}

		private Exception GetException( string url, Exception ex, string marker )
		{
			var urlWithoutPass = this.GetUrlWithoutPassword( url );
			var message = string.Format( "Marker:{0}\tCan't to get data for url '{1}'", marker, urlWithoutPass );
			VolusionLogger.Log.Trace( message );
			return new Exception( message, ex );
		}

		private void LogUpdateRequest( string url, string xmlContent, string marker )
		{
			var urlWithoutPass = this.GetUrlWithoutPassword( url );
			VolusionLogger.Log.Trace( "Marker:{0}\tPUT/POST Request for url '{1}'\n{2}", marker, urlWithoutPass, xmlContent );
		}

		private void LogUpdateResponse( string url, HttpStatusCode statusCode, string marker )
		{
			var urlWithoutPass = this.GetUrlWithoutPassword( url );
			VolusionLogger.Log.Trace( "Marker:{0}\tPUT/POST Response for url '{1}' with code '{2}'", marker, urlWithoutPass, statusCode );
		}

		private Exception UpdateException( string url, Exception ex, string marker )
		{
			var urlWithoutPass = this.GetUrlWithoutPassword( url );
			var message = string.Format( "Marker:{0}\tCan't to put/post data for url '{1}'", marker, urlWithoutPass );
			VolusionLogger.Log.Trace( message );
			return new Exception( message, ex );
		}

		private string GetUrlWithoutPassword( string url )
		{
			var urlWithoutPass = Regex.Replace( url, "(EncryptedPassword=)\\w+", "EncryptedPassword=***" );
			return urlWithoutPass;
		}
		#endregion

		#region SSL certificate hack
		private void AllowInvalidCertificate()
		{
			ServicePointManager.ServerCertificateValidationCallback += this.AllowCert;
		}

		private bool AllowCert( object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error )
		{
			return true;
		}
		#endregion
	}
}