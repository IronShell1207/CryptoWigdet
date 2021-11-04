using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptoWigdet
{
    class ExtPathes
    {
        public static string MainFolder
        {
            get
            {
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CryptoWidgets\";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }
        public static string FavCursPath = MainFolder + "favcurs.json";
    }
}
