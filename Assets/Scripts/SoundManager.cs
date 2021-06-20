using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource eatSound;
    public AudioSource slimeDashAndAttack;
    public AudioSource slimeSwordAttack;
    public AudioSource fireballSound;
    public AudioSource slimeTakeDamage;
    public AudioSource enemyInstantiateSound;
    public AudioSource skeletonAttack;
    public AudioSource skeletonDeath;
    public AudioSource spikeSound;
    public AudioSource dropBoneSound;
    public AudioSource movementSlimeSound;
    

    private void Awake()
    {
        Instance = this;
    }    
}
