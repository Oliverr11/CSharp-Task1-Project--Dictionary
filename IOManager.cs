using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Task1ExamCSharpp
{
    public class IOManager
    {
        private string extension = ".json";

        public void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void WriteJson(string path, string fileName, Object obj)
        {
            EnsureDirectoryExists(path);

            string fullPath = Path.Combine(path, fileName + extension);

            StreamWriter streamWriter = new StreamWriter(fullPath);
            string content = JsonConvert.SerializeObject(obj);
            streamWriter.Write(content);
            streamWriter.Close();
        }

        public T ReadJson<T>(string path, string fileName)
        {
            string fullPath = Path.Combine(path, fileName + extension);
            string content = string.Empty;

            if (File.Exists(fullPath))
            {
                StreamReader streamReader = new StreamReader(fullPath);
                content = streamReader.ReadToEnd();
                streamReader.Close();

                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                throw new FileNotFoundException("The file was not found.", fullPath);
            }
        }

        public List<FileInfo> LoadFiles(string path)
        {
            EnsureDirectoryExists(path);

            DirectoryInfo folder = new DirectoryInfo(path);
            FileInfo[] files = folder.GetFiles();
            return files.ToList();
        }

        public string GetFileName(FileInfo file)
        {
            return Path.GetFileNameWithoutExtension(file.Name);
        }
    }
}
