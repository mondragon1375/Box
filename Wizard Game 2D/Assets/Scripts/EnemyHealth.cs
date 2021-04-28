using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] 
    private float health;

    public int scoreReward;

    void Update()
    {
        if (health < 1)
        {
            Destroy(gameObject);
            GameplayManager.instance.AddScore(scoreReward);
        }
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            health -= GameObject.Find("Player").GetComponent<Player>().currentWeapon.damage;
            Destroy(collision.gameObject);
        }
    }
}
