using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMeleeAttack : MonoBehaviour
{
    public ContactFilter2D filter;
    private CircleCollider2D circleCollider2D;
    private Collider2D[] hits = new Collider2D[10];
    private SpriteRenderer spriteRenderer;
    //Attack
    private int _attackPower=1;

    //Swing
    private float _attackDelay = 0.05f;
    private float lastSwing;

    private Undead _targetUndead;
    

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //angel = GetComponent<Angel>();
    }
    
    private void Update()
    {
        //Collision
        circleCollider2D.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            //if (angel.EnableAttack==true)
            //{
                //EnableAttackRange(angel.EnableAttack);
                if (hits[i] == null)
                {
                    continue;
                }
                FindClosestUndead();
                OnCollide(hits[i]);
                //clean up array manual
                hits[i] = null;
            //}
        }
       
    }

    private void FixedUpdate()
    {
        //if (_targetUndead != null)
        //{
        //    if (!_targetUndead.gameObject.activeSelf)
        //    {
        //        gameObject.SetActive(false);
        //        _targetUndead = null;
        //        return;
        //    }
        //}
    }
    public void EnableAttackRange(bool cond)
    {
        if (cond == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void OnCollide(Collider2D collision)
    {
        if (collision.tag == "Undead"&&_targetUndead.gameObject==collision.gameObject)
        {
            
            //Debug.Log("Hit");
            if (Time.time >= lastSwing)
            {
                //Debug.Log("Attack");
                lastSwing = Time.time + _attackDelay;
                collision.SendMessage("ReduceUndeadHealth", _attackPower);
            }
            else
            {
                //Debug.Log("Cooldown");
                lastSwing = Time.time + _attackDelay;
            }
        }
    }

    void FindClosestUndead()
    {
        float distanceClosestUndead = Mathf.Infinity;
        Undead closestUndead = null;
        Undead[] allUndeads = GameObject.FindObjectsOfType<Undead>();
        foreach(Undead currentUndead in allUndeads)
        {
            float distanceToUndead = (currentUndead.transform.position - this.transform.position).sqrMagnitude;
            if (currentUndead == null)
            {
                return;
            }
            if (distanceToUndead < distanceClosestUndead)
            {
                distanceClosestUndead = distanceToUndead;
                closestUndead = currentUndead;
                _targetUndead = closestUndead;
            }
        }
        //Debug.DrawLine(this.transform.position, closestUndead.transform.position);
    }
}
