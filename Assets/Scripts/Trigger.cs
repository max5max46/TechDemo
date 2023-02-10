using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool insideTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        insideTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        insideTrigger = false;
    }
}
