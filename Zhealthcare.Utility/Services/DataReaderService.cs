using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace Exxat.Common.Components;

public static class DataReaderService
{
    public static List<T> LoadJsonDataFromFile<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        List<T> jsonData = JsonConvert.DeserializeObject<List<T>>(json);
        return jsonData.Where(a => a != null).ToList();
    }
    public static Dictionary<string, T> LoadJsonPathDataFromFolder<T>(string folderPath)
    {

        Dictionary<string, T> jsonData = new();
        if (Directory.Exists(folderPath))
        {
            var files = Directory.GetFiles(folderPath, @"*.json", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                using var reader = new StreamReader(file);
                var json = reader.ReadToEnd();
                jsonData.Add(file, JsonConvert.DeserializeObject<T>(json));
            }
        }
        return jsonData;
    }

    public static List<string> ReadAllFiles(string folderPath)
    {
        List<string> nonJsonFilePaths = new();
        if (Directory.Exists(folderPath))
        {
            var files = Directory.GetFiles(folderPath, @"*.*", SearchOption.AllDirectories);
            files = files.Where(x => !x.ToLower().EndsWith("readme.md") && !x.Contains("\\.cd\\") && !x.EndsWith("\\.git")).ToArray();
            foreach (var file in files)
            {
                using var reader = new StreamReader(file);
                var json = reader.ReadToEnd();
                if (!IsJsonValid(json))
                    nonJsonFilePaths.Add(file);
            }
        }
        return nonJsonFilePaths;
    }

    public static bool IsJsonValid(string jsonString)
    {
        try
        {
            var value = JsonValue.Parse(jsonString);
            return value != null;
        }
        catch (FormatException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }


    public static List<string> GetAllJsonFilesFromFolder(params string[] paths)
    {
        string folderPath = Path.Combine(paths);
        List<string> jsonData = new List<string>();
        if (Directory.Exists(folderPath))
        {
            var files = Directory.GetFiles(folderPath, @"*.json", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                using var reader = new StreamReader(file);
                var json = reader.ReadToEnd();
                jsonData.Add(json);
            }
        }
        return jsonData.Where(a => !string.IsNullOrEmpty(a)).ToList();
    }
}