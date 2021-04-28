using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerBody;
    private float horizontalInput;
    private float verticleInput;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticleInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        playerBody.velocity = new Vector3(horizontalInput*8, playerBody.velocity.y, verticleInput*8);
    }
}
