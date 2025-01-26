using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CollorBubble
{
    Merah,
    Kuning,
    Hijau,
    Biru,
    Ungu
}
public class Bubble5 : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public CollorBubble Collorbubbles;
    public float SpawnTime;
    private void Start()
    {
        ScoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Destroy(gameObject,SpawnTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bullet = GameObject.FindObjectOfType<BulletActive5>();
        var soundEffect = GameObject.Find("SoundManager").GetComponent<SoundManager5>();
        if (collision.gameObject.CompareTag("Bullet") && Collorbubbles == bullet.Collors)
        {
            soundEffect.Sound_1();
            var ScoreToInt = Convert.ToInt32(ScoreText.text);
            ScoreToInt += 1;
            ScoreText.text = ScoreToInt.ToString();
            var particle = Resources.Load<GameObject>("ParticleEffect");
            GameObject particles = Instantiate(particle,transform.position,Quaternion.identity);
            Destroy(particles,2);
            Destroy(gameObject);
        }
    }
}
