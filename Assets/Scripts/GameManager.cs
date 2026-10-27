
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
    public TMP_Text highScoreText;
    public GameObject hitEffectPrefab;
    public Animator comboAnimator;
    public int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    public void Judge(float distance, Vector3 hitPosition)
    {
        if (distance <= 0.2f)
        {
            score += 1000;
            combo++;
            comboAnimator.Play("Combo pop", 0, 0f);
            judgmentText.text = "PERFECT";
            Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
        }
        else if (distance <= 0.5f)
        {
            score += 500;
            combo++;
            comboAnimator.Play("Combo pop", 0, 0f);
            judgmentText.text = "GOOD";
            Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
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
        if (score > highScore)
        {
            highScore = score;
            
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        resultPanel.SetActive(true);

        finalScore.text = "Final Score: " + score;
        finalCombo.text = "Final Combo: " + combo;
        highScoreText.text = "High Score:" + highScore;

    }
}

