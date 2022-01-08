using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadMeleeAttack : MonoBehaviour
{
    private static UndeadMeleeAttack _instance = null;

    public static UndeadMeleeAttack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UndeadMeleeAttack>();
            }
            return _instance;

        }
    }
    public ContactFilter2D filter;
    private CircleCollider2D circleCollider2D;
    private Collider2D[] hits = new Collider2D[10];
    private SpriteRenderer spriteRenderer;

    //Attack
    private float _attackPower = 40;
    //Swing
    [SerializeField]public float _attackDelay = 2f;
    private float lastSwing;

    private Angel _targetAngel;
    private Undead _currentUndead;
    private GameOverScript _currentLeader;
    private Animator anim;
    private bool allowAttack;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        _currentLeader = GetComponent<GameOverScript>();
    }
    private void Update()
    {

        Undead[] allUndeads = GameObject.FindObjectsOfType<Undead>();
        foreach (Undead undead in allUndeads)
        {
            _currentUndead = undead;
        }

        circleCollider2D.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            FindClosestAngel();
            //lastSwing -= Time.unscaledDeltaTime;
            //if (lastSwing <= 0f)
            //{
            OnCollide(hits[i]);
            //    lastSwing = _attackDelay;
            //}
           
            //clean up array manual
            hits[i] = null;
        }
    }
    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.tag == "Leader")
    //     {
    //         if (Time.time >= lastSwing)
    //         {
    //             lastSwing = Time.time + _attackDelay;
    //             Debug.Log("Undead Hit Leader");
    //             _currentLeader.ReduceLeaderHealth(_attackPower);
    //             if (collision.gameObject.activeSelf == false || _currentLeader == null)
    //             {
    //                 Debug.Log("Leader Dead");
    //                 _currentUndead.isStop = false;
    //             }
    //         }
    //         else
    //         {
    //             lastSwing = Time.time + _attackDelay;
    //         }
    //     }
    // }
    private void OnCollide(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.tag == "Melee")
        {
            //Debug.Log(lastSwing+" "+Time.time);
            _currentUndead.isStop = true;
            lastSwing -= Time.unscaledDeltaTime;
            if (lastSwing<=0f)
            {
                Debug.Log("Cooldown done");
                if (_targetAngel.Range.activeSelf == true)
                {
                    Debug.Log("Undead Hit");
                    //lastSwing = Time.time + _attackDelay;
                    _targetAngel.ReduceAngelHealth(_attackPower);
                    if (collision.gameObject.activeSelf == false||_targetAngel==null)
                    {
                        Debug.Log("AngelDead");
                        _currentUndead.isStop = false;
                    } 
                }
                lastSwing = _attackDelay;
            }
            //else if(Time.time<lastSwing)
            //{
            //    lastSwing = Time.time ;
            //}
        }
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
                if (closestAngel.Range.activeSelf == true)
                {
                    _targetAngel = closestAngel;
                }
            }
        }
        if (closestAngel != null)
        {
            Debug.DrawLine(this.transform.position, closestAngel.transform.position);
        }
    }
    void AttackDelay()
    {
        allowAttack = true;
        Debug.Log("AttackDelaying");
    }
}
