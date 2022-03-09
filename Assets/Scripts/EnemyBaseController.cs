using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float iddleTime = 5f;

    private Animator anim;
    private Rigidbody2D rb;

    public List<Vector2> waypoints;
    private int waypointIndex = 0;
    private Vector2 movement;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Adds starting position to waypoints list.
        if (waypoints == null)
            waypoints = new List<Vector2>();

        waypoints.Add(gameObject.transform.position);
        SetNextWaypoint();
    }

    private void Update()
    {
        var nextWaypoint = waypoints[waypointIndex];
        if (nextWaypoint == (Vector2)transform.position)
        {
            // If we reach a waypoint, we set a new destination after an iddle time
            movement = Vector2.zero;
            StartCoroutine(SetNextWaypoint());
        }
    }

    private void FixedUpdate()
    {
        // Moves only if we need to..
        if (movement != Vector2.zero)
            rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * movement);
    }

    protected virtual IEnumerator SetNextWaypoint()
    {
        yield return new WaitForSeconds(iddleTime);

        // Calculates the next waypoint...
        waypointIndex = waypointIndex + 1 < waypoints.Count ? waypointIndex++ : 0;
        var nextWaypoint = waypoints[waypointIndex];

        // Rotates towards waypoint and starts moving again..
        var direction = nextWaypoint - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        // Set variables for movement animation...
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
    }
}