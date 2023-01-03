using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform gameOverPanel;
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

    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Game");
    }
}
