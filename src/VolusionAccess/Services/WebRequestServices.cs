using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VolusionAccess.Misc;
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
			var url = commandParams.GetFullEndpointWithAuth( this._config );
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
			var url = commandParams.GetFullEndpointWithAuth( this._config );
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

		public void PostData( string endpoint, string xmlContent )
		{
			var request = this.CreateServicePostRequest( endpoint, xmlContent );
			using( var response = ( HttpWebResponse )request.GetResponse() )
				this.LogUpdateInfo( request.Address.OriginalString, response.StatusCode, xmlContent );
		}

		public async Task PostDataAsync( string endpoint, string xmlContent )
		{
			var request = this.CreateServicePostRequest( endpoint, xmlContent );
			using( var response = await request.GetResponseAsync() )
				this.LogUpdateInfo( request.Address.OriginalString, ( ( HttpWebResponse )response ).StatusCode, xmlContent );
		}

		#region WebRequest configuration
		private HttpWebRequest CreateGetServiceGetRequest( string url )
		{
			this.AllowInvalidCertificate();

			var uri = new Uri( url );
			var request = ( HttpWebRequest )WebRequest.Create( uri );
			request.Method = WebRequestMethods.Http.Get;

			return request;
		}

		private HttpWebRequest CreateServicePostRequest( string endpoint, string content )
		{
			this.AllowInvalidCertificate();

			var uri = new Uri( endpoint.GetFullEndpointWithAuth( this._config ) );
			var request = ( HttpWebRequest )WebRequest.Create( uri );

			request.Method = WebRequestMethods.Http.Post;
			request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
			request.Headers[ "Content-Action" ] = "Volusion_API";

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
				var xmlResponse = reader.ReadToEnd();

				VolusionLogger.Log.Trace( "Response\t{0} - {1}", response.ResponseUri, xmlResponse );

				if( !String.IsNullOrEmpty( xmlResponse ) )
					result = XmlSerializeHelpers.Deserialize< T >( xmlResponse );
			}

			return result;
		}

		private void LogUpdateInfo( string url, HttpStatusCode statusCode, string xmlContent )
		{
			VolusionLogger.Log.Trace( "Response\tPUT/POST call for the url '{0}' has been completed with code '{1}'.\n{2}", url, statusCode, xmlContent );
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