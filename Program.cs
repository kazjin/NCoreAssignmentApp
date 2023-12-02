using NCoreAssignmentApp.Readers;

namespace NCoreAssignmentApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to NCoreAssignmentApp!");

            var filePath = RequestFilePath();

            await ReadTextAndWriteToConsole(filePath);
        }

        private static string RequestFilePath()
        {
            var filePath = string.Empty;

            while (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Insert in the next line the full path of the file:");
                filePath = Console.ReadLine();
            }

            return filePath;
        }

        private static async Task ReadTextAndWriteToConsole(string filePath)
        {
            var nCoreTextReader = new NCoreTextReader();
            var content = await nCoreTextReader.ReadContent(filePath);

            Console.WriteLine("The contents of the file:");
            Console.WriteLine(content);
        }

    }
}
