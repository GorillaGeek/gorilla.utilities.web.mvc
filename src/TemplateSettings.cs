namespace Gorilla.Utilities.Web.Mvc
{
    public class TemplateSettings
    {
        public static string Pattern = @"{{{{ {0} }}}}";
        public static string Index = @"$index";
        public static string ParentIndex = @"$parent.{0}";
    }
}
