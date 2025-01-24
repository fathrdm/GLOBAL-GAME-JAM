using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton untuk akses global
    private const int SEQUENCE_SIZE = 3; // Jumlah bubble minimum untuk meledak

    private List<Transform> sequenceBubbles; // Menyimpan bubble dalam sequence
    //public GameObject explosionPrefab; // Efek ledakan

    private void Awake()
    {
        if (instance == null)
            instance = this;

        sequenceBubbles = new List<Transform>();
    }

    // Mengecek bubble yang bersentuhan untuk sequence
    public void ProcessTurn(Transform currentBubble)
    {
        StartCoroutine(CheckSequence(currentBubble));
    }

    private IEnumerator CheckSequence(Transform currentBubble)
    {
        yield return new WaitForSeconds(0.1f); // Tunggu sebentar sebelum mengecek

        sequenceBubbles.Clear();
        CheckBubbleSequence(currentBubble);

        // Jika sequence mencukupi jumlah, bubble meledak
        if (sequenceBubbles.Count >= SEQUENCE_SIZE)
        {
            foreach (Transform bubble in sequenceBubbles)
            {
                // Membuat efek ledakan
                //Instantiate(explosionPrefab, bubble.position, Quaternion.identity);
                Destroy(bubble.gameObject); // Hapus bubble dari game
            }
        }

        sequenceBubbles.Clear(); // Reset sequence untuk turn berikutnya
    }

    // Rekursif untuk mengecek bubble dengan warna yang sama
    private void CheckBubbleSequence(Transform currentBubble)
    {
        sequenceBubbles.Add(currentBubble);

        Bubble bubbleScript = currentBubble.GetComponent<Bubble>();
        List<Transform> neighbours = bubbleScript.GetNeighbours();

        foreach (Transform neighbour in neighbours)
        {
            if (!sequenceBubbles.Contains(neighbour))
            {
                Bubble neighbourScript = neighbour.GetComponent<Bubble>();
                if (neighbourScript.bubbleColor == bubbleScript.bubbleColor)
                {
                    CheckBubbleSequence(neighbour); // Lanjutkan pengecekan ke tetangga
                }
            }
        }
    }
}
