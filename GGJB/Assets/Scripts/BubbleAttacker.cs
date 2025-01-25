using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAttacker : MonoBehaviour
{
    public List<Transform> fixedBubbles; // Daftar bubble yang fix di stage
    public Transform player;            // Transform pemain (target)
    public float attackInterval = 5f;   // Waktu antar serangan
    public float moveSpeed = 2f;        // Kecepatan bubble menyerang
    public int damage = 1;              // Damage ke pemain

    private bool isAttacking = false;

    private void Start()
    {
        // Mulai serangan secara berkala
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (fixedBubbles.Count > 0)
            {
                // Pilih bubble secara acak
                Transform randomBubble = fixedBubbles[Random.Range(0, fixedBubbles.Count)];

                // Pastikan bubble valid dan belum menyerang
                if (randomBubble != null && randomBubble.gameObject.activeInHierarchy)
                {
                    StartCoroutine(AttackPlayer(randomBubble));
                }
            }

            yield return new WaitForSeconds(attackInterval);
        }
    }

    private IEnumerator AttackPlayer(Transform bubble)
    {
        isAttacking = true;

        // Hapus bubble dari daftar fixedBubbles
        fixedBubbles.Remove(bubble);

        // Hitung arah gerakan bubble
        Vector2 direction = (player.position - bubble.position).normalized;

        // Gerakkan bubble ke arah awal posisi pemain
        while (bubble != null)
        {
            bubble.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            // Jika bubble menyentuh pemain
            if (player != null && Vector2.Distance(bubble.position, player.position) < 0.1f)
            {
                OnHitPlayer(bubble);
                yield break;
            }

            yield return null;
        }
    }

    private void OnHitPlayer(Transform bubble)
    {
        // Kurangi HP pemain
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        // Hancurkan bubble setelah menyerang
        Destroy(bubble.gameObject);
    }

    public void AddBubble(Transform bubble)
    {
        // Tambahkan bubble baru ke daftar fixedBubbles
        if (!fixedBubbles.Contains(bubble))
        {
            fixedBubbles.Add(bubble);
        }
    }

    public void RemoveBubble(Transform bubble)
    {
        // Hapus bubble dari daftar fixedBubbles
        if (fixedBubbles.Contains(bubble))
        {
            fixedBubbles.Remove(bubble);
        }
    }
}
