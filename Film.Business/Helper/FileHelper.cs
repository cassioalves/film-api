using System;
using System.IO;
using System.Text;

namespace Film.Business
{
    public class FileHelper
    {
        public void SaveTextFile(string text, string fileName)
        {
            try
            {
                var path = $"{fileName}";
                File.WriteAllText(path, text, Encoding.GetEncoding("ISO-8859-1"));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
