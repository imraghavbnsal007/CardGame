using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
 
    public Button Dealbtn;
    public Button Hitbtn;
    public Button Standbtn;
    public Button Betbtn;
    public Text standBtnTxt;

    private int standCount = 0;

    public PlayingScript playerScript;
    public PlayingScript dealerScript;


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
        GameObject.Find("Deck").GetComponent<Deck>().ShuffleCards();
        playerScript.StartHand();
        dealerScript.StartHand();
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
