using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    Transform startPoint;
    Transform endPoint;
    bool goingToEndPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.GetChild(1);
        endPoint = transform.parent.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, startPoint.transform.position) <= 0.1f)
            goingToEndPoint = true;

        if (Vector3.Distance(transform.position, endPoint.transform.position) <= 0.1f)
            goingToEndPoint = false;
    }

    private void FixedUpdate()
    {
        if (goingToEndPoint)
        {
            transform.Translate((endPoint.position - transform.position).normalized * speed * Time.fixedDeltaTime);
        }

        if (!goingToEndPoint)
        {
            transform.Translate((startPoint.position - transform.position).normalized * speed * Time.fixedDeltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null;
    }

}
