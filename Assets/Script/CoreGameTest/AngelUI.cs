using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AngelUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    [SerializeField] private Image _angelIcon;

    private Angel _angelPrefab;
    private Angel _currentSpawnedAngel;
    public bool StopAttack;

    public void SetAngelPrefab(Angel angel)
    {

        _angelPrefab =angel;

        _angelIcon.sprite = angel.GetAngelHeadIcon();
        
        
    }

    // Implementasi dari Interface IBeginDragHandler
    // Fungsi ini terpanggil sekali ketika pertama men-drag UI
    public void OnBeginDrag(PointerEventData eventData)
    {
        _angelPrefab._CardSlot.gameObject.SetActive(false);
        GameObject newTowerObj = Instantiate(_angelPrefab.gameObject);
        _currentSpawnedAngel = newTowerObj.GetComponent<Angel>();
        _currentSpawnedAngel.ToggleOrderInLayer(true);

        if (_currentSpawnedAngel.tag == "Melee")
            _currentSpawnedAngel.Range.SetActive(false);
            SoundManagerScript.PlaySound ("Select");
    }

    // Implementasi dari Interface IDragHandler
    // Fungsi ini terpanggil selama men-drag UI
    public void OnDrag(PointerEventData eventData)
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        _currentSpawnedAngel.transform.position = targetPosition;

        if(_currentSpawnedAngel.tag=="Melee")
            _currentSpawnedAngel.Range.SetActive(false);
    }

    // Implementasi dari Interface IEndDragHandler
    // Fungsi ini terpanggil sekali ketika men-drop UI ini
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentSpawnedAngel.PlacePosition == null||_currentSpawnedAngel._angelCost>MapManager.Instance._totalGold)
        {
            if (_currentSpawnedAngel.tag == "Melee")
                _currentSpawnedAngel.Range.SetActive(false);
            Destroy(_currentSpawnedAngel.gameObject);
            
        }
        else
        {
            if (_currentSpawnedAngel.tag == "Melee")
                _currentSpawnedAngel.Range.SetActive(true);
                SoundManagerScript.PlaySound ("DropCharacter");

            _currentSpawnedAngel.LockPlacement();
            _currentSpawnedAngel.ToggleOrderInLayer(false);
            MapManager.Instance.RegisterSpawnedAngel(_currentSpawnedAngel);
            _currentSpawnedAngel = null;
            
        }
    }

}
