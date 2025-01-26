using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using TMPro;
using UnityEngine;

public enum EndingStage
{
    Ggj25,
    HappyEnding,
    BadEnding,
}

public class EndingManager5 : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public EndingStage Ending;
    public int TimerMinit;
    public float TimerDetik;

    private void Start()
    {
        Ending = EndingStage.Ggj25;
        ScoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        TimerDetik = TimerDetik + 1 * Time.deltaTime;
        if (TimerDetik >= 60) 
        {
            TimerMinit += 1;
            TimerDetik = 0;
        }

        var scoreToInt = Convert.ToInt32(ScoreText.text);

        if (TimerMinit >= 3 && scoreToInt <= 20) 
        {
            Ending = EndingStage.HappyEnding;
        }
        else if(TimerMinit >= 3 && scoreToInt >= 20)
        {
            Ending = EndingStage.BadEnding;
        }

        switch (Ending)
        {
            case EndingStage.HappyEnding:
                Ending = EndingStage.HappyEnding;
                Debug.Log("Happy");
                break;
            case EndingStage.BadEnding:
                Ending = EndingStage.BadEnding;
                Debug.Log("BadEnding");
                break;
        }
    }
}
