using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngelTowerUI : MonoBehaviour
{
    [SerializeField] private Image _towerIcon;

    private AngelTower _towerPrefab;

    public void SetTowerPrefab(AngelTower tower)
    {
        _towerPrefab = tower;

        _towerIcon.sprite = tower.GetTowerHeadIcon();
    }
}
