using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    
    public void SwitchSceneToTestMenu()
    {
        SceneManager.LoadScene("CoreGameTest");
    }
    public void RegisterLevelType(int type)
    {
        MapManager.Instance.RegisterEnemyCount(type);
    }
    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
    }
    
    public void PauseScene(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void ContinueScene(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}
