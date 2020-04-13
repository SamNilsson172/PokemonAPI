using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    public Text eventText;
    public Button[] buttons;
    public UpdateUI updateUI;
    public enum State { wait, atk, bag, swap, run, check, end, newMove }
    int state;
    public int nextState;

    private void Start()
    {
        Party.TestPokemans();
        state = 5; //set state to check on start for ui and variables to be defined
    }

    void FixedUpdate()
    {
        {
            switch (state) //call methods depending on state, do nothing more than call methods in update for simplicity
            {
                case (int)State.wait:
                    Wait();
                    break;

                case (int)State.atk:
                    Atk();
                    break;

                //case (int)State.bag:
                //    Bag();
                //    break;

                //case (int)State.swap:
                //    Swap();
                //    break;

                case (int)State.run:
                    Run();
                    break;

                case (int)State.check:
                    Check();
                    break;

                case (int)State.end:
                    End();
                    break;
            }

        }
    }

    float currentTime; //time passed since wait method sarted to be called
    float waitTime; //time that has to be waited before next input can be inputed
    bool waitForInput; //if waiting for users input
    void Wait() //wait for specified amount of time
    {
        if (!waitForInput)
        {
            UIWaitDisplay(true); //show waiting UI
            currentTime += Time.deltaTime;
            if (currentTime >= waitTime) //done waiting
            {
                state = nextState; //go to next state
                currentTime = 0; //reset for next wait
                if (nextState == (int)State.wait) //if next state is wait the user needs to press a button to change that
                    waitForInput = true;
            }

        }
        else //waiting for input
        {
            UIWaitDisplay(false); //dont show waiting UI while waiting for input
            if (nextState != (int)State.wait) //user has inputed
            {
                waitForInput = false;
                state = nextState;
            }
        }
    }

    bool myTurn = true; //if player is attacking
    public int atkIndex; //what attack to use, changes from button
    void Atk()
    {
        if (myTurn)
        {
            playerPokemon.Attack(atkIndex, opponentPokemon, Effectivness(playerPokemon.moves[atkIndex].type, opponentPokemon.type));
            nextState = (int)State.atk; //player attacks first so let opponent attack after
            eventText.text = "Player attacked";
        }
        if (!myTurn)
        {
            int opponentMove = Random.Range(0, opponentPokemon.KnownMoves()); //use random move
            opponentPokemon.Attack(opponentMove, playerPokemon, Effectivness(opponentPokemon.moves[opponentMove].type, playerPokemon.type));
            nextState = (int)State.wait;
            eventText.text = "Opponent attacked";
        }
        myTurn = !myTurn; //swap turn after each atk
        waitTime = 1;
        state = (int)State.check; //wait is always run after check
    }

    void Run()
    {
        state = (int)State.wait;
        int random = Random.Range(0, 25);
        int lvlDifference = playerPokemon.lvl - opponentPokemon.lvl;
        if (random + lvlDifference > 10) //sum math for escaping
        {
            state = (int)State.wait;
            waitTime = 1;
            eventText.text = "You ran";
            nextState = (int)State.end; //if succesfull end fight
        }
        else
        {
            myTurn = false;
            eventText.text = "failed to run";
            nextState = (int)State.atk; //if not succesfull let opponent attack
        }
    }

    PartyPokemon playerPokemon; //variables for ease of use
    PartyPokemon opponentPokemon;
    void Check()
    {
        state = (int)State.wait;
        float totOpponentHp = 0;
        foreach (PartyPokemon p in Party.opponentParty) //see if opponent has any pokemon with hp left
        {
            totOpponentHp += p.CurrentHp;
            if (p.CurrentHp > 0) //if it dose set active pokemon to it
            {
                opponentPokemon = p;
            }
        }
        if (totOpponentHp <= 0) //if not end fight
        {
            waitTime = 1;
            eventText.text = "Player won";
            nextState = (int)State.end;
        }

        float totPlayerHp = 0;
        foreach (PartyPokemon p in Party.playerParty)
        {
            totPlayerHp += p.CurrentHp;
            if (p.CurrentHp > 0)
            {
                playerPokemon = p;
            }
        }
        if (totPlayerHp <= 0)
        {
            waitTime = 1;
            eventText.text = "Opponent won";
            nextState = (int)State.end;
        }
        updateUI.UIUpdate(playerPokemon, opponentPokemon); //update UI here because it feels logical
    }

    void End()
    {
        Destroy(gameObject);
    }

    void UIWaitDisplay(bool waiting) //what parts of the UI to display
    {
        if (waiting)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, 1f); //show text
            foreach (Button b in buttons) //no clickiing buttons
                b.interactable = false;
        }
        if (!waiting)
        {
            eventText.color = new Color(eventText.color.r, eventText.color.g, eventText.color.b, 0f); //don't show text
            foreach (Button b in buttons) //can click buttons
                b.interactable = true;
        }
    }

    enum Types { normal, grass, fire, water }
    float Effectivness(int atk, int def)
    {
        float effectivness = 1;
        switch (atk)
        {
            case (int)Types.normal:
                effectivness = 1;
                break;

            case (int)Types.grass:
                if (def == (int)Types.water) //super effective
                    effectivness = 1.5f;
                if (def == (int)Types.normal || def == (int)Types.grass) //normal effective
                    effectivness = 1;
                if (def == (int)Types.fire) //not very effective
                    effectivness = .5f;
                break;

            case (int)Types.fire:
                if (def == (int)Types.grass) //super effective
                    effectivness = 1.5f;
                if (def == (int)Types.normal || def == (int)Types.fire) //normal effective
                    effectivness = 1;
                if (def == (int)Types.water) //not very effective
                    effectivness = .5f;
                break;

            case (int)Types.water:
                if (def == (int)Types.fire) //super effective
                    effectivness = 1.5f;
                if (def == (int)Types.normal || def == (int)Types.water) //normal effective
                    effectivness = 1;
                if (def == (int)Types.grass) //not very effective
                    effectivness = .5f;
                break;
        }
        return effectivness;
    }
}
