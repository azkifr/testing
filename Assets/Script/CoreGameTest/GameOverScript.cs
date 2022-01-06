using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]private GameObject panelWin;
    [SerializeField] private GameObject panelLose;

    public int _LeaderHealth = 1;
    
    //enemy dmg properti
    public int atk=1;
    private int count=0;//jumlah enemy attack
    public float _attackDelay = 1f;
    private float lastSwing=-9999f;
    private bool triggered;

    private void Update()
    {
        if (triggered)
        {
            Debug.Log(_LeaderHealth);
            //if (Time.time >= lastSwing)
            //{
            //    lastSwing = Time.time + _attackDelay;
            //    ReduceLeaderHealth(atk);
            //}
            //else
            //{
            //    lastSwing = Time.time + _attackDelay;
            //}
            if (Time.time > lastSwing + _attackDelay)
            {
                ReduceLeaderHealth(atk*count);
                lastSwing = Time.time + _attackDelay;
            }
        }

        if (!GameObject.FindWithTag("Undead")&&MapManager.Instance.EnemyCount==0)
        {
            EnableGameOver(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Undead")
        {
            triggered = true;
            count++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Undead")
        {
            triggered = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    public void ReduceLeaderHealth(int damage)
    {
        _LeaderHealth -= damage;
        Debug.Log(_LeaderHealth);
        if (_LeaderHealth <= 0)
        {
            Debug.Log("RIP");
            gameObject.SetActive(false);
            EnableGameOver(2);
        }
    }
    public void EnableGameOver(int num)
    {
        Time.timeScale = 0;
        if (num == 1)
        {
            panelWin.SetActive(true);
        }
        else if (num == 2)
        {
            panelLose.SetActive(true);
        }
    }
}
