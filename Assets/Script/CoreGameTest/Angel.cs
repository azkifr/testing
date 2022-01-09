using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    //private static Angel _instance = null;

    //public static Angel Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<MapManager>();
    //        }
    //        return _instance;
    //    }
    //}
    // Tower Component
    [SerializeField] private SpriteRenderer _angelPlace;
    [SerializeField] private SpriteRenderer _angelHead;
    // Tower Properties
    [SerializeField] private float _shootPower = 1;
    [SerializeField] private float _shootDistance = 1f;
    [SerializeField] private float _shootDelay = 5f;
    [SerializeField] private float _bulletSpeed = 1f;
    [SerializeField] private float _bulletSplashRadius = 0f;
    [SerializeField] private float _maxHealth = 1;
    [SerializeField] public float _angelHealth = 1;
    [SerializeField] public double _angelCost = 10;

    [SerializeField] public GameObject Range;
    [SerializeField] private AngelRangeAttack _rangePrefab;
    [SerializeField] public HealthBar _healthBar;

    //Script angel range

    private float _runningShootDelay;
    private Undead _targetUndead;
    private Quaternion _targetRotation;
    private Animator anim;

    private void Start()
    {
        anim = _angelHead.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _angelHealth = _maxHealth;

        // _healthFill.size = _healthBar.size;
    }
    // Mengecek musuh terdekat
    public void CheckNearestUndead(List<Undead> undeads)
    {
        //Debug.Log("Checking");
        if (_targetUndead != null)
        {
            if (!_targetUndead.gameObject.activeSelf || Vector3.Distance(transform.position, _targetUndead.transform.position) > _shootDistance)
            {

                _targetUndead = null;

            }
            else
            {

                return;

            }
        }
        //Debug.Log("Checking - 2");

        float nearestDistance = Mathf.Infinity;
        Undead nearestUndead = null;

        foreach (Undead undead in undeads)
        {
            //Debug.Log("Checking - 3");
            float distance = Vector3.Distance(transform.position, undead.transform.position);
            if (distance > _shootDistance)
            {
                //Debug.Log("Check - 4.1");
                //Debug.Log(distance);
                continue;

            }
            if (distance < nearestDistance)
            {
                //Debug.Log("Check - 4.2");
                nearestDistance = distance;

                nearestUndead = undead;
            }
        }

        _targetUndead = nearestUndead;
        //Debug.Log(_targetUndead.name);
    }



    // Menembak musuh yang telah disimpan sebagai target
    public void ShootTarget()
    {
        //Debug.Log("Shoot");
        if (_targetUndead == null)
        {
            
            return;

        }

        _runningShootDelay -= Time.unscaledDeltaTime;
        anim.SetTrigger("Attack");
        if (_runningShootDelay <= 0f)
        {
            //bool headHasAimed = Mathf.Abs(_angelHead.transform.rotation.eulerAngles.z - _targetRotation.eulerAngles.z) < 10f;
            //if (!headHasAimed)
            //{

            //    return;

            //}

            AngelRangeAttack bullet = MapManager.Instance.GetArrowFromPool(_rangePrefab);

            bullet.transform.position = transform.position;
            bullet.SetProperties(_shootPower, _bulletSpeed, _bulletSplashRadius);
            bullet.SetTargetUndead(_targetUndead);
            //Debug.Log(_targetUndead.name);
            bullet.gameObject.SetActive(true);

            _runningShootDelay = _shootDelay;

        }

    }



    // Membuat tower selalu melihat ke arah musuh

    public void SeekTarget()
    {
        if (_targetUndead == null)
        {

            return;

        }

        Vector3 direction = _targetUndead.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _targetRotation = Quaternion.Euler(new Vector3(0f, 0f, targetAngle - 90f));


        //_angelHead.transform.rotation = Quaternion.RotateTowards(_angelHead.transform.rotation, _targetRotation, Time.deltaTime * 180f);
    }

    //=======
    public Vector2? PlacePosition { get; private set; }
    public bool IsDead;

    public void SetPlacePosition(Vector2? newPosition)
    {
        PlacePosition = newPosition;
    }
    public void LockPlacement()
    {
        transform.position = (Vector2)PlacePosition;
    }

    // Mengubah order in layer pada tower yang sedang di drag
    public void ToggleOrderInLayer(bool toFront)
    {
        int orderInLayer = toFront ? 2 : 0;
        _angelPlace.sortingOrder = orderInLayer;
        _angelHead.sortingOrder = orderInLayer;
    }

    // Fungsi yang digunakan untuk mengambil sprite pada Tower Head
    public Sprite GetAngelHeadIcon()
    {
        return _angelHead.sprite;
    }

    public void ReduceAngelHealth(float damage)
    {
        _angelHealth -= damage;
        float health = _angelHealth/_maxHealth;
        _healthBar.SetSize(health);
        if (_angelHealth <= 0)
        {
            // anim.SetTrigger("Dead");
            Debug.Log("RIP");
            gameObject.SetActive(false);
            
        }
    }
}
