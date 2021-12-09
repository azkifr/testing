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
    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    
    public void QuitGame ()
    {
        Application.Quit();
    }
}
