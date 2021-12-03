using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyStop : MonoBehaviour
{
    public bool stop;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 8)
        {
            stop = true;
            Debug.Log("Collide");
        }
        else
        {
            stop = false;
        }
        //if (_placedTower != null)
        //{
        //    return;
        //}
        //AngelTower tower = collision.GetComponent<AngelTower>();

        //if (tower != null)
        //{
        //    tower.SetPlacePosition(transform.position);
        //    _placedTower = tower;
        //}
    }
}
