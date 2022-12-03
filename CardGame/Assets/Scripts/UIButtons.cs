using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{


    public void ResetScene()
    {  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Button is working");
    }


}
