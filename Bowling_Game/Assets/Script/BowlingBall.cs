using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BowlingBall : MonoBehaviour
{
    private static ScoreManager scoreManager; 
    private Rigidbody rb;
    private Vector3 dragStartPosition;
    private bool isDragging = false;
    private Vector3 initialPosition;

    public int maxThrows = 3; 
    private int currentThrows = 0;  
    public TMP_Text Text; 
    public GameObject losePanel; 
    public GameObject winPanel; 
    public Button nextLevelButton;  
    public Button restartButton; 

    public float maxForce = 200f; 
    public GameObject pinPrefab; 
    public Transform pinsParent; 

    void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position; 
        losePanel.SetActive(false);
        winPanel.SetActive(false);

        nextLevelButton.onClick.AddListener(LoadNextLevel);
        restartButton.onClick.AddListener(RestartGame);

        nextLevelButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 dragVector = dragStartPosition - currentMousePosition;

            float forceMagnitude = Mathf.Clamp(dragVector.magnitude / 100f, 0, maxForce);
            Vector3 forceDirection = new Vector3(dragVector.x, 0, dragVector.y).normalized;

            rb.AddForce(forceDirection * forceMagnitude);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.AddForce(-rb.velocity * 10f, ForceMode.Impulse); 
        }
    }

    public void ResetBall()
    {
        transform.position = initialPosition; 
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero; 
        ThrowBall();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pin")
        {
           GetComponent<AudioSource>().Play();
        }
                
        if(collision.gameObject.tag == "Wall")
        {
           ResetBall();
        }
    }
             void ThrowBall()
        {
            if(scoreManager.NumberScore() == 10)
            {
                ShowWinPanel();
            }
            if (currentThrows < maxThrows)
            {  
                currentThrows++;
                if (currentThrows == maxThrows)
                {
                    if(scoreManager.NumberScore() != 10)
                       ShowLosePanel();
                       
                }
            }
            else
            {
                ShowLosePanel(); 
            }
        }
    void ShowLosePanel()
    {
        losePanel.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Text.text="Game Over";
        this.enabled = false; 
    }
    void ShowWinPanel()
    {
        winPanel.SetActive(true);        
        nextLevelButton.gameObject.SetActive(true);
        Text.text="You Win";
        this.enabled = false; 
    }
        public void LoadNextLevel()
    {
        SceneManager.LoadScene("GameScene2");
    }
        public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
