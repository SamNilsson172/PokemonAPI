using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace PokemonAPI.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : ControllerBase
    {

        Pokemon[] AllPokemon = new Pokemon[2];

        [HttpGet]
        public string Get() //gets latest pokemon
        {
            Move[] bdd = new Move[2];
            bdd[0] = new Move();
            bdd[0].Create("Tackle", 10, 0, 40, 1, 0);
            bdd[1] = new Move();
            bdd[1].Create("Murder", 1000000, 1, 1, 1, 0);
            AllPokemon[0] = new Pokemon();
            AllPokemon[0].Create("BigDaddy", 1000, 10, 10, 1, bdd);

            Move[] bt = new Move[1];
            bt[0] = new Move();
            bt[0].Create("Tackle", 10, 0, 40, 1, 0);
            AllPokemon[1] = new Pokemon();
            AllPokemon[1].Create("Beta", 100, 10, 10, 1, bt);

            return SerializePokemon(AllPokemon);
        }

        XmlSerializer pokemonSerializer = new XmlSerializer(typeof(Pokemon[]));

        string SerializePokemon(Pokemon[] p) //makes string from pokemon
        {
            using (MemoryStream ms = new MemoryStream()) //https://sites.google.com/view/csharp-referens/filhantering/serialisering?authuser=0
            {
                pokemonSerializer.Serialize(ms, p);
                return Encoding.ASCII.GetString(ms.ToArray()); //https://stackoverflow.com/questions/3542237/quick-way-to-get-the-contents-of-a-memorystream-as-an-ascii-string
            }
        }
    }
}