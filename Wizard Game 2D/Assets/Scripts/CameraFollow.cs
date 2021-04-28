using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerPos;

    // Hard-coded map boundries of the Camera
    [SerializeField] private float xBound = 10f;
    [SerializeField] private float yBound = 10f;

    void Awake() 
    {
        // Camera finds the current player
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    { 
        // Camera follows the current player in the defined map boundries
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBound, xBound), Mathf.Clamp(transform.position.y, -yBound, yBound), transform.position.z);
    }
}
