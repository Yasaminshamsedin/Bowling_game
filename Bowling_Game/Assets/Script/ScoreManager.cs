using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // متغیر برای نگهداری ارجاع به Text UI
    private int score = 0; // متغیر برای نگهداری امتیاز

    void Start()
    {
        UpdateScoreText(); // به‌روزرسانی متن در شروع بازی
    }

    public void IncrementScore()
    {
        score++; // افزایش امتیاز
        UpdateScoreText(); // به‌روزرسانی متن
    }

    private void UpdateScoreText()
    {
        scoreText.text="Score:" + score.ToString();
    }
    public int NumberScore()
    {
        return score;
    }
}
