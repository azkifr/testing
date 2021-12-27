using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]private GameObject panel;


    public int _LeaderHealth = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.tag == "Undead")
        // {
        //    Debug.Log("Enemy Pass");
        //    panel.SetActive(true);
        //    Time.timeScale = 0;
        // }
    }
    // private void OnCollide2D(Collider2D collision)
    // {
    //     if (collision.tag == "Undead")
    //     {
    //         Debug.Log("Enemy Pass");
    //         panel.SetActive(true);
    //         Time.timeScale = 0;
    //     }
    // }
    
    public void ReduceLeaderHealth(int damage)
    {
        _LeaderHealth -= damage;
        if (_LeaderHealth <= 0)
        {
            Debug.Log("RIP");
            gameObject.SetActive(false);
            Time.timeScale = 0;
            panel.SetActive(true);
        }
    }
}
