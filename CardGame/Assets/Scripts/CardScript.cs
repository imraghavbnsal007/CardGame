using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    // card value
    public int cardValue = 0;

    public int GetCardValue()
    {

        return cardValue;
    }

    public void SetCardValue(int newCardValue)
    {
        cardValue = newCardValue;
    }

    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void ResetCard()
    {
        Sprite backOfCard = GameObject.Find("Deck_Blackjack").GetComponent<Deck_Blackjack>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = backOfCard;
        cardValue = 0;

    }
}
