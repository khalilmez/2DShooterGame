using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject winMenu;
    [SerializeField]
    GameObject gameOverMenu;
    [SerializeField]
    private Text UITextEnemiesLeftText;
    public void TogglePause()
    {
        if (Time.timeScale != 0)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        UITextEnemiesLeftText.text = "Enemies left : " + GameManager.Instance.EnemiesLeft;
        if (GameManager.Instance.EnemiesLeft <= 0)
        {
            winMenu.SetActive(true);
            GameManager.Instance.WonGame();
        }
        if (GameManager.Instance.State == GameState.GameOver)
        {
            gameOverMenu.SetActive(true);
            GameManager.Instance.WonGame();
        }
    }
}
