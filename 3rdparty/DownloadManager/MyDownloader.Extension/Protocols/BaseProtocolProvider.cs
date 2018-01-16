using MyDownloader.Core;
using System;
using System.Net;

namespace MyDownloader.Extension.Protocols
{
    public class BaseProtocolProvider
    {
        static BaseProtocolProvider()
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
        }

        protected WebRequest GetRequest(ResourceLocation location)
        {
            WebRequest request = WebRequest.Create(location.URL);
            request.Timeout = 30000;
            SetProxy(request);
            return request;
        }

        protected void SetProxy(WebRequest request)
        {
            if (HttpFtpProtocolExtension.parameters.UseProxy)
            {
                WebProxy proxy = new WebProxy(HttpFtpProtocolExtension.parameters.ProxyAddress, HttpFtpProtocolExtension.parameters.ProxyPort)
                {
                    BypassProxyOnLocal = HttpFtpProtocolExtension.parameters.ProxyByPassOnLocal
                };
                request.Proxy = proxy;

                if (!String.IsNullOrEmpty(HttpFtpProtocolExtension.parameters.ProxyUserName))
                {
                    request.Proxy.Credentials = new NetworkCredential(
                        HttpFtpProtocolExtension.parameters.ProxyUserName,
                        HttpFtpProtocolExtension.parameters.ProxyPassword,
                        HttpFtpProtocolExtension.parameters.ProxyDomain);
                }
            }
        }
    }
}