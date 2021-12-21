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
    private int _attackPower = 1;
    //Swing
    public float _attackDelay = 0.5f;
    private float lastSwing;

    private Angel _targetAngel;
    private Undead _currentUndead;
    private Animator anim;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
            
            OnCollide(hits[i]);
            //clean up array manual
            hits[i] = null;
        }
    }
    private void OnCollide(Collider2D collision)
    {
        
        if (collision.tag == "Melee")
        {
            //Debug.Log(collision.name);
            _currentUndead.isStop = true;
            if (Time.time >= lastSwing)
            {
                if (_targetAngel.Range.activeSelf == true)
                {
                    Debug.Log("Undead Hit");
                    lastSwing = Time.time + _attackDelay;
                    _targetAngel.ReduceAngelHealth(_attackPower);
                    if (collision.gameObject.activeSelf == false||_targetAngel==null)
                    {
                        Debug.Log("AngelDead");
                        _currentUndead.isStop = false;
                    }
                }
            }
            else
            {
                lastSwing = Time.time + _attackDelay;
            }
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
        //if (closestAngel != null)
        //{
        //    Debug.DrawLine(this.transform.position, closestAngel.transform.position);
        //}
    }

}
