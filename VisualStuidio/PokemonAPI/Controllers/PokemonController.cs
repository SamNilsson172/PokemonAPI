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
            Move[] bulb = new Move[2];
            bulb[0] = new Move();
            bulb[0].Create("Tackle", 10, 0, 40, 0, 0);
            bulb[1] = new Move();
            bulb[1].Create("VineWhip", 20, 0, 15, 1, 0);
            byte[] imgBack = System.IO.File.ReadAllBytes("C:\\Users\\sam.nilsson\\Desktop\\Skolarbete\\PokemonAPI\\Images\\BulbasaurBack.png"); //https://stackoverflow.com/questions/8084590/how-to-load-image-from-sql-server-into-picture-box
            byte[] imgFront = System.IO.File.ReadAllBytes("C:\\Users\\sam.nilsson\\Desktop\\Skolarbete\\PokemonAPI\\Images\\BulbasaurFront.png");
            AllPokemon[0] = new Pokemon();
            AllPokemon[0].Create("Bulbasaur", 50, 10, 10, 1, bulb, imgBack, imgFront);

            Move[] charm = new Move[2];
            charm[0] = new Move();
            charm[0].Create("Tackle", 10, 0, 40, 1, 0);
            charm[1] = new Move();
            charm[1].Create("Ember", 20, 0, 15, 2, 0);
            imgBack = System.IO.File.ReadAllBytes("C:\\Users\\sam.nilsson\\Desktop\\Skolarbete\\PokemonAPI\\Images\\CharmanderBack.png");
            imgFront = System.IO.File.ReadAllBytes("C:\\Users\\sam.nilsson\\Desktop\\Skolarbete\\PokemonAPI\\Images\\CharmanderFront.png");
            AllPokemon[1] = new Pokemon();
            AllPokemon[1].Create("Charmander", 50, 10, 10, 1, charm, imgBack, imgFront);

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