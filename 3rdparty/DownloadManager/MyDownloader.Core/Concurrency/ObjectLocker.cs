using System;
using System.Threading;

namespace MyDownloader.Core.Concurrency
{
    public class ObjectLocker : IDisposable
    {
        #region Fields

        private object obj;

        #endregion Fields

        #region Constructor

        public ObjectLocker(object obj)
        {
            this.obj = obj;
            Monitor.Enter(this.obj);
        }

        #endregion Constructor

        #region IDisposable Members

        public void Dispose()
        {
            Monitor.Exit(this.obj);
        }

        #endregion IDisposable Members
    }
}