using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BowlingBall : MonoBehaviour
{
    public float speed = 200f; 
    private Vector3 startMousePosition;
    private Vector3 endMousePosition;
    private static ScoreManager scoreManager; 
    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 initialPosition;

    public int maxThrows = 3; 
    private int currentThrows = 0;  
    public GameObject losePanel; 
    public GameObject winPanel; 
    public Button nextLevelButton;  
    public Button restartButton; 
    public GameObject[] health1;

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
            startMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            endMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;

            Vector3 mouseDelta = endMousePosition - startMousePosition;

            Vector3 forceDirection = new Vector3(mouseDelta.x, 0, mouseDelta.y).normalized;

            Vector3 force = forceDirection * speed;

            rb.AddForce(force, ForceMode.Impulse);
        }

    }

    public void ResetBall()
    {
        transform.position = initialPosition; 
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero; 
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
           ThrowBall();
        }
    }

     public void ThrowBall()
    {
        UpdateLivesDisplay();
        if (currentThrows <= maxThrows)
        {  
            currentThrows++;
            if(scoreManager.NumberScore() == 10)
            {
                 ShowWinPanel();
            }
            else if (currentThrows == maxThrows)
            {
                if(scoreManager.NumberScore() != 10)
                 ShowLosePanel();         
            }
        }
     }

    void ShowLosePanel()
    {
        losePanel.SetActive(true);
        restartButton.gameObject.SetActive(true);
        this.enabled = false; 
    }
    void ShowWinPanel()
    {
        winPanel.SetActive(true);        
        nextLevelButton.gameObject.SetActive(true);
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

        void UpdateLivesDisplay()
    {
        if (health1.Length > maxThrows - currentThrows - 1)
            {
                Destroy(health1[maxThrows - currentThrows - 1]);
            }
    }
}