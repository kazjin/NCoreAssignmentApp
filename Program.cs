using NCoreAssignmentApp.Readers;

namespace NCoreAssignmentApp
{
    internal class Program
    {
        private static NCoreTextReader? _nCoreTextReader;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to NCoreAssignmentApp!");

            var chosenFileTypeKey = ShowFileTypeMenu();
            var isEncrypted = ShowEncryptedMenu();

            await ProcessChoice(chosenFileTypeKey, isEncrypted);
        }

        private static bool ShowEncryptedMenu()
        {
            Console.WriteLine("Is the file encrypted? Type Y or N");
            Console.WriteLine("Y: yes");
            Console.WriteLine("N: no");
            var choiceKey = Console.ReadKey();

            return choiceKey.Key == ConsoleKey.Y;
        }

        private static async Task ProcessChoice(ConsoleKeyInfo chosenKey, bool isEncrypted)
        {
            string encryptedString = isEncrypted ? "encrypted " : "";
            string filePath;
            switch (chosenKey.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    Console.WriteLine($"\nSelected to read a {encryptedString}text file");
                    filePath = RequestFilePath();
                    await ReadTextAndWriteToConsole(filePath, isEncrypted);
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    Console.WriteLine("\nSelected to read a xml file");
                    filePath = RequestFilePath();
                    await ReadXmlAndWriteToConsole(filePath);
                    break;
                default:
                    Console.WriteLine("\nInput is not valid. Try again");
                    chosenKey = ShowFileTypeMenu();
                    isEncrypted = ShowEncryptedMenu();
                    await ProcessChoice(chosenKey, isEncrypted);
                    break;
            }
        }

        private static ConsoleKeyInfo ShowFileTypeMenu()
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

        private static async Task ReadTextAndWriteToConsole(string filePath, bool isEncrypted)
        {
            _nCoreTextReader ??= new NCoreTextReader();

            string content;
            if (isEncrypted)
            {
                content = await _nCoreTextReader.ReadEncryptedContent(filePath);
            }
            else
            {
                content = await _nCoreTextReader.ReadContent(filePath);
            }


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
