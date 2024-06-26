using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Mediator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QAction_5;
using Skyline.DataMiner.Scripting;

public static class Utils
{
    public static T XmlDeserializeFromString<T>(string xmlTextData)
    {
        using (TextReader reader = new StringReader(xmlTextData))
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(reader);
        }
    }

    public static T XmlDeserializeFromFile<T>(string path)
    {
        var text = ReadFile(path);
        return (T)XmlDeserializeFromString<T>(text);
    }

    public static T JsonDeserializeFromFile<T>(string path)
    {
        var text = ReadFile(path);
        return (T)JsonConvert.DeserializeObject<T>(text);
    }

    public static T JsonDeserializeFromFile<T>(string path, JsonSerializerSettings settings = null)
    {
        var text = ReadFile(path);
        return (T)JsonConvert.DeserializeObject<T>(text, settings);
    }

    public static string ReadFile(string path)
    {
        try
        {
            using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
            {
                return streamReader.ReadToEnd();
            }
        }
        catch (IOException ex)
        {
            throw new ApplicationException($"Error reading file {path}: {ex.Message}", ex);
        }
    }

    public static TValue GetValueOrDefault<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue defaultValue)
    {
        if(key == null)
        {
            return defaultValue;
        }
        return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    }
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
       => self.Select((item, index) => (item, index));

}
