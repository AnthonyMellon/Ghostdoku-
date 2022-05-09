using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;

    private float heldDownTimer;

    public float allowedHoldTime;
    public Rigidbody2D rb;

    private Vector3 target;

    void Start() {
        target = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButton(0))
        {
            heldDownTimer += Time.deltaTime;
        }
        if(Input.GetMouseButtonUp(0) && heldDownTimer < allowedHoldTime)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }
        if(Input.GetMouseButtonUp(0))
        {
            heldDownTimer = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        
    }
}
