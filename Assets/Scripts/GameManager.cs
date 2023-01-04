using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private RectTransform pausePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    // Update is called once per frame
    void Update()
    {
        if (PlayerStat.life <= 0)
        {
            End();
        }
    }

    private void End()
    {
        if (Spawn.Instance.GetWave() > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Spawn.Instance.GetWave());
        }

        highScoreText.text = $"High Score : {PlayerPrefs.GetInt("HighScore", 0)} Wave";
        scoreText.text = $"Score : {Spawn.Instance.GetWave()} Wave";

        gameOverPanel.gameObject.SetActive(true);

    }

    public virtual void Pause()
    {
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
    }

    public virtual void UnPause()
    {
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
