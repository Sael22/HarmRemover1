using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    
    public float timeLeft = 120f;
    public Text countdownText;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        countdownText.text = "Time: " + Mathf.Round(timeLeft);
        
        if (timeLeft < 0)
        {
            SceneManager.LoadScene("Lose");
        }
    }


}
