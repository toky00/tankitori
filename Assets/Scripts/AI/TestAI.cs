using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TestAI : MonoBehaviour
{
    public Transform targetDestination;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, targetDestination.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if(path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        float distanceToLast = Vector2.Distance(rb.position, path.vectorPath[path.vectorPath.Count-1]);
        if (distanceToLast < 0.5f)
            return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = ((Vector2)rb.transform.right).normalized * speed * Time.fixedDeltaTime;
        Vector2 forceReverse = (-(Vector2)rb.transform.right).normalized * speed * Time.fixedDeltaTime;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        float angleReverse = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)+180;

        Debug.Log(rb.rotation+" ||| "+angle + " || " + angleReverse);

        if (rb.rotation != angle)
        {
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, Quaternion.Euler(0, 0, angle), 1f);
        }
        /*else if (rb.rotation <= angleReverse - 45 && rb.rotation >= angleReverse + 45)
        {
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, Quaternion.Euler(0, 0, angleReverse), 1f);
        }*/

        if (rb.rotation >= angle - 45 && rb.rotation <= angle + 45)
        {
            rb.AddForce(force);
        }
        /*else if (rb.rotation >= angleReverse - 45 && rb.rotation <= angleReverse + 45)
        {
            rb.AddForce(forceReverse);
        }*/

        Debug.DrawRay(rb.transform.position, direction, Color.red);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
