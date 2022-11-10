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
}
