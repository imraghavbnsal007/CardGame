using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// keep track of cards for player and dealer

public class PlayingScript : MonoBehaviour
{

    public CardScript cardScript;
    public Deck_Blackjack deckScript;

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
        CheckAce();
        cardID++;

        return totalHandValue;
    }

    public void CheckAce()
    {
        foreach(CardScript ace in aceList)
        {
            if (totalHandValue + 10 < 22 & ace.GetCardValue() == 1)
            {
                ace.SetCardValue(11);
                totalHandValue += 10;

            }

            else if (totalHandValue > 21 && ace.GetCardValue() == 11)
            {
                ace.SetCardValue(1);
                totalHandValue -= 10;

            }
            
        }
    }

    public void AdjustMoney(int amount)
    {
        bettingMoney += amount;
    }

    public int GetMoney()
    {
        return bettingMoney;
    }

    //hides all cards, resets the needed variables
    public void ResetHand()
    {
        for(int i=0; i<hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardID=0;
        totalHandValue=0;
        aceList= new List<CardScript>();
    }


}

