using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; // Maksimum HP pemain
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player hit! Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            TakeDamage(1);
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Tambahkan logika game over di sini
    }
}
