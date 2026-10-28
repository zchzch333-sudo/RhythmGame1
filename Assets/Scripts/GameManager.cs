
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public TMP_Text currentAccuracyText;
    public TMP_Text finalAccuracyText;

    public GameObject hitEffectPrefab;
    public Animator comboAnimator;
    public int highScore = 0;
    public string highScoreKey = "Song1HighScore";

    // Accuracy
    public int perfectCount = 0;
    public int goodCount = 0;
    public int missCount = 0;

    // Grade Images
    public Image currentGradeImage;
    public Image finalGradeImage;

    public Sprite gradeF;
    public Sprite gradeC;
    public Sprite gradeB;
    public Sprite gradeA;
    public Sprite gradeS;
    public int totalNotes = 239;
    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        scoreText.text = score.ToString("D6");
        currentGradeImage.gameObject.SetActive(false);
        currentAccuracyText.text = "0.0%";
    }
    public void Judge(float distance, Vector3 hitPosition)
    {
        if (distance <= 0.2f)
        {
            score += 1000;
            combo++;
            perfectCount++;
            comboAnimator.Play("Combo pop", 0, 0f);
            judgmentText.text = "PERFECT";
            Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
        }
        else if (distance <= 0.5f)
        {
            score += 500;
            combo++;
            goodCount++;
            comboAnimator.Play("Combo pop", 0, 0f);
            judgmentText.text = "GOOD";
            Instantiate(hitEffectPrefab, hitPosition, Quaternion.identity);
        }
        else
        {
            combo = 0;
            judgmentText.text = "MISS";
            missCount++;
        }

        scoreText.text = score.ToString("D6");
        comboText.text = combo.ToString();
        UpdateGrade();
    }
    void UpdateGrade()
    {
        if (totalNotes <= 0)
        {
            return;
        }

        float accuracy = (perfectCount + goodCount * 0.7f) / totalNotes * 100f;
        currentAccuracyText.text = accuracy.ToString("F1") + "%";

        Sprite grade;

        if (accuracy >= 95f)
        {
            grade = gradeS;
        }
        else if (accuracy >= 85f)
        {
            grade = gradeA;
        }
        else if (accuracy >= 70f)
        {
            grade = gradeB;
        }
        else if (accuracy >= 50f)
        {
            grade = gradeC;
        }
        else
        {
            grade = gradeF;
        }

        currentGradeImage.sprite = grade;
        currentGradeImage.gameObject.SetActive(true);
    }
    public void EndGame()
    {
        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }
        resultPanel.SetActive(true);

        finalScore.text = score.ToString("D6");
        finalCombo.text = "Final Combo: " + combo;
        highScoreText.text = highScore.ToString("D6");
        finalAccuracyText.text = currentAccuracyText.text;
        finalGradeImage.sprite = currentGradeImage.sprite;
    }
}

