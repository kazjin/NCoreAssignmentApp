namespace NCoreAssignmentApp.Readers
{
    internal interface IReader
    {
        Task<string> ReadContent(string filePath);
        Task<string> ReadEncryptedContent(string filePath);
    }
}
