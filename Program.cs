using NCoreAssignmentApp.Authenication;
using NCoreAssignmentApp.Authentication;
using NCoreAssignmentApp.Readers;
using NCoreAssignmentApp.Readers.EncryptionEnum;
using System.Text;
using static NCoreAssignmentApp.Readers.NCoreXmlReader;

namespace NCoreAssignmentApp
{
    internal class Program
    {
        private static NCoreTextReader? _nCoreTextReader;
        private static AuthenticationService? _authenticationService;
        private static NCoreXmlReader? _nCoreXmlReader;
        private static NCoreJsonReader? _nCoreJsonReader;

        static async Task Main()
        {
            Console.WriteLine("\nHello, welcome to NCoreAssignmentApp!\n");

            var chosenFileTypeKey = ShowFileTypeMenu();
            var isJsonFile = chosenFileTypeKey.Key == ConsoleKey.NumPad3 || chosenFileTypeKey.Key == ConsoleKey.D3;
            var isEncrypted = IsEncryptedMenu(skip: isJsonFile);
            EncryptionType selectedEncryption = EncryptionType.None;

            if (isEncrypted)
            {
                selectedEncryption = SelectEncryptionMenu();
            }

            await ProcessChoice(chosenFileTypeKey, selectedEncryption);

            await RebootApplication();
        }

        private static async Task RebootApplication()
        {
            await Main();
        }

        private static EncryptionType SelectEncryptionMenu()
        {
            Console.WriteLine("\nChoose the encryption you want to use:");
            Console.WriteLine($"1. {EncryptionType.Reverse}");
            Console.WriteLine($"2. {EncryptionType.Zero}");

            var choiceKey = Console.ReadKey();

            switch (choiceKey.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    return EncryptionType.Reverse;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    return EncryptionType.Zero;
                default:
                    Console.WriteLine("\nInput is not valid. Try again");
                    SelectEncryptionMenu();
                    break;
            }

            return EncryptionType.None;
        }

        private static bool IsEncryptedMenu(bool skip)
        {
            if (skip) return false;

            Console.WriteLine("\nIs the file encrypted? Type Y or N");
            Console.WriteLine("Y: yes");
            Console.WriteLine("N: no");
            var choiceKey = Console.ReadKey();

            return choiceKey.Key == ConsoleKey.Y;
        }

        private static async Task ProcessChoice(ConsoleKeyInfo chosenKey, EncryptionType encryptionType)
        {
            string encryptedString = encryptionType != EncryptionType.None ? "encrypted " : "";
            string filePath;
            switch (chosenKey.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    Console.WriteLine($"\nSelected to read a {encryptedString}text file");

                    HandleRoleSelection(encryptionType);

                    filePath = RequestTextFilePath();
                    await ReadTextAndWriteToConsole(filePath, encryptionType);
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    Console.WriteLine("\nSelected to read a xml file");

                    HandleRoleSelection(encryptionType);

                    filePath = RequestXmlFilePath();
                    await ReadXmlAndWriteToConsole(filePath, encryptionType);
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    Console.WriteLine("\nSelected to read a json file");

                    filePath = RequestJsonFilePath();
                    await ReadJsonAndWriteToConsole(filePath);
                    break;
                default:
                    Console.WriteLine("\nInput is not valid. Try again");
                    chosenKey = ShowFileTypeMenu();
                    await ProcessChoice(chosenKey, encryptionType);
                    break;
            }
        }

        private static void HandleRoleSelection(EncryptionType encryptionType)
        {
            _authenticationService ??= new AuthenticationService();

            var role = SelectRoleMenu();
            var useRealImplementation = RealRolesMenu();
            var isEncrypted = encryptionType != EncryptionType.None;
            var canRead = _authenticationService.CanReadFile(role, isEncrypted, useRealImplementation);
            while (!canRead)
            {
                Console.WriteLine($"\nWARNING: The role {role} does not have permission to read the file. Select another role:");

                role = SelectRoleMenu();
                useRealImplementation = RealRolesMenu();
                canRead = _authenticationService.CanReadFile(role, isEncrypted, useRealImplementation);
            }
        }

