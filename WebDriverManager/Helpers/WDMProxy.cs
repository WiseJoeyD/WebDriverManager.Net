using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Text;

namespace WebDriverManager.Proxy
{
    public class WDMProxy
    {
        public WebProxy userProxy { get; private set; }

        public WDMProxy()
        {
            userProxy = new WebProxy();
        }

        /// <summary>
        /// Sets Address for Proxy
        /// <para>Enter full url, and optional port number, of proxy</para>
        /// </summary>
        /// <param name="url">Full URL, including port only if necessary</param>
        public void SetProxyURL(string url)
        {
            userProxy.Address = new Uri(url);
        }

        /// <summary>
        /// Sets Network Credentials for Proxy
        /// <para>Enter Username and Password, and optionally domain, for proxy</para>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="domain"></param>
        public void SetProxyCredentials(string userName, SecureString password, string domain = "")
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                userProxy.Credentials = new NetworkCredential(userName, password);
            }
            else
            {
                userProxy.Credentials = new NetworkCredential(userName, password, domain);
            }
        }
    }
}
