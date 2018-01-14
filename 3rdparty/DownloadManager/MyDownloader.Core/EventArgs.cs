using System;

namespace MyDownloader.Core
{
    #region ResolvingProtocolProviderEventArgs

    public class ResolvingProtocolProviderEventArgs : EventArgs
    {
        #region Fields

        private IProtocolProvider provider;
        private string url;

        #endregion Fields

        #region Constructor

        public ResolvingProtocolProviderEventArgs(IProtocolProvider provider,
            string url)
        {
            this.url = url;
            this.provider = provider;
        }

        #endregion Constructor

        #region Properties

        public string URL
        {
            get { return url; }
        }

        public IProtocolProvider ProtocolProvider
        {
            get { return provider; }
            set { provider = value; }
        }

        #endregion Properties
    }

    #endregion ResolvingProtocolProviderEventArgs

    #region DownloaderEventArgs

    public class DownloaderEventArgs : EventArgs
    {
        #region Fields

        private Downloader downloader;
        private bool willStart;

        #endregion Fields

        #region Constructor

        public DownloaderEventArgs(Downloader download)
        {
            this.downloader = download;
        }

        public DownloaderEventArgs(Downloader download, bool willStart) : this(download)
        {
            this.willStart = willStart;
        }

        #endregion Constructor

        #region Properties

        public Downloader Downloader
        {
            get { return downloader; }
        }

        public bool WillStart
        {
            get { return willStart; }
        }

        #endregion Properties
    }

    #endregion DownloaderEventArgs

    #region SegmentEventArgs

    public class SegmentEventArgs : DownloaderEventArgs
    {
        #region Fields

        private Segment segment;

        #endregion Fields

        #region Constructor

        public SegmentEventArgs(Downloader d, Segment segment)
            : base(d)
        {
            this.segment = segment;
        }

        #endregion Constructor

        #region Properties

        public Segment Segment
        {
            get { return segment; }
            set { segment = value; }
        }

        #endregion Properties
    }

    #endregion SegmentEventArgs
}