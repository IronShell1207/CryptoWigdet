using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptoWigdet
{
    public class JsonHandler
    {
        public static void SaveJson(object listSet, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                JsonWriter jsonWriter = new JsonTextWriter(sw);
                JsonSerializer jsnS = new JsonSerializer();
                jsnS.Formatting = Formatting.Indented;
                jsnS.Serialize(jsonWriter, listSet);
            }
        }
        public static Settings LoadRCS(string path)
        {
            using (StreamReader jsReader = new StreamReader(path))
            {
                JsonReader json = new JsonTextReader(jsReader);
                JsonSerializer jsonSerializer = new JsonSerializer();
                var list = jsonSerializer.Deserialize<Settings>(json);
                return list;
            }
        }
       /* public static Settings LoadRCS(string path)
        {
            using (StreamReader jsReader = new StreamReader(path))
            {
                JsonReader json = new JsonTextReader(jsReader);
                JsonSerializer jsonSerializer = new JsonSerializer();
                var list = jsonSerializer.Deserialize<FavList>(json);
                return list;
            }
        }*/
    }

}
