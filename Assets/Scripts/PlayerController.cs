using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Animator playerAnimator;
    private Rigidbody2D playerRigidBody;
    private Vector2 movement;

    private GameManager gameManager;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        // Captures the player input..
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Updates the animator variables for movement..
        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Vertical", movement.y);
    }

    private void FixedUpdate()
    {
        playerRigidBody.MovePosition(playerRigidBody.position + movementSpeed * Time.fixedDeltaTime * movement);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Fruit"))
        {
            gameManager.OnFruitCollected();
            col.gameObject.SetActive(false);
        }
    }
}
