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

    bool canJump = false;
    Vector3 activeSpawnpoint = new Vector3(-1.894f, 1.79f, 0);
    Rigidbody rb;
    float inputHorizontal;
    float inputVertical;
    Vector3 initialCameraPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialCameraPos = Camera.main.transform.position;
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
        Vector3 direction = Camera.main.transform.forward * inputVertical + Camera.main.transform.right * inputHorizontal;
        rb.AddForce(direction * speed);

        //Camera seguir o jogador
        Camera.main.transform.position = new Vector3(transform.position.x + initialCameraPos.x, transform.position.y + initialCameraPos.y, transform.position.z + initialCameraPos.z);
    }

    public void Respawn()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        transform.position = activeSpawnpoint;
        rb.isKinematic = false;
        gameOverScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "GameOverTrigger":
                rb.isKinematic = true;
                gameOverScreen.SetActive(true);
                break;

            case "Spawnpoint":
                activeSpawnpoint = other.transform.position;
                Destroy(other.gameObject);
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
        }
    }
}