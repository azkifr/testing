using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void SwitchSceneToTestMenu()
    {
        SceneManager.LoadScene("TestMenu");
    }
    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
