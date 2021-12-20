using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]private GameObject panel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Undead")
        {
            Debug.Log("Enemy Pass");
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
