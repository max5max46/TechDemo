using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRespawner : MonoBehaviour
{
    GameObject coin;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        coin = transform.GetChild(0).gameObject;
        Instantiate(coin, transform).transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
        transform.GetChild(1).gameObject.SetActive(true);
        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(1).childCount == 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(transform.GetChild(1).gameObject);
                timer = 2;
                Instantiate(coin, transform).transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                
            }
        }
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
