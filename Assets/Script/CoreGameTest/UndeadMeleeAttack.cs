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
    private float _attackDelay = 1.0f;
    private float lastSwing;

    private Angel _targetAngel;
    private Undead _currentUndead;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
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
            
            if (AngelMeleeAttack.Instance.StopAttack == true||AngelMeleeAttack.Instance.StopAttack==null)
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
            Debug.Log(collision.name);
            Undead.Instance.isStop = true;
            if (Time.time >= lastSwing)
            {
                Debug.Log("Hit");
                lastSwing = Time.time + _attackDelay;
                collision.SendMessage("ReduceAngelHealth", _attackPower);
                if (collision.gameObject.activeSelf==false)
                {
                    Debug.Log("AngelDead");
                    Undead.Instance.isStop = false;
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
            Debug.DrawLine(this.transform.position, closestAngel.transform.position);
        }
    }
}
