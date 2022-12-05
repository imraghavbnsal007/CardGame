using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Solitaire");
    }

    public void ExitGame(){
        SceneManager.LoadScene("MainMenu");
    }
    public void SecondGame(){
        SceneManager.LoadScene("Spider");
    }
    public void QuitGame(){
        Application.Quit();
    }
    
}
