using NCoreAssignmentApp.Readers;

namespace NCoreAssignmentApp.Tests
{
    public class ReaderTests
    {
        private readonly string TestFilesPath = Path.GetFullPath("TestFiles");
        private readonly string TextFileName = "test.txt";
        private readonly string XmlFileName = "xml-test.xml";
        private readonly string EncryptedTextFileName = "reverse-encrypted.txt";

        [Fact]
        public async void GivenTextFile_WhenTextReaderReads_ThenShouldReturnContents()
        {
            var textReader = new NCoreTextReader();

            var result = await textReader.ReadContent(Path.Combine(TestFilesPath, TextFileName));

            Assert.Contains("reading the contents of this text file", result);
        }

        [Fact]
        public async void GivenXmlFile_WhenXmlReaderReads_ThenShouldOutpoutContents()
        {
            var xmlReader = new NCoreXmlReader();

            var result = await xmlReader.ReadContent(Path.Combine(TestFilesPath, XmlFileName));

            Assert.Contains("reading text content from xml file", result);
        }

        [Fact]
        public async void GivenEncryptedTextFile_WhenTextReaderReads_ThenShouldReturnDecryptedContents()
        {
            var textReader = new NCoreTextReader();

            var result = await textReader.ReadEncryptedContent(Path.Combine(TestFilesPath, EncryptedTextFileName));

            Assert.Contains("This is some encrypted text in a file", result);
        }
    }
}