using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
 

namespace ATS
{
    class DBFile<T>
    {
        public static void Write(object data, string nameFile)
        {
            var serializer = new JsonSerializer();

            using (StreamWriter fs = new StreamWriter($"{nameFile}.json"))
            {
                using (var jsonTextWriter = new JsonTextWriter(fs))
                {
                    serializer.Serialize(fs, data);
                }
            }
        }
        public static List<T> Read(string nameFile)
        {
            var _dataList = new List<T> { };
            try
            {
                using (StreamReader file = File.OpenText($"{nameFile}.json"))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    _dataList = (List<T>)serializer.Deserialize(file, typeof(List<T>));
                }
                return _dataList;
            }
            catch { return _dataList; }
        }
    }
}
