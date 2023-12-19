using NCoreAssignmentApp.Readers.EncryptionEnum;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NCoreAssignmentApp.Readers
{
    public class NCoreXmlReader
    {
        public class XmlReaderModel
        {
            public List<string> XmlTags { get; set; } = [];
            public List<string> XmlValues { get; set; } = [];
        }


        public async Task<XmlReaderModel> ReadContent(string filePath)
        {
            XmlReaderSettings settings = new()
            {
                Async = true
            };

            XmlReaderModel model = new();

            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            model.XmlTags.Add($"\n{reader.Name}:");
                            break;
                        case XmlNodeType.Text:
                            model.XmlValues.Add($"{await reader.GetValueAsync()}");
                            break;
                        default:
                            break;
                    }
                }
            }

            return model;
        }

        public async Task<XmlReaderModel> ReadEncryptedContent(string filePath, EncryptionType encryptionType)
        {
            var model = await ReadContent(filePath);

            if (encryptionType == EncryptionType.Reverse)
            {
                return DecryptReverseEncryption(model);
            }

            return DecryptZeroEncrypion(model);
        }

        private static XmlReaderModel DecryptReverseEncryption(XmlReaderModel model)
        {
            for (int i = 0; i < model.XmlValues.Count; i++)
            {
                var charArray = model.XmlValues[i].ToCharArray();
                Array.Reverse(charArray);
                model.XmlValues[i] = new string(charArray);
            }

            return model;
        }

        private static XmlReaderModel DecryptZeroEncrypion(XmlReaderModel model)
        {
            for (int i = 0; i < model.XmlValues.Count; i++)
            {
                var zeroed = model.XmlValues[i];
                model.XmlValues[i] = zeroed.Replace("0", string.Empty); ;
            }

            return model;
        }
    }
}
