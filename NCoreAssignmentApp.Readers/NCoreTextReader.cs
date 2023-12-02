using System.Text;

namespace NCoreAssignmentApp.Readers
{
    public class NCoreTextReader : IReader
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
    }
}
