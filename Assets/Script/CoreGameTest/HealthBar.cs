using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    float Size = 1f;
    
    private void Start()
    {
        GameObject AngelMelee = GameObject.Find("AngelMelee");
        Angel angel = AngelMelee.GetComponent<Angel>();
    }

    private void Update()
    {
        bar = transform.Find("Bar");
        ReduceHealthBar();
    }

    void ReduceHealthBar()
    {
        int damage = 1;
        float Totaldamage = damage*100/100;
        float ReduceSize = Size - Totaldamage;
        bar.localScale = new Vector3(ReduceSize, Size);
    }
}
