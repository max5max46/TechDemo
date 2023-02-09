using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public KillPlane killPlane;
    public Transform respawnObject;
    public Transform cam;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        killPlane.respawnPoint = respawnObject.transform.position;
        //player.transform.Rotate(new Vector3 (player.transform.rotation.x, transform.rotation.y, player.transform.rotation.z));
        //cam.transform.Rotate(Vector3.zero);
        gameObject.SetActive(false);
    }
}
