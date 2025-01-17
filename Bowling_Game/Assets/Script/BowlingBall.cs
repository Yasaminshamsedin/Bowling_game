using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
      private Rigidbody rb;
    private Vector3 dragStartPosition;
    private bool isDragging = false;
    private Vector3 initialPosition;

    public float maxForce = 200f; 
    public GameObject pinPrefab; 
    public Transform pinsParent; 

    private int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position; 
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pin"))
        {
            score++;
            Destroy(other.gameObject);
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
           GetComponent<AudioSource>().Play();
           transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
