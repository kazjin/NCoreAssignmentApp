namespace NCoreAssignmentApp.Readers
{
    internal interface IReader
    {
        Task<string> ReadContent(string filePath);
    }
}
