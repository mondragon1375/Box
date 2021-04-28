using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 12;

    private Vector2 dir;

    void Start()
    {
        dir = GameObject.Find("Dir").transform.position;
        transform.position = GameObject.Find("FirePoint").transform.position;
        Destroy(gameObject, 3.25f);
        transform.eulerAngles = new Vector3(0, 0, GameObject.Find("Player").transform.rotation.eulerAngles.z);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, dir, speed * Time.deltaTime);
    }
}
