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
    public void SwitchToMap(int lvl)
    {
        Time.timeScale = 1;
        
        if (lvl == 1)
        {
            SceneManager.LoadScene("Map-1");
        }
        else if (lvl == 2)
        {
            SceneManager.LoadScene("Map-2");
        }
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
