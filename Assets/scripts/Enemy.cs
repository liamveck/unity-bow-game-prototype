using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float timeActiveAfterKilled;
    public bool killed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(killed == true)
        {
            timeActiveAfterKilled = timeActiveAfterKilled - Time.deltaTime;
        }
        if(timeActiveAfterKilled <= 0)
        {
            Object.Destroy(gameObject);
        }
    }

}
