using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static float score = 0.0f;
    public static float highscore = 0.0f;

    private void OnEnable()
    {
        score = 0.0f;
        highscore = PlayerPrefs.GetFloat("Score", 0.0f);
    }

    public void SaveScore()
    {
        var s = PlayerPrefs.GetFloat("Score", -1.0f);
        if (score > s)
        {
            highscore = score;
            PlayerPrefs.SetFloat("Score", highscore);
            PlayerPrefs.Save();
        }
        score = 0.0f;
    }

    //if we want some kind of event when you add score
    public static void AddScore(float n)
    {
        score += n;
    }



}
