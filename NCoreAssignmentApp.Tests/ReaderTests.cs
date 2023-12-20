using NCoreAssignmentApp.Readers;
using NCoreAssignmentApp.Readers.EncryptionEnum;

namespace NCoreAssignmentApp.Tests
{
    public class ReaderTests
    {
        private readonly string TestFilesPath = Path.GetFullPath("TestFiles");
        private readonly string TextFileName = "test.txt";
        private readonly string ReverseEncryptedTextFileName = "reverse-encrypted.txt";
        private readonly string ZeroEncryptedTextFileName = "zero-encrypted.txt";
        private readonly string XmlFileName = "xml-test.xml";
        private readonly string ReverseEncryptedXmlFileName = "xml-contents-reversed.xml";
        private readonly string ZeroEncryptedXmlFileName = "zero-xml-contents.xml";
        private readonly string JsonFileName = "json.json";

        [Fact]
        public async void GivenTextFile_WhenTextReaderReads_ThenShouldReturnContents()
        {
            var textReader = new NCoreTextReader();

            var result = await textReader.ReadContent(Path.Combine(TestFilesPath, TextFileName));

            Assert.Contains("reading the contents of this text file", result);
        }

        [Fact]
        public async void GivenReverseEncryptedTextFile_WhenTextReaderReadsEncrypted_ThenShouldReturnDecryptedContents()
        {
            var textReader = new NCoreTextReader();

            var result = await textReader.ReadEncryptedContent(Path.Combine(TestFilesPath, ReverseEncryptedTextFileName), EncryptionType.Reverse);

            Assert.Contains("This is some encrypted text in a file", result);
        }

        [Fact]
        public async void GivenZeroEncryptedTextFile_WhenTextReaderReadsEncrypted_ThenShouldReturnDecryptedContents()
        {
            var textReader = new NCoreTextReader();

            var result = await textReader.ReadEncryptedContent(Path.Combine(TestFilesPath, ZeroEncryptedTextFileName), EncryptionType.Zero);

            Assert.Contains("This is some encrypted text in a file", result);
        }

        [Fact]
        public async void GivenXmlFile_WhenXmlReaderReads_ThenShouldOutpoutContents()
        {
            var xmlReader = new NCoreXmlReader();

            var result = await xmlReader.ReadContent(Path.Combine(TestFilesPath, XmlFileName));

            Assert.Contains("reading text content from xml file", result.XmlValues);
        }

        [Fact]
        public async void GivenReverseEncryptedXmlFile_WhenXmlReaderReads_ThenShouldOutpoutDecryptedContents()
        {
            var xmlReader = new NCoreXmlReader();

            var result = await xmlReader.ReadEncryptedContent(Path.Combine(TestFilesPath, ReverseEncryptedXmlFileName), EncryptionType.Reverse);

            Assert.Contains("You are reading the body of xml file", result.XmlValues);
        }

        [Fact]
        public async void GivenZeroEncryptedXmlFile_WhenXmlReaderReads_ThenShouldOutpoutDecryptedContents()
        {
            var xmlReader = new NCoreXmlReader();

            var result = await xmlReader.ReadEncryptedContent(Path.Combine(TestFilesPath, ZeroEncryptedXmlFileName), EncryptionType.Zero);

            Assert.Contains("reading text content from xml file", result.XmlValues);
        }

        [Fact]
        public async void GivenJsonFile_WhenJsonReaderReads_ThenShouldOutpoutContents()
        {
            var jsonReader = new NCoreJsonReader();

            var result = await jsonReader.ReadContent(Path.Combine(TestFilesPath, JsonFileName));

            Assert.Contains("Standard Generalized Markup Language", result);
        }

    }
}