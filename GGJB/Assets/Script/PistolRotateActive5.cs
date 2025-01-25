using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRotateActive5 : MonoBehaviour
{
    public GameObject[] BulletRandom; 
    public Transform FirePoint; 
    public float forceSpeed = 10f; 

    private GameObject spawnedBullet; 
    private Rigidbody2D bulletRb; 
    private bool isBulletSpawned = false; 

    private void Update()
    {
        var sound = GameObject.Find("SoundManager").GetComponent<SoundManager5>();
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }
        if (Input.GetMouseButtonUp(0) && isBulletSpawned)
        {
            sound.Sound_2();
            Shoot();
        }
    }

    private void SpawnBullet()
    {
        var random = Random.Range(0, BulletRandom.Length);
        spawnedBullet = Instantiate(BulletRandom[random], FirePoint.position, Quaternion.identity);
        bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        bulletRb.isKinematic = true; 
        isBulletSpawned = true;
    }

    private void Shoot()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = (worldPosition - FirePoint.position).normalized;
        bulletRb.isKinematic = false;
        bulletRb.AddForce(direction * forceSpeed, ForceMode2D.Impulse);
        isBulletSpawned = false;
    }
}

