using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporters : MonoBehaviour
{
    TeleporterPlayerDetect teleporter1;
    TeleporterPlayerDetect teleporter2;
    public GameObject player;
    PlayerController playerController;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        teleporter1 = transform.GetChild(0).GetComponent<TeleporterPlayerDetect>();
        teleporter2 = transform.GetChild(1).GetComponent<TeleporterPlayerDetect>();
        playerController = player.GetComponent<PlayerController>();
        offset = new Vector3(0, 1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (teleporter1.isOnTeleporter == true)
        {
            teleporter1.isOnTeleporter = false;
            player.transform.position = teleporter2.transform.position + offset;
            playerController.RecenterCamera(transform.GetChild(1).rotation.eulerAngles.y);
        }

        if (teleporter2.isOnTeleporter == true)
        {
            teleporter2.isOnTeleporter = false;
            player.transform.position = teleporter1.transform.position + offset;
            playerController.RecenterCamera(transform.GetChild(0).rotation.eulerAngles.y);
        }
    }
}
