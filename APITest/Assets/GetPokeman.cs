using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class GetPokeman : MonoBehaviour
{
    static XmlSerializer pokemonSerializer = new XmlSerializer(typeof(Pokemon));
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        Pokemon pokeman;

        string pokemonSerialized;
        using (WebClient client = new WebClient()) //https://stackoverflow.com/questions/1048199/easiest-way-to-read-from-a-url-into-a-string-in-net //finds pokemon via api
        {
            pokemonSerialized = client.DownloadString("https://localhost:44307/api/pokemon");
        }
        
        pokeman = DeserializePokemon(pokemonSerialized);
        

        print(pokeman.atk);
        text.text = pokeman.name;
    }

    public static string SerializePokemon(Pokemon p)
    {
        using (Stream fs = new FileStream("pokeman.xml", FileMode.OpenOrCreate)) //https://sites.google.com/view/csharp-referens/filhantering/serialisering?authuser=0
        {
            pokemonSerializer.Serialize(fs, p);
        }
        return System.IO.File.ReadAllText("pokeman.xml");
    }

    public static Pokemon DeserializePokemon(string s) //makes pokemon from string
    {
        using (Stream fs = GenerateStreamFromString(s)) //https://sites.google.com/view/csharp-referens/filhantering/serialisering?authuser=0
        {
            return (Pokemon)pokemonSerializer.Deserialize(fs);
        }
    }

    public static Stream GenerateStreamFromString(string s) //https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string //make filestream from string
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}
