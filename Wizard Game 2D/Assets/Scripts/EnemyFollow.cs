using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float speed;

    private List<Rigidbody2D> EnemyRBs;
    private Transform playerPos;
    private Rigidbody2D currentRB;

    private float repelRange = 20f;
    
    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        currentRB = GetComponent<Rigidbody2D>();

        if (EnemyRBs == null)
        {
            EnemyRBs = new List<Rigidbody2D>();
        }

        EnemyRBs.Add(currentRB);
    }

    void OnDestory()
    {
        EnemyRBs.Remove(currentRB);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > 0.7f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        }

        rotateAtPlayer();
    }

    void FixedUpdate()
    {
        Vector2 repelForce = Vector2.zero;
        foreach(Rigidbody2D enemy in EnemyRBs)
        {
            if (enemy == currentRB)
                continue;

            if (Vector2.Distance(enemy.position, currentRB.position) <= repelRange)
            {
                
                Vector2 repelDir = (currentRB.position - enemy.position);
                repelForce += repelDir;
            }
        }
    }


    void rotateAtPlayer()
    {
        Vector2 dir = new Vector2(playerPos.position.x, playerPos.position.y) - new Vector2(transform.position.x, transform.position.y);
        float angle = -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + 180;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }
}
