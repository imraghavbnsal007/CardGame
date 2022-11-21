using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Solitaire : MonoBehaviour
{

    public Sprite[] cardFaces;      // Holds the card Sprites
    public GameObject cardPrefab;   // Holds the card game object
    public GameObject deckButton;

    // positions cards correctly when they are dealt out
    public GameObject[] bottomPos;
    public GameObject[] topPos;
    
    // card suits and values
    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    
    // holds lists of cards within top and bottom positions
    public List<string>[] bottoms;
    public List<string>[] tops;

    // Lists to hold cards at the bottom positions
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();
    private List<string> bottom7 = new List<string>();

    public List<string> tripsOnDisplay = new List<string>();        // cards from the deck on display
    public List<List<string>> deckTrips = new List<List<string>>(); // list of all deck groups in chunks of up to three cards
    public List<string> deck;

    public List<string> discardPile = new List<string>();
    private int deckLocation;
    
    private int trips;          // the amount of triples the  deck can be sorted into
    private int tripsRemainder; // the remained after that sorting

    // Start is called before the first frame update
    void Start()
    {
        bottoms = new List<string>[] { bottom1, bottom2, bottom3, bottom4, bottom5, bottom6, bottom7 };
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /********************************************************/

    /*
        Handles the setup for the game
    */
    public void PlayCards()
    {
        foreach (List<string> list in bottoms)
        {
            list.Clear();
            
        }
        deck = GenerateDeck();  // generates strings with cardfaces
        Shuffle(deck);          // randomises the deck's order
        SFX.sfxInstance.audioSource.PlayOneShot(SFX.sfxInstance.shuffle);
        SolitaireSort();                    // Sorts cards into their positions
        StartCoroutine(SolitaireDeal());    // Deals the card objects
        SortDeckIntoTrips();
    } // PlayCards()

    /********************************************************/

    /*
        Generates a list of strings
    */
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    } // GenerateDeck()

    /********************************************************/

    /*
        Randomises the order of a List
    */
    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    } // Shuffle()

    /********************************************************/

    /*
        Deals the cards from the deck onto the screen
     */
    IEnumerator SolitaireDeal()
    {
        for (int i = 0; i < 7; i++)
        {
            float yOffset = 0f;     // starting position of the cards on the y axis
            float zOffset = 0.03f;  // puts cards on top of one another
            foreach (string card in bottoms[i])
            {
                // Slight pause when dealing the deck
                yield return new WaitForSeconds(0.01f);

                // Creates a new card object from the card prefab
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform);
                newCard.name = card;
                newCard.GetComponent<Selectable>().row = i;

                //makes the last card faceup
                if (card == bottoms[i][bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }

                yOffset = yOffset + 0.3f;
                zOffset = zOffset + 0.03f;
                discardPile.Add(card);

            }//foreach END


        }//for END
        foreach (string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }

        }
        discardPile.Clear();


    } // SolitaireDeal()

    /********************************************************/

    /*
        Adds cards into the bottom position Lists
        Removes those cards from the deck
    */
    void SolitaireSort()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);

            }
        }
    } // SolitaireSort()

    /********************************************************/

    /*
       Sorts deck so that three cards are dealt at a time
     */
    public void SortDeckIntoTrips()
    {
        trips = deck.Count / 3;
        tripsRemainder = deck.Count % 3;
        deckTrips.Clear();                  // ensures list is empty

        int modifier = 0;                   // increases by 3 each time to sort the deck int chunks
        // for each set of 3 cards, we create a temporary list of new strings to hold those three cards
        // which will be added to the deckTrips list. (list of lists with three strings each)
        for (int i = 0; i < trips; i++)
        {
            List<string> myTrips = new List<string>();
            for (int j = 0; j < 3; j++)
            {
                myTrips.Add(deck[j + modifier]);
            }
            deckTrips.Add(myTrips);
            modifier = modifier + 3;
        }

        //if there is a remainder
        if (tripsRemainder != 0)
        {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for (int k = 0; k < tripsRemainder; k++)
            {
                myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                modifier++;

            }
            deckTrips.Add(myRemainders);
            trips++;
        }
        deckLocation = 0;
    } // SortDeckIntoTrips()

    /********************************************************/


    /*
     Shows deck in chunks of 3
     */
    public void DealFromDeck()
    {

        //add remaining 
        foreach (Transform child in deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);


            }

        }
        // instantiates three new cards offset to the right of the deck
        if (deckLocation < trips)
        {
            // draw 3 new cards
            tripsOnDisplay.Clear();
            float xOffset = 2.5f;
            float zOffset = -0.2f;

            foreach (string card in deckTrips[deckLocation])
            {
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x + xOffset, deckButton.transform.position.y, deckButton.transform.position.z + zOffset), Quaternion.identity, deckButton.transform);
                xOffset = xOffset + 0.5f;
                zOffset = zOffset - 0.2f;
                newTopCard.name = card;     // makes card face appear accurately
                tripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().faceUp = true;
                newTopCard.GetComponent<Selectable>().inDeckPile = true;

            }
            deckLocation++;
        }// if()
        // else restack top deck
        else
        {
            RestackTopDeck();
        }
    } // DealFromDeck()

    /********************************************************/

    void RestackTopDeck()
    {
        deck.Clear();
        foreach (string card in discardPile)
        {
            deck.Add(card);

        }
        discardPile.Clear();
        SortDeckIntoTrips();
    }
} // RestackTopDeck()
