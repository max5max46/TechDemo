using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    Transform door;
    Trigger trigger;
    Transform startPoint;
    Transform endPoint;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        door = transform.GetChild(0);
        trigger = transform.GetChild(1).gameObject.GetComponent<Trigger>();
        startPoint = transform.GetChild(2);
        endPoint = transform.GetChild(3);
    }


    private void FixedUpdate()
    {

        if (trigger.insideTrigger && Vector3.Distance(door.transform.position, endPoint.transform.position) > 0.1f)
        {
            door.transform.Translate((endPoint.position - door.transform.position).normalized * speed * Time.fixedDeltaTime);
        }

        if (!trigger.insideTrigger && Vector3.Distance(door.transform.position, startPoint.transform.position) > 0.01f)
        {
            door.transform.Translate((startPoint.position - door.transform.position).normalized * speed * Time.fixedDeltaTime);
        }

    }
}
