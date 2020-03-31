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

        XmlSerializer pokemonSerializer = new XmlSerializer(typeof(List<Pokemon>));
        List<Pokemon> AllPokemon = new List<Pokemon>();

        [HttpGet]
        public string Get() //gets latest pokemon, raplace with get all pokemon later
        {
            AllPokemon.Add(new Pokemon()
            {
                name = "BigBoy",
                hp = 5,
                atk = 5,
                def = 5,
                type = 1
            }); ;

            return SerializePokemon(AllPokemon);
        }

        string SerializePokemon(List<Pokemon> p) //makes string from pokemon
        {
            using (Stream fs = new FileStream("pokeman.xml", FileMode.OpenOrCreate)) //https://sites.google.com/view/csharp-referens/filhantering/serialisering?authuser=0
            {
                pokemonSerializer.Serialize(fs, p);
            }
            return System.IO.File.ReadAllText("pokeman.xml");
        }
    }
}