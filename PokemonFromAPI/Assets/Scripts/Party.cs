using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party
{
    //do static method here for checking array lenth , pokemon != null
    public static List<PartyPokemon> playerParty = new List<PartyPokemon>();
    public static List<PartyPokemon> opponentParty = new List<PartyPokemon>();


    public static void TestPokemans()
    {
        playerParty.Add(new PartyPokemon(1, 1, AllPokemon.pokemon[0]));
        opponentParty.Add(new PartyPokemon(1, 1, AllPokemon.pokemon[1]));
    }
}
