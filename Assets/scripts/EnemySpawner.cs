using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float maxX;
    public float maxZ;
    public float Zoffset;
    public int maxEnemys;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int enemys = GameObject.FindGameObjectsWithTag("enemy").Length;
        for (int i = 0; i < (maxEnemys-enemys); i++)
        {
            SpawnNewEnemy();
        }
    }

    void SpawnNewEnemy()
    {
        GameObject newEnemy = Instantiate(Enemy);
        Vector3 pos = new Vector3((Random.value-0.5f)*2*maxX,newEnemy.transform.position.y, (Random.value - 0.5f) * 2 * maxZ + Zoffset);
        newEnemy.transform.position = pos;
    }
}
