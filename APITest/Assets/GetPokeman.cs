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
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        XmlSerializer pokemonSerializer = new XmlSerializer(typeof(Pokemon)); 
        Pokemon pokeman;

        string pokemonSerialized;
        using (WebClient client = new WebClient()) //https://stackoverflow.com/questions/1048199/easiest-way-to-read-from-a-url-into-a-string-in-net
        {
            pokemonSerialized = client.DownloadString("https://localhost:44307/api/pokemon");
        }

        using (Stream fs = GenerateStreamFromString(pokemonSerialized)) //https://sites.google.com/view/csharp-referens/filhantering/serialisering?authuser=0
        {
            pokeman = (Pokemon)pokemonSerializer.Deserialize(fs);
        }

        print(pokeman.atk);

        text.text = pokeman.name;
    }

    public static Stream GenerateStreamFromString(string s) //https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}
