using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stop : MonoBehaviour
{
    public Button stopButton; 
    public Button continueButton;  
    public Button exitButton; 
    public GameObject panel;
    void Start()
    {
        stopButton.onClick.AddListener(stopGame);
        continueButton.onClick.AddListener(continueGame);
        exitButton.onClick.AddListener(exitGame);
        panel.SetActive(false);
    }
        public void stopGame()
    {
        this.enabled = false; 
        panel.SetActive(true);
    }

        public void exitGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

        public void continueGame()
    {
        panel.SetActive(false);
        this.enabled = true; 
    }
}
