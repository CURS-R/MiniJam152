using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int lifetime = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletLifeTime());
    }

    IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rat")
        {
            Destroy(gameObject);
        }
    }
}