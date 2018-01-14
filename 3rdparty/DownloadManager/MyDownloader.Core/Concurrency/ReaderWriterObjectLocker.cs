using System;
using System.Threading;

namespace MyDownloader.Core.Concurrency
{
    public class ReaderWriterObjectLocker
    {
        #region BaseReleaser

        private class BaseReleaser
        {
            protected ReaderWriterObjectLocker locker;

            public BaseReleaser(ReaderWriterObjectLocker locker)
            {
                this.locker = locker;
            }
        }

        #endregion BaseReleaser

        #region ReaderReleaser

        private class ReaderReleaser : BaseReleaser, IDisposable
        {
            public ReaderReleaser(ReaderWriterObjectLocker locker)
                : base(locker)
            {
            }

            #region IDisposable Members

            public void Dispose()
            {
                locker.locker.ReleaseReaderLock();
            }

            #endregion IDisposable Members
        }

        #endregion ReaderReleaser

        #region WriterReleaser

        private class WriterReleaser : BaseReleaser, IDisposable
        {
            public WriterReleaser(ReaderWriterObjectLocker locker)
                : base(locker)
            {
            }

            #region IDisposable Members

            public void Dispose()
            {
                locker.locker.ReleaseWriterLock();
            }

            #endregion IDisposable Members
        }

        #endregion WriterReleaser

        #region Fields

        private ReaderWriterLock locker;
        private IDisposable writerReleaser;
        private IDisposable readerReleaser;

        #endregion Fields

        #region Constructor

        public ReaderWriterObjectLocker()
        {
            // TODO: update to ReaderWriterLockSlim on .net 3.5
            locker = new ReaderWriterLock();

            writerReleaser = new WriterReleaser(this);
            readerReleaser = new ReaderReleaser(this);
        }

        #endregion Constructor

        #region Methods

        public IDisposable LockForRead()
        {
            locker.AcquireReaderLock(-1);

            return readerReleaser;
        }

        public IDisposable LockForWrite()
        {
            locker.AcquireWriterLock(-1);

            return writerReleaser;
        }

        #endregion Methods
    }
}