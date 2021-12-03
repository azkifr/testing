using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private int _bulletPower;
    private float _bulletSpeed;
    private float _bulletSplashRadius;

    private AngelTower _targetAngel;
    // FixedUpdate adalah update yang lebih konsisten jeda pemanggilannya
    // cocok digunakan jika karakter memiliki Physic (Rigidbody, dll)
    private void FixedUpdate()
    {
        if (_targetAngel != null)
        {
            if (!_targetAngel.gameObject.activeSelf)
            {
                gameObject.SetActive(false);

                _targetAngel = null;

                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, _targetAngel.transform.position, _bulletSpeed * Time.fixedDeltaTime);

            Vector3 direction = _targetAngel.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, targetAngle - 90f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_targetAngel == null)
        {

            return;
        }

        if (collision.gameObject.Equals(_targetAngel.gameObject))
        {
            gameObject.SetActive(false);
            

            // Bullet yang memiliki efek splash area
            if (_bulletSplashRadius > 0f)
            {
                LevelManager.Instance.ExplodeAt(transform.position, _bulletSplashRadius, _bulletPower);
            }
            // Bullet yang hanya single-target
            else
            {
                _targetAngel.ReduceAngelHealth(_bulletPower);
            }

            _targetAngel = null;
        }
    }


    public void SetProperties(int bulletPower, float bulletSpeed, float bulletSplashRadius)
    {
        _bulletPower = bulletPower;

        _bulletSpeed = bulletSpeed;

        _bulletSplashRadius = bulletSplashRadius;
    }

    public void SetTargetAngel(AngelTower angel)
    {
        _targetAngel = angel;
    }
}
