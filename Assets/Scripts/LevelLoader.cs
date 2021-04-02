using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 3f;
    [SerializeField] float startMenuDelayInSeconds = 4f;
    Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if  (currentScene.buildIndex == 0)
        {
            StartCoroutine(StartMenu(startMenuDelayInSeconds));
        }
    }
    public void LoadStartMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
        //FindObjectOfType<GameSession>().ResetScore();
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void LoadOptionsMenu()
    {
        Debug.Log(0);
        SceneManager.LoadScene("OptionsScreen");
    }
    public void LoadGameOver()
    {
        StartCoroutine(GameOver(delayInSeconds));
    }
    IEnumerator StartMenu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoadStartMenu();
    }
    IEnumerator GameOver(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Game Over");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
