using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public KillPlane killPlane;
    public Transform respawnObject;

    private void OnTriggerEnter(Collider other)
    {
        killPlane.respawnPoint = respawnObject.transform.position;
        killPlane.yRotation = transform.parent.rotation.eulerAngles.y;
        gameObject.SetActive(false);
    }
}
