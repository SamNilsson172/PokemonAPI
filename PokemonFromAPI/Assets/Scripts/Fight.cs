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
        state = 5;
    }

    void FixedUpdate()
    {
        {
            switch (state)
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
                    state = (int)State.wait;
                    break;

                case (int)State.end:
                    End();
                    break;
            }

        }
    }

    float currentTime;
    float waitTime;
    bool waitForInput;
    void Wait() //wait for specified amount of time
    {
        if (!waitForInput)
        {
            UIWaitDisplay(true); //show waiting UI
            currentTime += Time.deltaTime;
            if (currentTime >= waitTime)
            {
                state = nextState;
                currentTime = 0;
                if (nextState == (int)State.wait)
                    waitForInput = true;
            }

        }
        else //waiting for input
        {
            UIWaitDisplay(false); //dont show waiting UI while waiting for input
            if (nextState != (int)State.wait) //player has inputed
            {
                waitForInput = false;
                state = nextState;
            }
        }
    }

    bool myTurn = true;
    public int atkIndex;
    void Atk()
    {
        Debug.Log("Bruh");
        //do speed comparison and use other bool for attacking
        if (myTurn)
        {
            playerPokemon.Attack(atkIndex, opponentPokemon);
            nextState = (int)State.atk;
            eventText.text = "Player attacked";
        }
        if (!myTurn)
        {
            opponentPokemon.Attack(Random.Range(0, opponentPokemon.moves.Length), playerPokemon);
            nextState = (int)State.wait;
            eventText.text = "Opponent attacked";
        }
        myTurn = !myTurn;
        waitTime = 1;
        state = (int)State.check; //wait is always run after check
    }

    void Run()
    {
        int random = Random.Range(1, 25);
        int lvlDifference = playerPokemon.lvl - opponentPokemon.lvl;
        if (random + lvlDifference > 10)
        {
            state = (int)State.wait;
            waitTime = 1;
            eventText.text = "You ran";
            nextState = (int)State.end;
        }
        else eventText.text = "failed to run";
    }

    PartyPokemon playerPokemon;
    PartyPokemon opponentPokemon;
    void Check()
    {
        state = (int)State.wait;
        float totOpponentHp = 0;
        foreach (PartyPokemon p in Party.opponentParty)
        {
            totOpponentHp += p.CurrentHp;
            if (p.CurrentHp > 0)
            {
                opponentPokemon = p;
            }
        }
        if (totOpponentHp <= 0)
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
        updateUI.UIUpdate(playerPokemon, opponentPokemon);
    }

    void End()
    {
        Destroy(gameObject);
    }

    void UIWaitDisplay(bool waiting)
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
}
