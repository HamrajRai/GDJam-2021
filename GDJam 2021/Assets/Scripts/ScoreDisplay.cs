using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI currentScore = null;
    [SerializeField] TMPro.TextMeshProUGUI highScore = null;

    private void Update()
    {
        currentScore.text = "Score: " + ScoreManager.score.ToString();
        highScore.text = "Highscore: " + ScoreManager.highscore.ToString();
    }

}
