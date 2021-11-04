using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptoWigdet
{
    public class ExchangesHandlers
    {
        private static Settings _SettingsH;
        public static Settings SettingsH
        {
            get
            {
                if (_SettingsH != null)
                    return _SettingsH;
                if (File.Exists(ExtPathes.FavCursPath))
                {
                    _SettingsH = JsonHandler.LoadRCS(ExtPathes.FavCursPath);
                    return _SettingsH;
                }
                _SettingsH = new Settings { FavPairs = new List<string> { }, WSize = 0.8, WOpacity = 0.5 };
                return _SettingsH;

            }
            set
            {
                _SettingsH = value;
            }
        }

    }
}
