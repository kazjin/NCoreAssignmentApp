using NCoreAssignmentApp.Readers;

namespace NCoreAssignmentApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to NCoreAssignmentApp!");

            var chosenKey = ShowMenu();

            await ProcessChoice(chosenKey);
        }

        private static async Task ProcessChoice(ConsoleKeyInfo chosenKey)
        {
            string filePath;
            switch (chosenKey.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    Console.WriteLine("\nSelected to read a text file");
                    filePath = RequestFilePath();
                    await ReadTextAndWriteToConsole(filePath);
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    Console.WriteLine("\nSelected to read a xml file");
                    filePath = RequestFilePath();
                    await ReadXmlAndWriteToConsole(filePath);
                    break;
                default:
                    Console.WriteLine("\nInput is not valid. Try again");
                    chosenKey = ShowMenu();
                    await ProcessChoice(chosenKey);
                    break;
            }
        }

        private static ConsoleKeyInfo ShowMenu()
        {
            Console.WriteLine("Choose which file type to read:");
            Console.WriteLine("1. text file");
            Console.WriteLine("2. xml file");
            var choiceKey = Console.ReadKey();
            return choiceKey;
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

        private static async Task ReadXmlAndWriteToConsole(string filePath)
        {
            var nCoreTextReader = new NCoreXmlReader();
            var content = await nCoreTextReader.ReadContent(filePath);

            Console.WriteLine("The contents of the file:");
            Console.WriteLine(content);
        }
    }
}
