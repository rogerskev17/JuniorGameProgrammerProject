using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerCamera;

    public float speed = 5.0f;
    public bool isJumping = false;
    public float rotationSpeed = 3.0f;
    public float rotationInput = 0;
    public bool isRotating;
    public int treasuresCollected = 0;
    public const int neededTreasures = 3;
    Rigidbody playerRb;

    public bool gameOver = false;
    public bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        float gravityModifier = 1.0f;
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        CheckBounds();
        if (!gameOver)
        {
            RotatePlayer();
            MovePlayer();
            Jump();
        }
    }

    void CheckWinCondition()
    {
        if (treasuresCollected == neededTreasures & !gameOver)
        {
            gameOver = true;
            Debug.Log("Game Over. You win!");
        }
    }

    void CheckBounds()
    {
        //boundary checking
        float xBoundary = 24.5f;
        float zBoundary = 24.5f;
        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        }
        else if (transform.position.z < -zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        }
    }

    void RotatePlayer()
    {
        //rotate player
        rotationInput = Input.GetAxis("Mouse X");
        if (rotationInput != 0 && Input.GetMouseButton(1))
        {
            //Debug.Log("Mouse is clicked and player is rotating");
            isRotating = true;
            transform.rotation = Quaternion.AngleAxis(playerCamera.transform.eulerAngles.y, playerCamera.up);
        }
        else
        {
            isRotating = false;
        }
    }

    void MovePlayer()
    {
        //check for directional inputs
        float horizontalInput;
        float verticalInput;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(playerCamera.right * Time.deltaTime * horizontalInput * speed);
        //transform.Translate(playerCamera.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Time.deltaTime * horizontalInput * speed * Vector3.right);
        transform.Translate(Time.deltaTime * verticalInput * speed * Vector3.forward);
    }

    void Jump()
    {
        //check for jump input
        float jumpForce = 10;
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !gameOver)
        {
            //Debug.Log("Player is jumping");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Player landed");
            isJumping = false;
            //Debug.Log("isJumping: " + isJumping);
        }

        if (collision.gameObject.CompareTag("Enemy") && !gameOver && !hasPowerup)
        {
            gameOver = true;
            Debug.Log("Game Over. You lose.");
        }

        if (collision.gameObject.CompareTag("Treasure"))
        {
            Treasure chest = collision.gameObject.GetComponent<Treasure>();
            if (!chest.treasureCollected)
            {
                chest.treasureCollected = true;
                treasuresCollected++;
                Debug.Log("Treasure Collected. treasuresCollected: " + treasuresCollected);
                Debug.Log("chest.treasureCollected: " + chest.treasureCollected);
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            //Debug.Log("hasPowerup at pickup: " + hasPowerup);
            Destroy(collision.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(15);
        hasPowerup = false;
        //Debug.Log("hasPowerup after timer: " + hasPowerup);
    }
}