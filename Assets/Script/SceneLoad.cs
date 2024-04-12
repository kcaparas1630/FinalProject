using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public static SceneLoad instance;
    [SerializeField] Animator transitionAnim;
    [SerializeField] GameObject image;
    [SerializeField] PlayableDirector gameStart;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1;
    }
    public void OnRepeatScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void OnNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    public void OnApplicationExit()
    {
        Application.Quit();
    }
  
}
