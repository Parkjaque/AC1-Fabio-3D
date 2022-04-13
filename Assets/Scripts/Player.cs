using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float forceJump;
    public LayerMask floorMask;
    public Text coinText;
    public Image jumpImage;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject mainCamera;
    public Text spawnpointNotification;

    bool canJump = false;
    Vector3 activeSpawnpoint;
    Rigidbody rb;
    float inputHorizontal;
    float inputVertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        activeSpawnpoint = transform.position;
        Cursor.visible = false;
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        if (canJump && Input.GetButtonDown("Jump") && Physics.CheckSphere(transform.position + new Vector3(0, -0.501f, 0), .1f, floorMask))
        {
            rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = mainCamera.transform.forward * inputVertical + mainCamera.transform.right * inputHorizontal;
        rb.AddForce(direction * speed);
    }

    public void Respawn()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.position = activeSpawnpoint;
        rb.isKinematic = false;
        gameOverScreen.SetActive(false);
        Cursor.visible = false;
    }

    private IEnumerator disableNotification() 
    {
        yield return new WaitForSeconds(1.4f);
        spawnpointNotification.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "GameOverTrigger":
                rb.isKinematic = true;
                gameOverScreen.SetActive(true);
                Cursor.visible = true;
                break;

            case "Spawnpoint":
                activeSpawnpoint = other.transform.position;
                Spawnpoint spawnpoint = other.GetComponent<Spawnpoint>();
                spawnpointNotification.enabled = true;
                spawnpointNotification.text = "Level " + spawnpoint.level;
                Destroy(other.gameObject);
                StartCoroutine(disableNotification());
                break;

            case "Coin":
                Destroy(other.gameObject);
                coinText.text = (int.Parse(coinText.text) + 1).ToString();
                break;

            case "CanJump":
                canJump = true;
                jumpImage.enabled = true;
                Destroy(other.gameObject);
                break;

            case "Finish":
                winScreen.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }
}