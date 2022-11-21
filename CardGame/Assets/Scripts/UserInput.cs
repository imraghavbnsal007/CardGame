using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UserInput : MonoBehaviour
{
    public GameObject slot1;
    private Solitaire solitaire;
    private float timer;
    private float doubleClickTime = 0.3f;

    private int clickCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindObjectOfType<Solitaire>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickCount == 1)
        {
            timer += Time.deltaTime;
        }
        if (clickCount == 3)
        {
            timer = 0;
            clickCount = 1;
        }

        if (timer > doubleClickTime)
        {
            timer = 0;
            clickCount = 0;
        }
        GetMouseClick();
    }
    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // if the left mouse button is clicked
        {
            clickCount++;
            // we save the mouse position in world units
            // the z position is the distance from the camera
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            // depending on what has been clicked, we call the appropriate functions
            if (hit)
            {
                if (hit.collider.CompareTag("Deck"))
                {
                    SFX.sfxInstance.audioSource.PlayOneShot(SFX.sfxInstance.flip);
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    SFX.sfxInstance.audioSource.PlayOneShot(SFX.sfxInstance.flip);
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Top"))
                {
                    SFX.sfxInstance.audioSource.PlayOneShot(SFX.sfxInstance.thump);
                    Top(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Bottom"))
                {
                    SFX.sfxInstance.audioSource.PlayOneShot(SFX.sfxInstance.thump);
                    Bottom(hit.collider.gameObject);
                }
            }
        }
    }

    void Deck()
    {
        // print("Clicked on decked");
        solitaire.DealFromDeck();
        slot1 = this.gameObject;

    }

    void Card(GameObject selected)
    {
        // print("Clicked on Card");
        if (!selected.GetComponent<Selectable>().faceUp)//if the card clicked on is facedown
        {
            if (!Blocked(selected)) //if the card clicked on is not blocked
            {
                //flip it over
                selected.GetComponent<Selectable>().faceUp = true;
                slot1 = this.gameObject;
            }
        }
        else if (selected.GetComponent<Selectable>().inDeckPile)
        {
            if (!Blocked(selected))
            {
                if (slot1 == selected)// if the same card clicked twice
                {
                    if (DoubleClick())
                    {
                        //atempt auto stack
                        AutoStack(selected);

                    }

                }
                else
                {
                    slot1 = selected;

                }
            }
        }
        else
        {
            if (slot1 == this.gameObject) // not null because we pass it instead
            {
                slot1 = selected;
            }
            //if there is already a card is selected(and it is not the same card)
            else if (slot1 != selected)
            {
                //if(the new card is eligible to stack on the old card
                if (Stackable(selected))
                {
                    //stack it
                    Stack(selected);
                }
                else
                {
                    slot1 = selected;

                }

                //else
                //select the new card


            }
            else if (slot1 == selected)
            {
                if (DoubleClick())
                {
                    AutoStack(selected);
                    //atempt auto stack
                }
            }
        }

        //if the card clicked on is not blocked
        //flip it over

        //if the card clicked on is in the deck pile with the trips
        //if it is not blocked
        //select it

        //if the card is faceUp
        //if there is no card currently selected
        //select the card




        //else if there is already a card selected and it is the same card
        // if the time is short enough then it is a double click
        //if the card is eligible to fly up top then do it


    }

    void Top(GameObject selected)
    {
        print("Clicked on Top");
        if (slot1.CompareTag("Card"))
        {
            if (slot1.GetComponent<Selectable>().value == 1)
            {
                Stack(selected);
            }
        }
    }

    void Bottom(GameObject selected)
    {
        print("Clicked on Bottom");
        // if the card is king and the empty slot is bottom then stack
        if (slot1.CompareTag("Card"))
        {
            if (slot1.GetComponent<Selectable>().value == 13)
            {
                Stack(selected);
            }
        }
    }

    bool Stackable(GameObject selected)
    {
        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();

        //compare them if they are stack
        // return false;
        // ifin the top pile must stack 
        if (!s2.inDeckPile)
        {
            if (s2.top)
            {
                if (s1.suit == s2.suit || (s1.value == 1 && s2.suit == null))
                {
                    if (s1.value == s2.value + 1)
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

                    if (s1.suit == "C" || s1.suit == "S")
                    {
                        card1Red = false;
                    }
                    if (s2.suit == "C" || s2.suit == "S")
                    {
                        card2Red = false;
                    }
                    if (card1Red == card2Red)
                    {
                        // print("not stackable");
                        return false;
                    }
                    else
                    {
                        // print("stacked");
                        return true;
                    }
                }

            }

        }

        return false;
    }
    void Stack(GameObject selected)
    {
        //if on top of king or empty bottom stack the cards in place
        //else stack the cards with a negativiy y offset

        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();
        float yOffset = 0.5f;
        if (s2.top || !s2.top && s1.value == 13)
        {
            yOffset = 0;
        }
        slot1.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - yOffset, selected.transform.position.z - 0.01f);
        slot1.transform.parent = selected.transform; //this makes children move with the parents

        if (s1.inDeckPile) //remove the cards from the tops pile to prevent duplicate card
        {
            solitaire.tripsOnDisplay.Remove(slot1.name);

        }
        else if (s1.top && s2.top && s1.value == 1) // allows movemnt to cards between top spots
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = 0;
            solitaire.topPos[s1.row].GetComponent<Selectable>().suit = null;
        }
        else if (s1.top) // keeps the track of the current value of the top decks as a cards has been removed
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = s1.value - 1;
        }
        else // removes the card string from the appropriate bottom list
        {
            solitaire.bottoms[s1.row].Remove(slot1.name);
        }

        s1.inDeckPile = false; // you cannot add cards to the trips pile so this is always fine
        s1.row = s2.row;

        if (s2.top)
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = s1.value;
            solitaire.topPos[s1.row].GetComponent<Selectable>().suit = s1.suit;
            s1.top = true;
        }
        else
        {
            s1.top = false;
        }

        //after completing move reset slot1 to be essentially null as being null will break the logic 
        slot1 = this.gameObject;
    }

    bool Blocked(GameObject selected)
    {
        Selectable s2 = selected.GetComponent<Selectable>();
        if (s2.inDeckPile == true)
        {
            if (s2.name == solitaire.tripsOnDisplay.Last())
            {
                return false;
            }
            else
            {
                print(s2.name + "is blocked by" + solitaire.tripsOnDisplay.Last());
                return true;
            }
        }
        else
        {
            if (s2.name == solitaire.bottoms[s2.row].Last()) // check if it is a bottom cards
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    bool DoubleClick()
    {
        if (timer < doubleClickTime && clickCount == 2)
        {
            print("Double click");
            return true;
        }
        else
        {
            return false;
        }


    }
    void AutoStack(GameObject selected)
    {
        for (int i = 0; i < solitaire.topPos.Length; i++)
        {
            Selectable stack = solitaire.topPos[i].GetComponent<Selectable>();
            if (selected.GetComponent<Selectable>().value == 1) // if it is an Ace
            {
                if (solitaire.topPos[i].GetComponent<Selectable>().value == 0) // and the top position is empty
                {
                    slot1 = selected;
                    Stack(stack.gameObject); // sstack tehe ace up top in the first empty position found
                    break;
                }
            }
            else
            {
                if ((solitaire.topPos[i].GetComponent<Selectable>().suit == slot1.GetComponent<Selectable>().suit) && (solitaire.topPos[i].GetComponent<Selectable>().value == slot1.GetComponent<Selectable>().value - 1))
                {
                    //if it is not carrying any additional card
                    if (HasNoChildren(slot1))
                    {
                        //if it has no children
                        slot1 = selected;
                        //find a top spot that matches the condition for auotstacking if it exists
                        string lastCardname = stack.suit + stack.value.ToString();
                        if (stack.value == 1)
                        {
                            lastCardname = stack.suit + "A";
                        }
                        if (stack.value == 11)
                        {
                            lastCardname = stack.suit + "J";
                        }
                        if (stack.value == 12)
                        {
                            lastCardname = stack.suit + "Q";
                        }
                        if (stack.value == 13)
                        {
                            lastCardname = stack.suit + "K";
                        }

                        GameObject lastCard = GameObject.Find(lastCardname);
                        Stack(lastCard);
                        break;

                    }

                }

            }
        }
    }

    bool HasNoChildren(GameObject card)
    {
        int i = 0;
        foreach (Transform child in card.transform)
        {
            i++;
        }
        if (i == 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
