using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    float timeStartOffset = 0;

    bool gotStartTime = false;

    [SerializeField] float speed = 1f;

    void Update()
    {
        if (!gotStartTime)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }
        this.transform.position = new Vector3(transform.position.x, transform.position.y, (Time.realtimeSinceStartup - timeStartOffset) * speed);
    }
}
