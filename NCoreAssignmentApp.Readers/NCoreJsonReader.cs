using NCoreAssignmentApp.Readers.EncryptionEnum;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using static NCoreAssignmentApp.Readers.NCoreXmlReader;

namespace NCoreAssignmentApp.Readers
{
    public class NCoreJsonReader
    {
        public NCoreJsonReader() { }

        public async Task<string> ReadContent(string filePath)
        {
            using StreamReader r = new(filePath);
            string json = r.ReadToEnd();
            var array = JsonConvert.DeserializeObject(json);

            return JsonConvert.SerializeObject(array);
        }

        public async Task<string> ReadEncryptedContent(string filePath, EncryptionType encryptionType)
        {
            var content = await ReadContent(filePath);

            if (encryptionType == EncryptionType.Reverse)
            {
                return DecryptReverseEncryption(content);
            }

            return DecryptZeroEncrypion(content);
        }

        private static string DecryptReverseEncryption(string jsonContent)
        {
            jsonContent = jsonContent.Replace("{", string.Empty);
            jsonContent = jsonContent.Replace("}", string.Empty);
            jsonContent = jsonContent.Replace("\"", string.Empty);

            var split = jsonContent.Split(':');

            StringBuilder builder = new StringBuilder();
            foreach (var kv in split)
            {
                var charArray = kv.ToCharArray();
                Array.Reverse(charArray);

                builder.Append($"{new string(charArray)} \n");
            }

            return builder.ToString();
        }

        private static string DecryptZeroEncrypion(string content)
        {
            return content.Replace("0", string.Empty);
        }
    }
}
