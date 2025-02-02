using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCredits : MonoBehaviour
{
    [SerializeField] GameObject buttonBack;
    private Animator animatorCredit;
    private void Start()
    {
        buttonBack.SetActive(false);
        animatorCredit = GetComponent<Animator>();

    }

    public void PlayAnimation()
    {
        buttonBack?.SetActive(false);
        animatorCredit.enabled = true;
    }
    public void BackButtonShowup()
    {
        buttonBack.SetActive(true);
        animatorCredit.enabled = false;
    }
}
