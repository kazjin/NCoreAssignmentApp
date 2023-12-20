using Newtonsoft.Json;
using System.IO;

namespace NCoreAssignmentApp.Readers
{
    public class NCoreJsonReader
    {
        public NCoreJsonReader() { }

        public async Task<string> ReadContent(string filePath)
        {
            using StreamReader r = new(filePath);
            string json = r.ReadToEnd();
            var array = JsonConvert.DeserializeObject(json);

            return JsonConvert.SerializeObject(array);
        }
    }
}
