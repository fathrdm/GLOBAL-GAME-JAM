using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float raycastRange = 0.15f; // Jarak deteksi tetangga dengan Raycast
    public float raycastOffset = 0.6f; // Offset posisi Raycast untuk sudut

    public bool isFixed = false; // Status apakah bubble sudah ditempatkan
    public BubbleColor bubbleColor; // Warna bubble

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah bubble bertabrakan dengan bubble lain yang sudah "fixed" atau batas permainan
        if ((collision.gameObject.tag == "Bubble" && collision.gameObject.GetComponent<Bubble>().isFixed)
            || collision.gameObject.tag == "Limit")
        {
            if (!isFixed)
                HasCollided();
        }
    }

    private void HasCollided()
    {
        // Matikan rigidbody dan tetapkan status bubble sebagai fixed
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Destroy(rb);
        isFixed = true;

        // Informasikan ke GameManager bahwa bubble ini ditempatkan
        GameManager.instance.ProcessTurn(transform);
    }

    public List<Transform> GetNeighbours()
    {
        List<Transform> neighbours = new List<Transform>();

        // Raycast untuk mendeteksi tetangga
        RaycastHit2D[] hits = new RaycastHit2D[]
        {
            Physics2D.Raycast(transform.position, Vector2.left, raycastRange),
            Physics2D.Raycast(transform.position, Vector2.right, raycastRange),
            Physics2D.Raycast(transform.position + new Vector3(-raycastOffset, raycastOffset, 0), new Vector2(-1f, 1f), raycastRange),
            Physics2D.Raycast(transform.position + new Vector3(-raycastOffset, -raycastOffset, 0), new Vector2(-1f, -1f), raycastRange),
            Physics2D.Raycast(transform.position + new Vector3(raycastOffset, raycastOffset, 0), new Vector2(1f, 1f), raycastRange),
            Physics2D.Raycast(transform.position + new Vector3(raycastOffset, -raycastOffset, 0), new Vector2(1f, -1f), raycastRange)
        };

        // Tambahkan tetangga yang ditemukan ke daftar
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Bubble"))
            {
                neighbours.Add(hit.transform);
            }
        }

        return neighbours;
    }

    // Menghancurkan bubble jika keluar dari layar
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Enum untuk warna bubble
    public enum BubbleColor
    {
        Blue, Yellow, Red, Green
    }

    // Debug: Menampilkan garis menuju tetangga yang terdeteksi
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        foreach (Transform neighbour in GetNeighbours())
        {
            Gizmos.DrawLine(transform.position, neighbour.position);
        }
    }
}
