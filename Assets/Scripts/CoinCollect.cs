using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    PlayerController player;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Rigidbody>() == null)
            transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
            player.coins++;
            GameObject.Destroy(transform.parent.gameObject);
        }
    }
}
