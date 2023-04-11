using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int numberOfEnemies = 8;
    [HideInInspector]public float isWin = -1;
    public static GameManager gameManager { get; private set; }

    [SerializeField] UIManager UIM;


    public Transform PlayerReference;

    [SerializeField] AudioSource backgroundMusic;


    public enum GameStatus
    {
        gameStart,
        gameRunning,
        gamePause,
        inMenu
    }

    [SerializeField] public GameStatus gameStatus;

    [SerializeField] float deadUISeconds = 2f;




    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }
    void Update()
    {
        if (isWin == 0)
        {
            StartCoroutine(FinishGame(false));
        }
        else if (isWin == 1)
        {
            StartCoroutine(FinishGame(true));
        }

        if (gameStatus == GameStatus.inMenu)
        {
            UIM.ShowStartMenu();
            Time.timeScale = 0;
        }
        else if (gameStatus == GameStatus.gameStart)
        {
            gameStatus = GameStatus.gameRunning;
            Time.timeScale = 1;
            backgroundMusic.Play();
        }
        else if (gameStatus == GameStatus.gamePause)
        {

            Time.timeScale = 0;
            backgroundMusic.Pause();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && gameStatus == GameStatus.gameRunning)
        {
            gameStatus = GameStatus.gamePause;
            UIM.InMenuCanvas.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gameStatus == GameStatus.gamePause)
        {
            gameStatus = GameStatus.gameRunning;
            UIM.InMenuCanvas.SetActive(false);
            Time.timeScale = 1;
            backgroundMusic.Play();
        }
    }

    //UI
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        gameStatus = GameStatus.gameStart;
        UIM.StartGameCanvas.SetActive(false);
    }

    public void ResumeGame()
    {
        gameStatus = GameStatus.gameRunning;
        UIM.InMenuCanvas.SetActive(false);
        Time.timeScale = 1;
        backgroundMusic.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FinishGame(bool isWin)
    {

        yield return new WaitForSeconds(deadUISeconds);
        backgroundMusic.Stop();
        if (isWin)
        {
            UIM.loseText.SetActive(false);
            UIM.winText.SetActive(true);
        }
        else
        {
            UIM.winText.SetActive(false);
            UIM.loseText.SetActive(true);
        }
        Time.timeScale = 0;
        UIM.StartGameCanvas.SetActive(false);
        UIM.InMenuCanvas.SetActive(false);
        UIM.EndGameCanvas.SetActive(true);
    }
}
