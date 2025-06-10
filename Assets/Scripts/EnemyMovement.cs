using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    protected Transform targetWaypoints;
    protected int waypointIndex = 0;//chỉ số của waypoint trong mảng

    protected Rigidbody2D rb2D;
    // Start is called b efore the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        targetWaypoints = WayPoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = targetWaypoints.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoints.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (waypointIndex >= WayPoints.points.Length - 1)
        {
           // GameManager.instance.EnemyReachedBase(); // send GameManager
            Destroy(gameObject);
            return;
        }

        //change next waypoint
        waypointIndex++;
        targetWaypoints = WayPoints.points[waypointIndex];

    }
}
