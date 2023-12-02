using System.Text;
using System.Xml;

namespace NCoreAssignmentApp.Readers
{
    public class NCoreXmlReader : IReader
    {
        public async Task<string> ReadContent(string filePath)
        {
            XmlReaderSettings settings = new()
            {
                Async = true
            };

            StringBuilder builder = new();

            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            builder.Append($"\n{reader.Name} ");
                            break;
                        case XmlNodeType.Text:
                            builder.Append($"text node: {await reader.GetValueAsync()}");
                            break;
                        default:
                            break;
                    }
                }
            }

            return builder.ToString();
        }
    }
}
