using System;

namespace WebDriverManager.Helpers
{
    public static class UrlHelper
    {
        public static string BuildUrl(string url, string version)
        {
            var release = version.LastIndexOf(".", StringComparison.CurrentCulture);

            if (release < 0)
            {
                return url
                .Replace("<version>", version);
            }
           else
            {
                return url
                .Replace("<version>", version)
                .Replace("<release>", version.Substring(0, release));
            }
            
        }
    }
}