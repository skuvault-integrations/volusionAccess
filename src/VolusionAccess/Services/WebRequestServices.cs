using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Netco.Logging;
using ServiceStack;
using VolusionAccess.Models.Configuration;

namespace VolusionAccess.Services
{
	internal class WebRequestServices
	{
		private readonly VolusionConfig _config;

		public WebRequestServices( VolusionConfig config )
		{
			this._config = config;
		}

		public T GetResponse< T >( string commandParams )
		{
			var url = string.Concat( this._config.HostWithAuth, commandParams );
			var result = this.GetResponseForSpecificUrl< T >( url );
			return result;
		}

		public T GetResponseForSpecificUrl< T >( string url )
		{
			T result;
			var request = this.CreateGetServiceGetRequest( url );
			using( var response = request.GetResponse() )
				result = ParseResponse< T >( response );

			return result;
		}

		public async Task< T > GetResponseAsync< T >( string commandParams )
		{
			var url = string.Concat( this._config.HostWithAuth, commandParams );
			var result = await this.GetResponseForSpecificUrlAsync< T >( url );
			return result;
		}

		public async Task< T > GetResponseForSpecificUrlAsync< T >( string url )
		{
			T result;
			var request = this.CreateGetServiceGetRequest( url );
			using( var response = await request.GetResponseAsync() )
				result = ParseResponse< T >( response );

			return result;
		}

		public void PutData( string endpoint, string jsonContent )
		{
			var request = this.CreateServicePutRequest( endpoint, jsonContent );
			using( var response = ( HttpWebResponse )request.GetResponse() )
				this.LogUpdateInfo( endpoint, response.StatusCode, jsonContent );
		}

		public async Task PutDataAsync( string endpoint, string jsonContent )
		{
			var request = this.CreateServicePutRequest( endpoint, jsonContent );
			using( var response = await request.GetResponseAsync() )
				this.LogUpdateInfo( endpoint, ( ( HttpWebResponse )response ).StatusCode, jsonContent );
		}

		#region WebRequest configuration
		private HttpWebRequest CreateGetServiceGetRequest( string url )
		{
			this.AllowInvalidCertificate();

			var uri = new Uri( url );
			var request = ( HttpWebRequest )WebRequest.Create( uri );

			request.Method = WebRequestMethods.Http.Get;
			request.Headers.Add( "Authorization", this.CreateAuthenticationHeader() );

			return request;
		}

		private HttpWebRequest CreateServicePutRequest( string endpoint, string content )
		{
			this.AllowInvalidCertificate();

			var uri = new Uri( string.Concat( this._config.HostWithAuth, endpoint ) );
			var request = ( HttpWebRequest )WebRequest.Create( uri );

			request.Method = WebRequestMethods.Http.Put;
			request.ContentType = "application/x-www-form-urlencoded";
			request.Headers.Add( "Authorization", this.CreateAuthenticationHeader() );

			using( var writer = new StreamWriter( request.GetRequestStream() ) )
				writer.Write( content );

			return request;
		}
		#endregion

		#region Misc
		private T ParseResponse< T >( WebResponse response )
		{
			var result = default( T );

			using( var stream = response.GetResponseStream() )
			{
				var reader = new StreamReader( stream );
				var jsonResponse = reader.ReadToEnd();

				this.Log().Trace( "[volusion]\tResponse\t{0} - {1}", response.ResponseUri, jsonResponse );

				if( !String.IsNullOrEmpty( jsonResponse ) )
					result = jsonResponse.FromXml< T >();
			}

			return result;
		}

		private string CreateAuthenticationHeader()
		{
			var authInfo = string.Concat( this._config.UserName, ":", this._config.Password );
			authInfo = Convert.ToBase64String( Encoding.Default.GetBytes( authInfo ) );

			return string.Concat( "Basic ", authInfo );
		}

		private void LogUpdateInfo( string url, HttpStatusCode statusCode, string jsonContent )
		{
			this.Log().Trace( "[volusion]\tPUT/POST call for the url '{0}' has been completed with code '{1}'.\n{2}", url, statusCode, jsonContent );
		}
		#endregion

		#region SSL certificate hack
		private void AllowInvalidCertificate()
		{
			ServicePointManager.ServerCertificateValidationCallback += AllowCert;
		}

		private bool AllowCert( object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error )
		{
			return true;
		}
		#endregion
	}
}