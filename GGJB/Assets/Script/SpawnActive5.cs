using System.Collections;
using UnityEngine;

public class SpawnActive5 : MonoBehaviour
{
    public GameObject[] Bubbles;
    public int LamaSpawn;
    public float Timer = 0;
    public Vector3 RandomX;

    private void Start()
    {

    }

    [System.Obsolete]
    void Update()
    {
        BulletSpawn();
    }

    [System.Obsolete]
    private void BulletSpawn()
    {
        var randomBub = Random.Range(0, Bubbles.Length);
        var randomPosX = Random.RandomRange(-7f, 7f);
        Timer = Timer + 1 * Time.deltaTime;
        if (Timer >= LamaSpawn)
        {
            transform.position = new Vector3(randomPosX, transform.position.y, transform.position.z);
            GameObject bubble = Instantiate(Bubbles[randomBub], transform.position, Quaternion.identity);
            Timer = 0;
        }
    }
}
