using MyDownloader.Core;
using MyDownloader.Core.Extensions;
using System;

namespace MyDownloader.Extension.Protocols
{
    public class HttpFtpProtocolExtension : IExtension
    {
        internal static IHttpFtpProtocolParameters parameters;

        #region IExtension Members

        public string Name
        {
            get { return "HTTP/FTP"; }
        }

        public IUIExtension UIExtension
        {
            get { return new HttpFtpProtocolUIExtension(); }
        }

        public HttpFtpProtocolExtension() :
            this(new HttpFtpProtocolParametersSettingsProxy())
        {
        }

        public HttpFtpProtocolExtension(IHttpFtpProtocolParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (HttpFtpProtocolExtension.parameters != null)
            {
                throw new InvalidOperationException("The type HttpFtpProtocolExtension is already initialized.");
            }

            HttpFtpProtocolExtension.parameters = parameters;

            ProtocolProviderFactory.RegisterProtocolHandler("http", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("https", typeof(HttpProtocolProvider));
            ProtocolProviderFactory.RegisterProtocolHandler("ftp", typeof(FtpProtocolProvider));
        }

        #endregion IExtension Members
    }
}