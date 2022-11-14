using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{

    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentID = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    void GetCardValues()
    {
        int number = 0;

        for (int i = 0; i<cardSprites.Length; i++)
        {
            number = i;
            number %= 13;
            if(number>10 || number == 0)
            {
                number = 10;
            }
            cardValues[i] = number++;
        }
        currentID = 1;
    }


    public void ShuffleCards()
    {
        // shuffle cards
        for(int i = cardSprites.Length - 1; i > 0; --i)  // count backwards
        {
            int k = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[k];
            cardSprites[k] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[k];
            cardValues[k] = value;

        }
    }

    public int DealCard(CardScript cardScript)
    {

        return 1;
    }
}

