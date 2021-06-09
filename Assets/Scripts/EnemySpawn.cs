using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private float NextSpawnTime;

    [SerializeField]
    private GameObject SkeletonWithSwordPrefab;
    [SerializeField]
    private float spawnDelay = 10;
    private int monsterCount =3;

    private void Update()
    {
        if (ShouldSpawn())
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        if (monsterCount < 8) {
            for (int i = 0; i < monsterCount; i++)
            {
                Instantiate(SkeletonWithSwordPrefab, new Vector3(Random.Range(-8.30f, 8.20f), Random.Range(-3.74f, 2.80f)), Quaternion.identity);
            }
            monsterCount += 2;
            NextSpawnTime = Time.time + spawnDelay;
        }
    }
    private bool ShouldSpawn()
    {
        return Time.time >= NextSpawnTime;
    } 


}
