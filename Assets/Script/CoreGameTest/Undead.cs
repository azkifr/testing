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
    [SerializeField] private float _maxHealth = 1;
    [SerializeField] private float _moveSpeed = 0.5f;
    // [SerializeField] private SpriteRenderer _healthBar;
    // [SerializeField] private SpriteRenderer _healthFill;
    [SerializeField] private GameObject Range;
    [SerializeField] public HealthBar _healthBar;

    private Angel _targetAngel;
    private Animator anim;

    private float _attackPower = 1;
    public float _currentHealth;
    public Vector3 TargetPosition { get; private set; }
    public int CurrentPathIndex { get; private set; }
    

    public bool isStop;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        //Undead[] allUndeads = GameObject.FindObjectsOfType<Undead>();
        //foreach (Undead undead in allUndeads)
        //{
        //    _currentUndead = undead;
        //}
        FindClosestAngel();
        
        if (_targetAngel.Range.activeSelf == true)
        {
            Range.SetActive(true);
        }
        
    }

    // Fungsi ini terpanggil sekali setiap kali menghidupkan game object yang memiliki script ini
    private void OnEnable()
    {
        _currentHealth = _maxHealth;

        // _healthFill.size = _healthBar.size;
    }

    public void MoveToTarget()
    {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _moveSpeed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
        // _healthBar.transform.parent = null;

        // Mengubah rotasi dari enemy
        Vector3 distance = TargetPosition - transform.position;
            // Menghadap kanan (default)
            if (distance.x > 0)
            {
                SoundManagerScript.PlaySound ("WalkUndead");

                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

            }

            // Menghadap kiri
            //else
            //{

            //    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            //}

        // _healthBar.transform.parent = transform;
    }

    public void ReduceUndeadHealth(float damage)
    {
        _currentHealth -= damage;
        float health = _currentHealth/_maxHealth;
        _healthBar.SetSize(health);
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

    void FindClosestAngel()
    {
        float distanceToClosestAngel = Mathf.Infinity;
        Angel closestAngel = null;
        Angel[] allAngels = GameObject.FindObjectsOfType<Angel>();

        foreach (Angel currentAngel in allAngels)
        {
           
            float distanceToAngel = (currentAngel.transform.position - this.transform.position).sqrMagnitude;
            if (currentAngel.Range.activeSelf == false)
            {
                continue;
            }
            if (currentAngel == null)
            {
                return;

            }
            if (distanceToAngel < distanceToClosestAngel)
            {
                distanceToClosestAngel = distanceToAngel;
                closestAngel = currentAngel;

                _targetAngel = closestAngel;
            }
        }
        if (closestAngel != null)
        {
            //Debug.DrawLine(this.transform.position, closestAngel.transform.position);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Leader")
        {
            isStop = true;
            // anim.SetTrigger("Attack");
            // UndeadMeleeAttack[] OnTriggerStay2D;
            // Debug.Log("Undead Hit Leader");
        }
    }
}