        private static bool RealRolesMenu()
        {
            Console.WriteLine("\nDo you want to use a real implementation?:");

            Console.WriteLine("Y: yes");
            Console.WriteLine("N: no");
            var choiceKey = Console.ReadKey();

            return choiceKey.Key == ConsoleKey.Y;
        }

        private static RoleType SelectRoleMenu()
        {
            Console.WriteLine("\nSelect the role you want to use:");

            Console.WriteLine($"1. {RoleType.User} (can read normal not encrypted files)");
            Console.WriteLine($"2. {RoleType.Manager} (can read also encrypted files)");
            Console.WriteLine($"3. {RoleType.Administration} (can read all)");
            var choiceKey = Console.ReadKey();

            switch (choiceKey.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    return RoleType.User;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    return RoleType.Manager;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    return RoleType.Administration;
                case ConsoleKey.NumPad4:
                default:
                    Console.WriteLine("\nInput is not valid. Try again");
                    SelectRoleMenu();
                    break;
            }

            return RoleType.User;
        }

        private static ConsoleKeyInfo ShowFileTypeMenu()
        {
            Console.WriteLine("Choose which file type to read:");
            Console.WriteLine("1. text file");
            Console.WriteLine("2. xml file");
            Console.WriteLine("3. json file");
            var choiceKey = Console.ReadKey();
            return choiceKey;
        }

        private static string RequestTextFilePath()
        {
            var filePath = string.Empty;

            while (string.IsNullOrEmpty(filePath) || !filePath.Contains(".txt"))
            {
                Console.WriteLine("\nInsert in the next line the full path of the text file:");
                filePath = Console.ReadLine();
            }

            return filePath;
        }

        private static string RequestXmlFilePath()
        {
            var filePath = string.Empty;

            while (string.IsNullOrEmpty(filePath) || !filePath.Contains(".xml"))
            {
                Console.WriteLine("\nInsert in the next line the full path of the xml file:");
                filePath = Console.ReadLine();
            }

            return filePath;
        }

        private static string RequestJsonFilePath()
        {
            var filePath = string.Empty;

            while (string.IsNullOrEmpty(filePath) || !filePath.Contains(".json"))
            {
                Console.WriteLine("\nInsert in the next line the full path of the json file:");
                filePath = Console.ReadLine();
            }

            return filePath;
        }

        private static async Task ReadTextAndWriteToConsole(string filePath, EncryptionType encryptionType)
        {
            _nCoreTextReader ??= new NCoreTextReader();

            string content;
            if (encryptionType == EncryptionType.None)
            {
                content = await _nCoreTextReader.ReadContent(filePath);
            }
            else
            {
                content = await _nCoreTextReader.ReadEncryptedContent(filePath, encryptionType);
            }


            Console.WriteLine("The contents of the file:");
            Console.WriteLine(content);
        }

        private static async Task ReadXmlAndWriteToConsole(string filePath, EncryptionType encryptionType)
        {
            _nCoreXmlReader ??= new NCoreXmlReader();

            XmlReaderModel xmContentTuple;
            if (encryptionType == EncryptionType.None)
            {
                xmContentTuple = await _nCoreXmlReader.ReadContent(filePath);
            }
            else
            {
                xmContentTuple = await _nCoreXmlReader.ReadEncryptedContent(filePath, encryptionType);
            }

            OutputReadableXmlContent(xmContentTuple);
        }

        private static async Task ReadJsonAndWriteToConsole(string filePath)
        {
            _nCoreJsonReader ??= new NCoreJsonReader();

            var result = await _nCoreJsonReader.ReadContent(filePath);

            Console.WriteLine("{0}", result);
        }

        private static void OutputReadableXmlContent(XmlReaderModel xmContentTuple)
        {
            Console.WriteLine("The contents of the file:");

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < xmContentTuple.XmlValues.Count; i++)
            {
                builder.Append($"{xmContentTuple.XmlTags[i]}: {xmContentTuple.XmlValues[i]}");
            }

            Console.WriteLine(builder.ToString());
        }
    }
}
