using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    // Start is called before the first frame update
    private void Start()
    {
        bar = transform.Find("Bar");
    }

    void ReduceHealthBar(float ReduceSize)
    {
        bar.localScale = new Vector3(ReduceSize, 1f);
    }
}
