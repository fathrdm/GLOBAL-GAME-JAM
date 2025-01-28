using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel4 : MonoBehaviour
{
    public void toCutscene()
    {
        SceneManager.LoadScene("CutScene5");
    }
}
