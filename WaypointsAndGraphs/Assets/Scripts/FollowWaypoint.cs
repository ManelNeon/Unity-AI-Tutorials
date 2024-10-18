using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;

    int currentWaypoint = 0;

    [SerializeField] float speed = 10.0f;

    [SerializeField] float rotSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, waypoints[currentWaypoint].transform.position) < 3)
        {
            currentWaypoint++;

            if(currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }

        //this.transform.LookAt(waypoints[currentWaypoint].transform);

        Quaternion lookAtWaypoint = Quaternion.LookRotation(waypoints[currentWaypoint].transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWaypoint, rotSpeed * Time.deltaTime);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
