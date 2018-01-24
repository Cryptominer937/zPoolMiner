using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner.Interfaces
{
    public interface IMinerUpdateIndicator
    {
        void SetMaxProgressValue(int max);
        void SetProgressValueAndMsg(int value, string msg);
        void SetTitle(string title);
        void FinishMsg(bool success);
    }
}
