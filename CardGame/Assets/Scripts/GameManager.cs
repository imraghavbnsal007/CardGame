using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
 
    public Button Dealbtn;
    public Button Hitbtn;
    public Button Standbtn;
    public Button Betbtn;

    public Text standBtnTxt;
    public Text moneytxt;
    public Text handtxt;
    public Text bettxt;
    public Text dealerhandtxt;


    private int standCount = 0;

    public PlayingScript playerScript;
    public PlayingScript dealerScript;

    public GameObject hideCard; // card hiding second dealer card

    int pot = 0;


    void Start()
    {
        // functions for buttons
        Dealbtn.onClick.AddListener(() => DealbtnClicked());
        Hitbtn.onClick.AddListener(() => HitbtnClicked());
        Standbtn.onClick.AddListener(() => StandbtnClicked());
        
    }

    private void StandbtnClicked()
    {
        standCount++;
        if (standCount > 1) Debug.Log("end function");
        HitDealer();
        standBtnTxt.text = "Call";
    }

    private void HitbtnClicked()
    {

        if(playerScript.GetCard() <= 10)
        {
            playerScript.GetCard();
        }
    }

    private void DealbtnClicked()
    {
        dealerhandtxt.gameObject.SetActive(false); // hide dealer score 
        GameObject.Find("Deck").GetComponent<Deck>().ShuffleCards();
        playerScript.StartHand();
        dealerScript.StartHand();

        handtxt.text = "Hand: " + playerScript.totalHandValue.ToString(); // update the hand score for the player
        dealerhandtxt.text = "Hand: " + dealerScript.totalHandValue.ToString(); // update the hand score for the dealer

        Dealbtn.gameObject.SetActive(false); // make button not always visible
        Hitbtn.gameObject.SetActive(true);
        Standbtn.gameObject.SetActive(true);
        standBtnTxt.text = "Stand";

        // set pot size
        pot = 40;
        bettxt.text = pot.ToString();
        playerScript.AdjustMoney(-20);
        moneytxt.text = playerScript.GetMoney().ToString();
    }

    private void HitDealer()
    {
        while(dealerScript.totalHandValue < 16 && dealerScript.cardID < 10)
        {
            dealerScript.GetCard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
