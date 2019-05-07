using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    public class FileHelper
    {
        public static bool IsFileExistant(string fullPath)
        {
            return File.Exists(fullPath);
        }
    }

    public class FileHelper<T>
    {
        public static T LoadObjectFromJson(string path)
        {
            if(FileHelper.IsFileExistant(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    try
                    {
                        string json = r.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                    catch
                    {
                        return default(T);
                    }
                }
            }
            else
            {
                return default(T);
            }
        }

        public static bool CreateFileFromObject(T entityObject, string path, string fileName)
        {
            try
            {
                string fullPath = Path.Combine(path, fileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                File.Create(fullPath).Dispose();

                using (StreamWriter w = new StreamWriter(Path.Combine(path, fileName)))
                {
                    w.Write(JsonConvert.SerializeObject(entityObject));
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public List<T> LoadObjectsFromJson(string address)
        {
            using (StreamReader r = new StreamReader(address))
            {
                try
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
