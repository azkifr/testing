using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglePlacement : MonoBehaviour
{
    private AngelTower _placedTower;
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (_placedTower != null)
        {
            return;
        }
        AngelTower tower = collision.GetComponent<AngelTower>();

        if (tower != null)
        {
            tower.SetPlacePosition (transform.position);
            _placedTower=tower;
        }
    }
    private void OnTriggerExit2D (Collider2D collision)
    {
        if (_placedTower==null)
        {
            return;
        }
        _placedTower.SetPlacePosition (null);
        _placedTower=null;
    }
}
