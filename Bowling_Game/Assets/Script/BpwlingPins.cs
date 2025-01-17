using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpwlingPins : MonoBehaviour
{
private static ScoreManager scoreManager; 

    private void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        float xRotation = transform.rotation.eulerAngles.x;

        if (xRotation < 60f || xRotation > 300f)
        {
            DestroyPin();
        }
    }

    private void DestroyPin()
    {

        scoreManager.IncrementScore(); 
        Destroy(gameObject);
    }
}
