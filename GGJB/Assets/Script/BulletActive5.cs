using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletActive5 : MonoBehaviour
{
    public float TimeToDestroy;
    public CollorBubble Collors;

    private void Start()
    {
        
    }
    private void Update()
    {
        Destroy(gameObject,TimeToDestroy);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bubbleColor = GameObject.FindObjectOfType<Bubble5>();
     
        if (collision.gameObject.CompareTag("Bubble") && bubbleColor.Collorbubbles == Collors)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bubble"))
        {
            Destroy(gameObject);
        }
    }
}
