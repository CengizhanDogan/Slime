using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private float NextSpawnTime;
    private float NextBoneTime;

    public List<GameObject> Skeletons = new List<GameObject>();
    [SerializeField]
    private float spawnDelay = 10;
    [SerializeField]
    private GameObject bone;
    private int monsterCount = 3;

    private void Update()
    {
        if (ShouldSpawn())
        {
            Spawn();
        }
        if (ShouldBone())
        {
            Bone();
        }
    }
    private void Spawn()
    {
        if (monsterCount < 8) {
            for (int i = 0; i < monsterCount; i++)
            {
                Instantiate(Skeletons[Random.Range(0, Skeletons.Count)], new Vector3(Random.Range(-8.30f, 8.20f), Random.Range(-3.74f, 1.5f)), Quaternion.identity);
            }
            monsterCount += 2;
            NextSpawnTime = Time.time + spawnDelay;
        }
    }

    private void Bone()
    {
        if (monsterCount < 10)
        {
            Instantiate(bone, new Vector3(Random.Range(-8.30f, 8.20f), 5.3f), Quaternion.identity);

            NextBoneTime = Time.time + spawnDelay / 4;
        }
    }
    private bool ShouldSpawn()
    {
        return Time.time >= NextSpawnTime;
    }

    private bool ShouldBone()
    {
        return Time.time >= NextBoneTime;
    }
}
