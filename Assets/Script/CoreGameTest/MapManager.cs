using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    // Fungsi Singleton

    private static MapManager _instance = null;

    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapManager>();
            }
            return _instance;
        }
    }
    //Sistem Generate Energi

    // Fungsi [Range (min, max)] ialah menjaga value agar tetap berada di antara min dan max-nya 
    [Range(0f, 1f)]
    public float AutoCollectPercentage = 0.1f;

    public Text GoldInfo;
    private float _collectSecond;
    public double _totalGold;

    //======================

    [SerializeField] private Transform _angelUIParent;
    [SerializeField] private GameObject _angelUIPrefab;
    [SerializeField] private Angel[] _angelPrefabs;
    [SerializeField] private Undead[] _undeadPrefabs;
    [SerializeField] private Transform[] _undeadPaths;

    [SerializeField] private float _spawnDelay = 5f;
    private List<Angel> _spawnedAngels = new List<Angel>();
    private List<Undead> _spawnedUndeads = new List<Undead>();
    private List<AngelMeleeAttack> _MeleeAttackRange = new List<AngelMeleeAttack>();

    private float _runningSpawnDelay;
    public int EnemyCount=1;


    private void Start()
    {
        InstantiateAllAngelUI();
    }

    private void Update()
    {
        // Fungsi untuk selalu mengeksekusi CollectPerSecond setiap detik 
        _collectSecond += Time.unscaledDeltaTime;
        if (_collectSecond >= 1f)
        {
            CollectPerSecond();
            _collectSecond = 0f;
        }

        // Counter untuk spawn enemy dalam jeda waktu yang ditentukan
        // Time.unscaledDeltaTime adalah deltaTime yang independent, tidak terpengaruh oleh apapun kecuali game object itu sendiri,
        // jadi bisa digunakan sebagai penghitung waktu


        _runningSpawnDelay -= Time.unscaledDeltaTime;
        if (_runningSpawnDelay <= 0f)
        {
            if (EnemyCount > 0)
            {
                SpawnUndead();
                EnemyCount--;
            }
            _runningSpawnDelay = _spawnDelay;
        }

        foreach (Undead undead in _spawnedUndeads)
        {
            if (!undead.gameObject.activeSelf)
            {
                continue;
            }

            // Kenapa nilainya 0.1? Karena untuk lebih mentoleransi perbedaan posisi,
            // akan terlalu sulit jika perbedaan posisinya harus 0 atau sama persis
            if (Vector2.Distance(undead.transform.position, undead.TargetPosition) < 0.1f)
            {
                undead.SetCurrentPathIndex(undead.CurrentPathIndex + 1);
                if (undead.CurrentPathIndex < _undeadPaths.Length)
                {
                    undead.SetTargetPosition(_undeadPaths[undead.CurrentPathIndex].position);
                }

                else
                {
                    undead.gameObject.SetActive(false);
                }
            }

            else
            {
                //Debug.Log(undead.isStop);
                if (undead.isStop == true)
                {
                    //Debug.Log("Stop");
                    undead.transform.Translate(new Vector3(0, 0, 0));

                }
                else
                {
                    undead.MoveToTarget();
                }
            }
        }
    }

    private void CollectPerSecond()
    {
        double output = 10;//1 gold per detik
        output *= AutoCollectPercentage;
        AddGold(output);
    }

    private void AddGold(double value)
    {
        _totalGold += value;
        GoldInfo.text = $"Gold: { _totalGold.ToString("0") }";
    }

    // Menampilkan seluruh Tower yang tersedia pada UI Tower Selection
    private void InstantiateAllAngelUI()
    {
        foreach (Angel angel in _angelPrefabs)
        {
            GameObject newAngelUIObj = Instantiate(_angelUIPrefab.gameObject, _angelUIParent);
            AngelUI newAngelUI = newAngelUIObj.GetComponent<AngelUI>();
 
            newAngelUI.SetAngelPrefab(angel);
            newAngelUI.transform.name = angel.name;
        }
    }

    // Mendaftarkan Tower yang di-spawn agar bisa dikontrol oleh LevelManager
    public void RegisterSpawnedAngel(Angel angel)
    {
        _totalGold -= angel._angelCost;
        _spawnedAngels.Add(angel);
        //angel.AttackScript.enabled = true;
        //GameObject.Find("Sword").GetComponent<AngelMeleeAttack>().enabled = true;
    }
    private void SpawnUndead()
    {
        int randomIndex = Random.Range(0, _undeadPrefabs.Length);
        string undeadIndexString = (randomIndex + 1).ToString();
        GameObject newUndeadObj = _spawnedUndeads.Find(
            e => !e.gameObject.activeSelf && e.name.Contains(undeadIndexString)
        )?.gameObject;

        if (newUndeadObj == null)
        {
            newUndeadObj = Instantiate(_undeadPrefabs[randomIndex].gameObject);
        }

        Undead newUndead = newUndeadObj.GetComponent<Undead>();
        if (!_spawnedUndeads.Contains(newUndead))
        {
            _spawnedUndeads.Add(newUndead);
        }

        newUndead.transform.position = _undeadPaths[0].position;
        newUndead.SetTargetPosition(_undeadPaths[1].position);
        newUndead.SetCurrentPathIndex(1);
        newUndead.gameObject.SetActive(true);
    }

    //public AngelMeleeAttack GetAttack

    // Untuk menampilkan garis penghubung dalam window Scene
    // tanpa harus di-Play terlebih dahulu
    private void OnDrawGizmos()
    {
        for (int i = 0; i < _undeadPaths.Length - 1; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(_undeadPaths[i].position, _undeadPaths[i + 1].position);
        }
    }

}
