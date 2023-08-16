using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Globalization;

namespace Zhealthcare.Service.Helper
{
    public static class FileReader
    {
        public static List<T> LoadCsvData<T>(params string[] paths)
        {
            var filePath = Path.Combine(paths);
            using var reader = new StreamReader(filePath);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Comment = '#',
                AllowComments = true,
                Delimiter = ",",
                BadDataFound = null
            };
            
            using var csv = new CsvReader(reader, csvConfig);
            csv.Context.TypeConverterOptionsCache.GetOptions<string>().NullValues.Add("");
            csv.Context.RegisterClassMap<PatientMap>();
            return csv.GetRecords<T>().ToList();
        }

        public static T LoadJsonData<T>(params string[] paths)
        {
            var filePath = Path.Combine(paths);
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
