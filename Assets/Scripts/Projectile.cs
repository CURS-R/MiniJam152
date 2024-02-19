using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public uint lifetime { get; private set; }
    AudioManager audioManager;

    void Start()
    {
        StartCoroutine(BulletLifeTime());
    }

    IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(lifetime);
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
        audioManager.BallSound();
    }
}