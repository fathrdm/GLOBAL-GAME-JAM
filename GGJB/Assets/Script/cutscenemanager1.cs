using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cutsceneManager1 : MonoBehaviour
{
    public GameObject nextLevelButton; 
    public Animator cutsceneAnimator;

    private void Start()
    {
        cutsceneAnimator = GetComponent<Animator>();
        nextLevelButton.SetActive(false);
    }

    public void ShowButton()
    {
        nextLevelButton.SetActive(true); 
    }
}
