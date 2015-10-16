using System.Web;
using System.Web.Mvc;

namespace Gorilla.Utilities
{
    /// <summary>
    /// Extension method for razor
    /// </summary>
    public static class ExtensionMethodsRazor
    {
        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified action name, all params will be in json format
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="routeValues">An object that contains the parameters for a route</param>
        /// <returns></returns>
        public static MvcHtmlString ActionJson(this UrlHelper url, string actionName, object routeValues)
        {
            return ActionJsonResult(url.Action(actionName, routeValues));
        }

        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified action name, all params will be in json format
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">An object that contains the parameters for a route</param>
        /// <returns></returns>
        public static MvcHtmlString ActionJson(this UrlHelper url, string actionName, string controller, object routeValues)
        {
            return ActionJsonResult(url.Action(actionName, controller, routeValues));
        }

        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified action name, all params will be in json format
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="routeValues">An object that contains the parameters for a route</param>
        /// <param name="protocol">Protocol. Either http or https</param>
        /// <returns></returns>
        public static MvcHtmlString ActionJson(this UrlHelper url, string actionName, string controller, object routeValues, string protocol)
        {
            return ActionJsonResult(url.Action(actionName, controller, routeValues, protocol));
        }

        private static MvcHtmlString ActionJsonResult(string url)
        {
            return new MvcHtmlString(HttpUtility.UrlDecode(url));
        }
    }
}
