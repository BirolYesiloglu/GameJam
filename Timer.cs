using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public float timeLeft = 15.0f;
    public Text countdownText; // Assign your UI Text element in the inspector
    public Text scoreText; // Assign your UI Text element for score in the inspector
    public Text highScoreText; // Assign your UI Text element for high score in the inspector

    void Start()
    {
        // Load the high score from PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
            int score = int.Parse(scoreText.text); // Parse the score from the scoreText
            if (score >= 10)
            {
                SceneManager.LoadScene("Win"); // Load the win scene
            }
            else
            {
                SceneManager.LoadScene("Lose"); // Load the lose scene
            }
            // If the current score is higher than the high score, update the high score
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
                highScoreText.text = score.ToString();
            }
        }

        countdownText.text = Mathf.Round(timeLeft).ToString();
    }
}
