using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI currentScore = null;
    [SerializeField] TMPro.TextMeshProUGUI highScore = null;
    [SerializeField] ScoreManager scoreManager = null;

    private void Update()
    {
        currentScore.text = "Score: " + scoreManager.score.ToString();
        highScore.text = "Highscore: " + scoreManager.highscore.ToString();
    }

}
