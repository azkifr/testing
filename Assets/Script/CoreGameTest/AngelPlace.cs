using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelPlace : MonoBehaviour
{
    private Angel _placedAngel;

    // Fungsi yang terpanggil sekali ketika ada object Rigidbody yang menyentuh area collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_placedAngel != null)
        {
            return;
        }

        Angel angel = collision.GetComponent<Angel>();
        if (angel != null)
        {
            
            if (collision.tag == "Range"&&gameObject.tag=="RangeTile")//Tile khusus range
            {
                //Debug.Log("Range");
                
                angel.SetPlacePosition(transform.position);
                
            }
            else if (collision.tag == "Melee"&&gameObject.tag=="MeleeTile")//Tile khusus Melee
            {
                //Debug.Log("Melee");
                
                angel.SetPlacePosition(transform.position);
                
            }
            
            _placedAngel = angel;
        }
    }

    // Kebalikan dari OnTriggerEnter2D, fungsi ini terpanggil sekali ketika object tersebut meninggalkan area collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_placedAngel == null)
        {
            return;
        }
        _placedAngel.SetPlacePosition(null);

        _placedAngel = null;
    }

}
