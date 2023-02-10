using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    Transform startPoint;
    Transform endPoint;
    Trigger trigger;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.GetChild(2);
        endPoint = transform.GetChild(3);
        trigger = transform.GetChild(1).gameObject.GetComponent<Trigger>();
    }


    private void FixedUpdate()
    {

        if (trigger.insideTrigger)
        {
            transform.Translate((endPoint.position - transform.position).normalized * speed * Time.fixedDeltaTime);
        }

        if (!trigger.insideTrigger)
        {
            transform.Translate((startPoint.position - transform.position).normalized * speed * Time.fixedDeltaTime);
        }

    }
}
