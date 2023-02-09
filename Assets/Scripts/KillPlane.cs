using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{

    public Transform player;
    public Vector3 respawnPoint;

    private void Start()
    {
        respawnPoint = player.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        player.position = respawnPoint;
    }
}
