using NCoreAssignmentApp.Readers;

namespace NCoreAssignmentApp.Tests
{
    public class ReaderTests
    {
        private readonly string TestFilesPath = Path.GetFullPath("TestFiles");
        private readonly string TextFileName = "test.txt";

        [Fact]
        public async void GivenTextFile_WhenTextReaderReads_ThenShouldReturnContents()
        {
            var textReader = new NCoreTextReader();

            var result = await textReader.ReadContent(Path.Combine(TestFilesPath, TextFileName));

            Assert.Contains("reading the contents of this text file", result);
        }
    }
}