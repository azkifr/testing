using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Fungsi Singleton
    private static LevelManager _instance = null;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {

                _instance = FindObjectOfType<LevelManager>();

            }
            return _instance;
        }
    }

    [SerializeField] private Transform _towerUIParent;
    [SerializeField] private GameObject _towerUIPrefab;
    [SerializeField] private AngelTower[] _towerPrefabs;
    private List<AngelTower> _spawnedTowers = new List<AngelTower> ();

    private void Start()
    {
        InstantiateAllTowerUI();
    }

    // Menampilkan seluruh Tower yang tersedia pada UI Tower Selection
    private void InstantiateAllTowerUI()
    {
        foreach (AngelTower tower in _towerPrefabs)
        {
            GameObject newTowerUIObj = Instantiate(_towerUIPrefab.gameObject, _towerUIParent);
            AngelTowerUI newTowerUI = newTowerUIObj.GetComponent<AngelTowerUI>();
            newTowerUI.SetTowerPrefab(tower);
            newTowerUI.transform.name = tower.name;
        }
    }
    
    public void RegisterSpawnedTower (AngelTower tower)
    {
        _spawnedTowers.Add (tower);
    }
}
