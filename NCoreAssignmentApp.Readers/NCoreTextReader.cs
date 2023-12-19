using NCoreAssignmentApp.Readers.EncryptionEnum;
using System.Text;

namespace NCoreAssignmentApp.Readers
{
    public class NCoreTextReader
    {
        public NCoreTextReader()
        {

        }

        public async Task<string> ReadContent(string filePath)
        {
            char[] result;
            StringBuilder builder = new();

            using (StreamReader reader = File.OpenText(filePath))
            {
                result = new char[reader.BaseStream.Length];
                await reader.ReadAsync(result, 0, (int)reader.BaseStream.Length);
            }

            foreach (char c in result)
            {
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        public async Task<string> ReadEncryptedContent(string filePath, EncryptionType encryptionType)
        {
            var encryptedText = await ReadContent(filePath);

            if (encryptionType == EncryptionType.Reverse)
            {
                var charArray = encryptedText.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }

            return encryptedText.Replace("0", string.Empty);
        }
    }
}
