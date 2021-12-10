using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    // Tower Component
    [SerializeField] private SpriteRenderer _angelPlace;
    [SerializeField] private SpriteRenderer _angelHead;

    // Tower Properties
    [SerializeField] private int _shootPower = 1;
    [SerializeField] private float _shootDistance = 1f;
    [SerializeField] private float _shootDelay = 5f;
    [SerializeField] private float _bulletSpeed = 1f;
    [SerializeField] private float _bulletSplashRadius = 0f;
    [SerializeField] private int _angelHealth = 1;
    [SerializeField] public double _angelCost = 10;

    //public GameObject AttackScript = GameObject.Find("AngelMeleeAttack");
    // Digunakan untuk menyimpan posisi yang akan ditempati selama tower di drag
    public Vector2? PlacePosition { get; private set; }

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
    public void ReduceAngelHealth(int damage)
    {
        _angelHealth -= damage;
        if (_angelHealth <= 0)
        {
            Debug.Log("RIP");
            gameObject.SetActive(false);
        }
    }
}
