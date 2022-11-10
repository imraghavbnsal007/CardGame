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
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
