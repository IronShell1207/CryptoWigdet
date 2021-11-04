using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoWigdet.ViewModels
{
    internal class WidgetModel : ViewModel
    {
        #region Название пары

        private string _TitlePair = "BTCUSDT";
        public string TitlePair
        {
            get => _TitlePair;
            set => Set(ref _TitlePair, value);
        }
        #endregion
        #region Цена пары

        private string _PairCurr = "0.00";
        public string PairCurr
        {
            get => _PairCurr;
            set => Set(ref _PairCurr, value);
        }
        #endregion

    }
}
