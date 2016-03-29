namespace SimpleTokenOAuth
{
    using Config;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    /// <summary>
    /// The token Authorize Attribute
    /// </summary>
    public class TokenAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Over Authorization 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // Checking wheather the Authorization parameter is pending
            if (actionContext == null || actionContext.Request == null || actionContext.Request.Headers == null || actionContext.Request.Headers.Authorization == null)
            {
                return false;
            }

            /// Checking whether the scheme is "Token"
            if (actionContext.Request.Headers.Authorization.Scheme != "Token")
            {
                return false;
            }

            // Getting the access Token value
            var token = actionContext.Request.Headers.Authorization.Parameter;

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            // Getting the config values
            var config = SimpleTokenAuthConfiguration.Instance;

            // Checking the config and Token Count
            if (config == null || !config.IsEnabled || config.Tokens == null || config.Tokens.Count == 0)
            {
                return false;
            }

            // Checking for the Mathcing Token
            var matchingToken = config.Tokens.GetElement(token);

            if (matchingToken == null || !matchingToken.IsAllowed)
            {
                return false;
            }

            return true;
        }

    }
}
