
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int combo = 0;

    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text judgmentText;
    public GameObject resultPanel;
    public TMP_Text finalScore;
    public TMP_Text finalCombo;

    public void Judge(float distance)
    {
        if (distance <= 0.2f)
        {
            score += 1000;
            combo++;
            judgmentText.text = "PERFECT";
        }
        else if (distance <= 0.5f)
        {
            score += 500;
            combo++;
            judgmentText.text = "GOOD";
        }
        else
        {
            combo = 0;
            judgmentText.text = "MISS";
        }

        scoreText.text = "Score: " + score;
        comboText.text = combo + "Combo";
    }
    public void EndGame()
    {
        resultPanel.SetActive(true);

        finalScore.text = "Final Score: " + score;
        finalCombo.text = "Final Combo: " + combo;
    }
}

