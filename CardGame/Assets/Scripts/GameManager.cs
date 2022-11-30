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
    public Text maintxt;


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
        Betbtn.onClick.AddListener(() => betClicked());
    }

    private void StandbtnClicked()
    {
        standCount++;
        if (standCount > 1) roundOver();
        HitDealer();
        standBtnTxt.text = "Call";
    }

    private void HitbtnClicked()
    {

        if(playerScript.cardID <= 10)
        {
            playerScript.GetCard();
            handtxt.text = "Hand: " + playerScript.totalHandValue.ToString();
            if(playerScript.totalHandValue > 20) roundOver();
        }
    }

    private void DealbtnClicked()
    {   //reset round, hide text, prep for next round
        playerScript.ResetHand();
        dealerScript.ResetHand();

        // hide dealer score 
        maintxt.gameObject.SetActive(false);
        dealerhandtxt.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<Deck>().ShuffleCards();
        playerScript.StartHand();
        dealerScript.StartHand();

        handtxt.text = "Hand: " + playerScript.totalHandValue.ToString(); // update the hand score for the player
        dealerhandtxt.text = "Hand: " + dealerScript.totalHandValue.ToString(); // update the hand score for the dealer
        //enble to hide dealer's card
        hideCard.GetComponent<Renderer>().enabled=true;

        // make button not always visible
        Dealbtn.gameObject.SetActive(false); 
        Hitbtn.gameObject.SetActive(true);
        Standbtn.gameObject.SetActive(true);
        standBtnTxt.text = "Stand";

        // set pot size
        pot = 40;
        bettxt.text ="Bets: €" + pot.ToString();
        playerScript.AdjustMoney(-20);
        moneytxt.text = playerScript.GetMoney().ToString();
    }

    private void HitDealer()
    {
        while(dealerScript.totalHandValue < 16 && dealerScript.cardID < 10)
        {
            dealerScript.GetCard();
            dealerhandtxt.text = "Hand: " + dealerScript.totalHandValue.ToString();
            if(dealerScript.totalHandValue>20) roundOver();
        }
    } 

    void roundOver() //check for a loser or a winner, so the hand is over
    {
         bool playerBust = playerScript.totalHandValue > 21;
         bool dealerBust = dealerScript.totalHandValue > 21;
         bool playerWin = playerScript.totalHandValue == 21;
         bool dealerWin = dealerScript.totalHandValue == 21;

         if (standCount < 2 && !playerBust && !dealerBust && !playerWin && !dealerWin) return;
        bool roundOver = true;
        //both loose
        
        if(playerBust && dealerBust)
        {
            maintxt.text = "All bust: bets returned";
            playerScript.AdjustMoney(pot/2);
        }
        // dealer wins
        else if (playerBust || (!dealerBust && dealerScript.totalHandValue > playerScript.totalHandValue))
        {
            maintxt.text= "Dealer wins!";
        }
        //player wins
        else if (dealerBust || dealerScript.totalHandValue < playerScript.totalHandValue)
        {
            maintxt.text = "Player wins!";
            playerScript.AdjustMoney(pot);
        }
        //if they tie
        else if (playerScript.totalHandValue == dealerScript.totalHandValue)
        {
            maintxt.text = "Push: bets returned";
            playerScript.AdjustMoney(pot/2);
        }
        else 
        {
            roundOver=false;
        }
        //set the ui for the next hand/move/turn
        if(roundOver)
        {
            Hitbtn.gameObject.SetActive(false);
            Standbtn.gameObject.SetActive(false);
            Dealbtn.gameObject.SetActive(true);
            maintxt.gameObject.SetActive(true);
            dealerhandtxt.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled=false;
            moneytxt.text ="" + playerScript.GetMoney().ToString();
            standCount=0;
        }
    }

    void betClicked()
    {
        Text newBet = Betbtn.GetComponentInChildren(typeof(Text)) as Text;
        int intBet=10;
        playerScript.AdjustMoney(-intBet);
        moneytxt.text ="€" + playerScript.GetMoney().ToString();
        pot +=(intBet*2);
        bettxt.text ="Bets: €" + pot.ToString();
    }

}
