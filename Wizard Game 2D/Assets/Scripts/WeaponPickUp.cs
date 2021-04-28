using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{

    public Weapon weapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().currentWeapon = weapon;
            collision.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = weapon.currentWeaponSpr;
            Destroy(gameObject);
        }
    }
}
