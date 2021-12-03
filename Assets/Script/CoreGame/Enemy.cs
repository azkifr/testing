using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy Properti
    [SerializeField] private int _shootPower = 1;
    [SerializeField] private float _shootDistance = 1f;
    [SerializeField] private float _shootDelay = 5f;
    [SerializeField] private float _bulletSpeed = 1f;
    [SerializeField] private float _bulletSplashRadius = 0f;
    [SerializeField] private EnemyAttack _bulletPrefab;

    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private SpriteRenderer _healthBar;
    [SerializeField] private SpriteRenderer _healthFill;

    private float _runningShootDelay;
    private AngelTower _targetAngel;
    private int _currentHealth;
    private Quaternion _targetRotation;
    public bool stopMove;

    public Vector3 TargetPosition { get; private set; }

    public int CurrentPathIndex { get; private set; }

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
        if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
        {
            // Menghadap atas
            if (distance.y > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }
            // Menghadap bawah
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
            }
        }

        else
        {
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
        }
        _healthBar.transform.parent = transform;
    }

    public void ReduceEnemyHealth(int damage)
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

    //Mengecek Angel terdekat
    public void CheckNearestAngel(List<AngelTower> angels)
    {
        if (_targetAngel != null)
        {
            if (!_targetAngel.gameObject.activeSelf || Vector3.Distance(transform.position, _targetAngel.transform.position) > _shootDistance)
            {
                _targetAngel = null;
            }
            else
            {
                return;
            }
        }

        float nearestDistance = Mathf.Infinity;
        AngelTower nearestAngel = null;

        foreach (AngelTower angel in angels)
        {
            float distance = Vector3.Distance(transform.position, angel.transform.position);
            if (distance > _shootDistance)
            {
                continue;
            }

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestAngel = angel;
            }
        }

        _targetAngel = nearestAngel;
    }

    // Menembak musuh yang telah disimpan sebagai target
    public void ShootTarget()
    {
        if (_targetAngel == null)
        {
            return;
        }

        _runningShootDelay -= Time.unscaledDeltaTime;
        if (_runningShootDelay <= 0f)
        {
            //bool headHasAimed = Mathf.Abs(_towerHead.transform.rotation.eulerAngles.z - _targetRotation.eulerAngles.z) < 10f;
            //if (!headHasAimed)
            //{
            //    return;
            //}

            EnemyAttack bullet = LevelManager.Instance.GetEnemyBulletFromPool(_bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.SetProperties(_shootPower, _bulletSpeed, _bulletSplashRadius);
            bullet.SetTargetAngel(_targetAngel);
            bullet.gameObject.SetActive(true);

            _runningShootDelay = _shootDelay;
        }
    }

    //Hentikan gerak Enemy jika ada Angel
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            stopMove = true;
            Debug.Log("Collide");
        }
    }
    void Update()
    {
        if (_targetAngel == null)
        {
            Debug.Log("EnemyDed");
        }
    }
}
