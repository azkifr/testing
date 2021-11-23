using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AngelTowerUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _towerIcon;

    private AngelTower _towerPrefab;
    private AngelTower _currentSpawnedTower;
    public void SetTowerPrefab(AngelTower tower)
    {
        _towerPrefab = tower;
        _towerIcon.sprite = tower.GetTowerHeadIcon();
    }
    public void OnBeginDrag (PointerEventData eventData)
    {
        GameObject newTowerObj = Instantiate (_towerPrefab.gameObject);
        _currentSpawnedTower = newTowerObj.GetComponent<AngelTower> ();
        _currentSpawnedTower.ToggleOrderInLayer (true);
    }
    public void OnDrag (PointerEventData eventData)
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint (mousePosition);
        _currentSpawnedTower.transform.position = targetPosition;
    }
    public void OnEndDrag (PointerEventData eventData)
    {
        if (_currentSpawnedTower.PlacePosition == null)
        {
            Destroy (_currentSpawnedTower.gameObject);
        }
        else
        {
            _currentSpawnedTower.LockPlacement ();
            _currentSpawnedTower.ToggleOrderInLayer (false);
            LevelManager.Instance.RegisterSpawnedTower (_currentSpawnedTower);
            _currentSpawnedTower = null;
        }

    }
}
