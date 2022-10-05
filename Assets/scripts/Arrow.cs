using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 speed;
    public float gravitationalConstant;
    private float timeAlive = 0;
    public float maxTimeAlive;
    private Vector3 previousPostion;
    private bool move = true;
    public float arrowForce;
    // Start is called before the first frame update
    void Start()
    {
        previousPostion = transform.position;
        UpdatePostition();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive = timeAlive + Time.deltaTime;
        if(timeAlive > maxTimeAlive)
        {
            Object.Destroy(gameObject);
        }

        UpdatePostition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            HitEnemy(collision.gameObject);
        }
        move = false;
    }

    void HitEnemy(GameObject enemy)
    {
        enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Vector3 force = new Vector3(speed.x * arrowForce, 0, speed.z * arrowForce);
        enemy.GetComponent<Rigidbody>().AddForce(force);
        enemy.GetComponent<Enemy>().killed = true;
        move = false;
        transform.parent = enemy.transform;
        //Object.Destroy(gameObject);//destroy the arrow
    }

    void UpdateRotation()
    {
        Vector3 lookat = transform.position-previousPostion;
        transform.rotation = Quaternion.LookRotation(lookat);
        Vector3 rotation = transform.rotation.eulerAngles + Quaternion.Euler(90, 0, 0).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);
        previousPostion = transform.position;
    }


    void UpdatePostition()
    {
        if (move)
        {
            speed = new Vector3(speed.x, speed.y - gravitationalConstant * Time.deltaTime, speed.z);
            Vector3 newpos = transform.position + speed * Time.deltaTime;
            transform.position = newpos;
            UpdateRotation();
        }
    }
}
