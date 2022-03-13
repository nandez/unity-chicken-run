using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Animator playerAnimator;
    private Rigidbody2D playerRigidBody;
    private Vector2 movement;
    private AudioSource playerAudioSource;
    private bool canPlayAudio = false;

    private GameManager gameManager;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
        StartCoroutine(EnableAudio());

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


        if (!playerAudioSource.isPlaying && canPlayAudio)
        {
            canPlayAudio = false;
            playerAudioSource.Play();
            StartCoroutine(EnableAudio());
        }
            
    }

    private void FixedUpdate()
    {
        playerRigidBody.MovePosition(playerRigidBody.position + movementSpeed * Time.fixedDeltaTime * movement);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Fruit"))
        {
            gameManager.OnFruitCollected();
            col.gameObject.SetActive(false);
        }
        else if (col.gameObject.CompareTag("Home"))
        {
            gameManager.OnHomeZoneEntered();
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            gameManager.OnPlayerDeath();
        }
    }

    private IEnumerator EnableAudio()
    {
        yield return new WaitForSeconds(10);
        canPlayAudio = true;
    }
}