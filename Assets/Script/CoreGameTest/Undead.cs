using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead : MonoBehaviour
{
    //private static Undead _instance = null;

    //public static Undead Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<Undead>();
    //        }
    //        return _instance;

    //    }
    //}
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private SpriteRenderer _healthBar;
    [SerializeField] private SpriteRenderer _healthFill;

    public int _currentHealth;
    public Vector3 TargetPosition { get; private set; }
    public int CurrentPathIndex { get; private set; }

    public bool isStop;

    private void Update()
    {
        
    }

    // Fungsi ini terpanggil sekali setiap kali menghidupkan game object yang memiliki script ini
    private void OnEnable()
    {
        _currentHealth = _maxHealth;

        _healthFill.size = _healthBar.size;
    }

    public void MoveToTarget()
    {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _moveSpeed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
        _healthBar.transform.parent = null;

        // Mengubah rotasi dari enemy
        Vector3 distance = TargetPosition - transform.position;
            // Menghadap kanan (default)
            if (distance.x > 0)
            {

                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

            }

            // Menghadap kiri
            else
            {

                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            }

        _healthBar.transform.parent = transform;
    }

    public void ReduceUndeadHealth(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            
            gameObject.SetActive(false);
        }
    }

    // Menandai indeks terakhir pada path
    public void SetCurrentPathIndex(int currentIndex)
    {
        CurrentPathIndex = currentIndex;
    }

}
