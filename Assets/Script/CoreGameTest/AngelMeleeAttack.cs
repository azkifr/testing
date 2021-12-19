using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMeleeAttack : MonoBehaviour
{
    //private static AngelMeleeAttack _instance = null;

    //public static AngelMeleeAttack Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<AngelMeleeAttack>();
    //        }
    //        return _instance;
    //    }
    //}
    [SerializeField] private SpriteRenderer _angelHead;
    public ContactFilter2D filter;
    private CircleCollider2D circleCollider2D;
    private Collider2D[] hits = new Collider2D[10];
    private SpriteRenderer spriteRenderer;
    //Attack
    private int _attackPower=1;

    //Swing
    public float _attackDelay = 1.0f;
    private float lastSwing;

    private Undead _targetUndead;
    private Animator anim;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        //_angelUI = angelUI.GetComponent<AngelUI>();
        //Collision
        circleCollider2D.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            //Debug.Log(_angelUI.StopAttack);
            //if (_angelUI.StopAttack==true)
            //{
            //    Debug.Log("STOP ATTACK ANGEL SCRIPT");
            //    continue;
            //}
            FindClosestUndead();
            OnCollide(hits[i]);
                //clean up array manual
            hits[i] = null;
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

    private void OnCollide(Collider2D collision)
    {
        anim = _angelHead.GetComponent<Animator>();
        if (collision.tag == "Undead" & _targetUndead.gameObject == collision.gameObject)
        {
            //Debug.Log("Hit");
            if (Time.time >= lastSwing)
            {
                Debug.Log("Angel Attack");
                anim.Play("AngelMeleeAttack");
                lastSwing = Time.time + _attackDelay;
                _targetUndead.ReduceUndeadHealth(_attackPower); 
                //Debug.Log(_targetUndead._currentHealth);
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
