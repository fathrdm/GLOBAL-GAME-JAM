using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GunActive5 : MonoBehaviour
{
    public GameObject[] BulletRandom;
    public Transform FirePoint;
    public float BulletSpeed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var random = Random.Range(0, BulletRandom.Length);
        var bullet = Instantiate(BulletRandom[random],FirePoint.position,Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
    }
}
