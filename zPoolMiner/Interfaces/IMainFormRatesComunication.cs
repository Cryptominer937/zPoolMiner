using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner.Interfaces {
    public interface IMainFormRatesComunication {
        void ClearRatesALL();
        void ClearRates(int groupCount);
        void AddRateInfo(string groupName, string deviceStringInfo, APIData iAPIData, double paying, bool isApiGetException);
        void ShowNotProfitable(string msg);
        void HideNotProfitable();
        //void RaiseAlertSharesNotAccepted(string algoName);
    }
}
