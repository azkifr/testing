using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelRangeAttack : MonoBehaviour
{
    private float _bulletPower;
    private float _bulletSpeed;
    private float _bulletSplashRadius;

    private Undead _targetUndead;

    // FixedUpdate adalah update yang lebih konsisten jeda pemanggilannya
    // cocok digunakan jika karakter memiliki Physic (Rigidbody, dll)

    private void FixedUpdate()
    {
        if (_targetUndead != null)
        {
            if (!_targetUndead.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                _targetUndead = null;
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _targetUndead.transform.position, _bulletSpeed * Time.fixedDeltaTime);

            Vector3 direction = _targetUndead.transform.position - transform.position;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, targetAngle - 90f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (_targetUndead == null)
        {

            return;

        }

        if (collision.gameObject.Equals(_targetUndead.gameObject))
        //if(collision.gameObject.tag=="Undead")
        {
            Debug.Log("collide");
            gameObject.SetActive(false);

            // Bullet yang memiliki efek splash area
            if (_bulletSplashRadius > 0f)
            {
                MapManager.Instance.ExplodeAt(transform.position, _bulletSplashRadius, _bulletPower);
            }
            //Bullet yang hanya single-target
            else
            {
                _targetUndead.ReduceUndeadHealth(_bulletPower);
            }

            _targetUndead = null;
        }
    }



    public void SetProperties(float bulletPower, float bulletSpeed, float bulletSplashRadius)
    {

        _bulletPower = bulletPower;

        _bulletSpeed = bulletSpeed;

        _bulletSplashRadius = bulletSplashRadius;
        //Debug.Log(_bulletPower);
    }



    public void SetTargetUndead(Undead undead)
    {
        //Debug.Log(undead.name);
        _targetUndead = undead;

    }
}
