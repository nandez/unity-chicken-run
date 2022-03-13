using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float idleTime = 3f;

    public List<Transform> waypoints;

    private int waypointIndex = 0;
    private float iddleCounter = 0f;
    private bool isIdle = false;

    private Animator anim;
    private AudioSource audioSrc;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isIdle)
        {
            // As we're in idle mode, we update the counter to wait prior moving.
            iddleCounter += Time.deltaTime;
            if (iddleCounter < idleTime)
                return;

            isIdle = false;

            if (Random.value < 0.1)
                audioSrc.Play();
        }

        if (waypoints?.Count > 0)
        {
            var waypoint = waypoints[waypointIndex];
            Vector3 newPosition;

            if (Vector3.Distance(transform.position, waypoint.position) < 0.01f)
            {
                // Here we've reached the waypoint, so we reset the counter and set the idle mode.
                newPosition = waypoint.position;
                iddleCounter = 0f;
                isIdle = true;

                // Set the movement variables for animation to zero.
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 0);

                // Moves to next waypoint
                waypointIndex = (waypointIndex + 1) % waypoints.Count;
            }
            else
            {
                // In this case, we move towards the waypoint.
                newPosition = Vector3.MoveTowards(transform.position, waypoint.position, movementSpeed * Time.deltaTime);

                // Calculates the direction of movement for animation variables..
                var direction = newPosition - transform.position;
                anim.SetFloat("Horizontal", direction.normalized.x);
                anim.SetFloat("Vertical", direction.normalized.y);
            }

            // Finally updates current position.
            transform.position = newPosition;
        }
    }
}