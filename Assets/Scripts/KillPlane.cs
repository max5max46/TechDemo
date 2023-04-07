using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{

    public Transform player;
    public PlayerController controller;
    public Vector3 respawnPoint;
    public float yRotation;

    private void Start()
    {
        respawnPoint = player.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller.RecenterCamera(yRotation);
            Debug.Log(respawnPoint.x + " " + respawnPoint.y + " " + respawnPoint.z);
            player.position = respawnPoint;
        }
    }
}
