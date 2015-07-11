using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using DotNetOpenAuth.AspNet;
using Newtonsoft.Json;

namespace FDI.Common
{
    public class FacebookScopedClient : IAuthenticationClient
    {
        private readonly string _appId;
        private readonly string _appSecret;
        private readonly string _scope;

        private const string BaseUrl = "https://www.facebook.com/dialog/oauth?client_id=";
        public const string GraphApiToken = "https://graph.facebook.com/oauth/access_token?";
        public const string GraphApiMe = "https://graph.facebook.com/me?";

        private static string GetHTML(string url)
        {
            string connectionString = url;

            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(connectionString);
                myRequest.Credentials = CredentialCache.DefaultCredentials;
                //// Get the response
                var webResponse = myRequest.GetResponse();
                var respStream = webResponse.GetResponseStream();
                ////
                if (respStream != null)
                {
                    var ioStream = new StreamReader(respStream);
                    var pageContent = ioStream.ReadToEnd();
                    //// Close streams
                    ioStream.Close();
                    respStream.Close();
                    return pageContent;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private IDictionary<string, string> GetUserData(string accessCode, string redirectURI)
        {
            var token = GetHTML(GraphApiToken + "client_id=" + _appId + "&redirect_uri=" + HttpUtility.UrlEncode(redirectURI) + "&client_secret=" + _appSecret + "&code=" + accessCode);
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            var accessToken = token.Substring(token.IndexOf("access_token=", StringComparison.Ordinal), token.IndexOf("&", StringComparison.Ordinal));
            var data = GetHTML(GraphApiMe + "fields=id,name,email,username,gender,link&" + accessToken);

            // this dictionary must contains
            var userData = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            return userData;
        }

        public FacebookScopedClient(string appId, string appSecret, string scope)
        {
            _appId = appId;
            _appSecret = appSecret;
            _scope = scope;
        }

        public string ProviderName
        {
            get { return "Facebook"; }
        }

        public void RequestAuthentication(HttpContextBase context, Uri returnUrl)
        {
            var url = BaseUrl + _appId + "&redirect_uri=" + HttpUtility.UrlEncode(returnUrl.ToString()) + "&scope=" + _scope;
            context.Response.Redirect(url);
        }

        public AuthenticationResult VerifyAuthentication(HttpContextBase context)
        {
            var code = context.Request.QueryString["code"];

            if (context.Request.Url != null)
            {
                var rawUrl = context.Request.Url.OriginalString;
                //From this we need to remove code portion
                rawUrl = Regex.Replace(rawUrl, "&code=[^&]*", "");

                var userData = GetUserData(code, rawUrl);

                if (userData == null)
                    return new AuthenticationResult(false, ProviderName, null, null, null);
                AuthenticationResult result;
                try
                {
                    var id = userData["id"];
                    var username = userData["username"];
                    userData.Remove("id");
                    userData.Remove("username");
                    result = new AuthenticationResult(true, ProviderName, id, username, userData);
                }
                catch (Exception) 
                {
                    result = new AuthenticationResult(false, ProviderName, null, null, null);
                }
                
                return result;
            }
            return null;
        }
    }
}