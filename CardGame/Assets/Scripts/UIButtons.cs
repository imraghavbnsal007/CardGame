using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{

    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;

    void Start()
    {
        // if there is no saved data from a previous game session, assign muted value 0
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else 
        {
            Load();
        }

        UpdateButtonIcon();         // loads up correct icon to match previous user choices

        if (muted)// assigns correct state for the audioListener
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    // when button is pressed: mute function
    public void OnButtonPress() 
    {
        // if the sound button is clicked and the game isn't muted, all sounds are muted
        if (muted == false)
        {
            muted = true;
            AudioListener.volume = 0;
        }
        // if the sound button is clicked and the game is muted, play all sounds
        else
        {
            muted = false;
            AudioListener.volume = 1;
        }

        Save();             //saves new mute bool value
        UpdateButtonIcon(); //updates icon
    }

    // updates the sound button icon
    private void UpdateButtonIcon()
    {
        if(muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }

    }

    // loads up the value for muted and checks if its equal to 1; returns false if not equal to 1
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    // evaluates and assigns the value for muted
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    public void ResetScene()
    {  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Button is working");
    }


}
