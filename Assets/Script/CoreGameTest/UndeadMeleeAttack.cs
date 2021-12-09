using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadMeleeAttack : MonoBehaviour
{
    public ContactFilter2D filter;
    private CircleCollider2D circleCollider2D;
    private Collider2D[] hits = new Collider2D[10];
    private SpriteRenderer spriteRenderer;

    //Attack
    private int _attackPower = 1;
    //Swing
    private float _attackDelay = 0.01f;
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
            undead.isStop = _currentUndead.isStop;
        }

        circleCollider2D.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            FindClosestAngel();
            //OnCollide(hits[i]);
            //clean up array manual
            hits[i] = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Melee")
        {
            Debug.Log(collision.name);
            if (Time.time >= lastSwing)
            {
                Debug.Log("Hit");
                _currentUndead.isStop = true;
                lastSwing = Time.time + _attackDelay;
                collision.SendMessage("ReduceAngelHealth", _attackPower);
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
        Debug.DrawLine(this.transform.position, closestAngel.transform.position);
    }
}
