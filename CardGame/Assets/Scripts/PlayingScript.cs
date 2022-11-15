using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// keep track of cards for player and dealer

public class PlayingScript : MonoBehaviour
{

    public CardScript cardScript;
    public Deck deckScript;

    public int totalHandValue = 0;

    // money to bet
    private int bettingMoney = 1000;

    // card objects on the table
    public GameObject[] hand;
    // next card to be turned over
    public int cardID = 0;

    List<CardScript> aceList = new List<CardScript>();

    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    // deal cards to the player or dealer
    public int GetCard()
    {
        int valueCard = deckScript.DealCard(hand[cardID].GetComponent<CardScript>());
        hand[cardID].GetComponent<Renderer>().enabled = true;
        totalHandValue += valueCard;
        // check if ace
        if (valueCard == 1)
        {
            aceList.Add(hand[cardID].GetComponent<CardScript>());
        }
        //AceCheck();
        cardID++;

        return totalHandValue;
    }
}