using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterPlayerDetect : MonoBehaviour
{
    public bool isOnTeleporter;
    bool teleportReady;
    // Start is called before the first frame update
    void Start()
    {
        teleportReady = false;
        isOnTeleporter = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.up, 0.3f))
        {
            if (teleportReady)
            {
                isOnTeleporter = true;
                teleportReady = false;
            }
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            teleportReady = true;
        }
    }
}
