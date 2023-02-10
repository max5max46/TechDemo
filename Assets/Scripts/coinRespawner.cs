using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRespawner : MonoBehaviour
{
    public GameObject coin;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.childCount == 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("Test");
                timer = 2;
                GameObject.Instantiate(coin, transform).transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
            }
        }
    }
}
