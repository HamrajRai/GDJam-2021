using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score = 0.0f;
    public float highscore = 0.0f;

    public void SaveScore()
    {
        var s = PlayerPrefs.GetFloat("Score", -1.0f);
        if (score > s)
            highscore = score;
        PlayerPrefs.SetFloat("Score", highscore);
        PlayerPrefs.Save();
        score = 0.0f;
    }

    public void AddScore(float n)
    {
        score += n;
    }



}
