using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActive5 : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        MovementPlayer();
    }

    private void MovementPlayer()
    {
        var moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (moveX * Speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("End Game");
    }
}
