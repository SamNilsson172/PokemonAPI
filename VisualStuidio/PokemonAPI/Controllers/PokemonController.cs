using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace PokemonAPI.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {

        static XmlSerializer pokemonSerializer = new XmlSerializer(typeof(Pokemon));
        static List<Pokemon> AllPokemon = new List<Pokemon>();

        static List<string> strings = new List<string>();

        [HttpGet]
        public string GetLatest() //gets latest pokemon, raplace with get all pokemon later
        {
            AllPokemon.Add(new Pokemon()
            {
                name = "BigBoy",
                hp = 5,
                atk = 5,
                def = 5,
                type = 1
            }); ;

            return SerializePokemon(AllPokemon[AllPokemon.Count - 1]);
        }

        string SerializePokemon(Pokemon p) //makes string from pokemon
        {
            using (Stream fs = new FileStream("pokeman.xml", FileMode.OpenOrCreate)) //https://sites.google.com/view/csharp-referens/filhantering/serialisering?authuser=0
            {
                pokemonSerializer.Serialize(fs, p);
            }
            return System.IO.File.ReadAllText("pokeman.xml");
        }

        Pokemon DeserializePokemon(string s) //makes pokemon from string
        {
            using (Stream fs = GenerateStreamFromString(s)) //https://sites.google.com/view/csharp-referens/filhantering/serialisering?authuser=0
            {
                return (Pokemon)pokemonSerializer.Deserialize(fs);
            }
        }

        public static Stream GenerateStreamFromString(string s) //https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string //makes filestream from stringS
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}