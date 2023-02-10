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
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.childCount == 1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("Test");
                timer = 2;
                GameObject.Instantiate(coin, transform).transform.position = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
