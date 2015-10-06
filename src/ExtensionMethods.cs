using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorilla.Utilities
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Adds or Updates an existing claim 
        /// </summary>
        /// <param name="ClaimType">ClaimType</param>
        /// <param name="ClaimValue">ClaimValue</param>
        public static void AddOrUpdateClaim(this ICollection<IdentityUserClaim> userClaims, string ClaimType, string ClaimValue)
        {
            var currentClaim = userClaims.FirstOrDefault(x => x.ClaimType == ClaimType);

            if (currentClaim == null)
            {
                currentClaim = new IdentityUserClaim() { ClaimType = ClaimType };
                userClaims.Add(currentClaim);
            }

            currentClaim.ClaimValue = ClaimValue;
        }

        /// <summary>
        /// Get the errors of ModelState
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetErrors(this System.Web.Mvc.ModelStateDictionary model)
        {
            var errors = new List<string>();
            foreach (var item in model.Values.Where(x => x.Errors.Any()))
            {
                errors.AddRange(item.Errors.Select(x => x.ErrorMessage));
            }

            return string.Join(", ", errors);
        }

        /// <summary>
        /// Get the errors of ModelState
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetErrors(this System.Web.Http.ModelBinding.ModelStateDictionary model)
        {
            var errors = new List<string>();
            foreach (var item in model.Values.Where(x => x.Errors.Any()))
            {
                errors.AddRange(item.Errors.Select(x => x.ErrorMessage));
            }

            return string.Join(", ", errors);
        }

        /// <summary>
        /// ~/url/image.png to http://localhost:1933/url/image.png
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ToAbsoluteUrl(this string url, string protocol = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;
            }

            if (url.StartsWith("~"))
            {
                url = url.Substring(1, url.Length);
            }

            var currentRequest = HttpContext.Current.Request;
            return string.Format("{0}://{1}{2}", protocol ?? currentRequest.Url.Scheme, currentRequest.Url.Authority, currentRequest.ApplicationPath, url);
        }
    }


}
