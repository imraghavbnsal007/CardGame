using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject slot1;
    private Solitaire solitaire;
    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindObjectOfType<Solitaire>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }
    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Deck"))
                {
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Top"))
                {
                    Top();
                }
                else if (hit.collider.CompareTag("Bottom"))
                {
                    Bottom();
                }
            }
        }
    }

    void Deck()
    {
        print("Clicked on decked");
        solitaire.DealFromDeck();

    }

    void Card(GameObject selected)
    {
        print("Clicked on Card");

        //if the card clicked on is facedown
        //if the card clicked on is not blocked
        //flip it over

        //if the card clicked on is in the deck pile with the trips
        //if it is not blocked
        //select it

        //if the card is faceUp
        //if there is no card currently selected
        //select the card

        if (slot1 == this.gameObject) // not null because we pass it instead
        {
            slot1 = selected;
        }
        //if there is already a card is selected(and it is not the same card)
        else if (slot1 != selected)
        {
            //if(the new card is eligible to stack on the old card
            if(Stackable(selected))
            {
                //stack it
            }
            else
            {
                slot1 = selected;
                
            }
            
            //else
            //select the new card
            
        }


        //else if there is already a card selected and it is the same card
        // if the time is short enough then it is a double click
        //if the card is eligible to fly up top then do it


    }

    void Top()
    {
        print("Clicked on Top");
    }

    void Bottom()
    {
        print("Clicked on Bottom");
    }

    bool Stackable(GameObject selected)
    {
        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();

        //compare them if they are stack
        // return false;
        // ifin the top pile must stack 
        if (s2.top)
        {
            if (s1.suit == s2.suit || (s1.value == 1 && s2.suit == null))
            {
                if(s1.value == s2.value + 1)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        else
        {
            if (s1.value == s2.value - 1)
            {
                bool card1Red = true;
                bool card2Red = true;

                if(s1.suit == "C" || s1.suit == "S")
                {
                    card1Red = false;
                }
                if (s2.suit == "C" || s2.suit == "S")
                {
                    card2Red = false;
                }
                if (card1Red == card2Red)
                {
                    print("not stackable");
                    return false;
                }
                else
                {
                    print("stacked");
                    return true;
                }
            }

        }
        return false;
    }
}
