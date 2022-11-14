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
        throw new NotImplementedException();
    }

    private void HitbtnClicked()
    {
        throw new NotImplementedException();
    }

    private void DealbtnClicked()
    {
        GameObject.Find("Deck").GetComponent<Deck>().ShuffleCards();
        playerScript.StartHand();
        dealerScript.StartHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
