using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{
    public GameObject Text;
    public GameObject Game;
    public void PauseGame()
    {
        if (Time.timeScale==1)
        {
            Time.timeScale = 0;
            Text.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Text.SetActive(false);
            
        }
    }

    public void PlayGame()
    {
        Game.SetActive(true);
    }

    


}
