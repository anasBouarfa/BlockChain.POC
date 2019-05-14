using Newtonsoft.Json;
using System.Collections.Generic;

namespace System.IO
{
    public class FileHelper
    {
        public static bool IsFileExistant(string fullPath)
        {
            return File.Exists(fullPath);
        }

        public static string ReadFromFile(string path)
        {
            if (IsFileExistant(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    try
                    {
                        return r.ReadToEnd();
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }

    public class FileHelper<T>
    {
        public static T LoadObjectFromJson(string path)
        {
            if (FileHelper.IsFileExistant(path))
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

                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

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
            catch
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