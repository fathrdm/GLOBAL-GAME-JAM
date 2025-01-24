using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform bubblesArea;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetAsBubbleAreaChild(Transform bubble)
    {
        SnapToNearestGridPosition(bubble);
        bubble.SetParent(bubblesArea);
    }

    public void DestroyBubbles(List<Transform> bubbles)
    {
        foreach (Transform bubble in bubbles)
        {
            Destroy(bubble.gameObject); // Hapus bubble dari scene
        }
    }

    private void SnapToNearestGridPosition(Transform bubble)
    {
        Vector3Int cellPosition = GetComponent<Grid>().WorldToCell(bubble.position);
        bubble.position = GetComponent<Grid>().GetCellCenterWorld(cellPosition);
        bubble.rotation = Quaternion.identity;
    }
}
