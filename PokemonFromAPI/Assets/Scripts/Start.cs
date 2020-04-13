using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using System.IO;
using System.Net;

public class Start : MonoBehaviour
{
    XmlSerializer pokemonSerializer = new XmlSerializer(typeof(Pokemon[]));
    public void Play() //get all pokemon from API server
    {
        string pokemonSerialized;
        using (WebClient client = new WebClient()) //https://stackoverflow.com/questions/1048199/easiest-way-to-read-from-a-url-into-a-string-in-net //finds pokemon via api
        {
            pokemonSerialized = client.DownloadString("https://localhost:44307/api/pokemon");
        }
        AllPokemon.pokemon = DeserializePokemon(pokemonSerialized); //get all pokemon from string
        SceneManager.LoadScene(2, LoadSceneMode.Single); //load game, won't load if server's not running
    }

    Pokemon[] DeserializePokemon(string s) //makes pokemon from string
    {
        using (Stream fs = GenerateStreamFromString(s)) //https://sites.google.com/view/csharp-referens/filhantering/serialisering?authuser=0
        {
            return (Pokemon[])pokemonSerializer.Deserialize(fs);
        }
    }

    Stream GenerateStreamFromString(string s) //https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string //make memorystream from string
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}
