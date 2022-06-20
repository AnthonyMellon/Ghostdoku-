using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {

    public NavMeshAgent agent;

    private float heldDownTimer;

    public float allowedHoldTime;
    public Rigidbody2D rb;

    private Vector3 target;

    public Texture2D idleLeft;

    public bool canMove;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        canMove = false;
        // target = transform.position;
        print(idleLeft);
    }

    // Update is called once per frame
    void Update() {
        //if (canMove)
        if (EventSystem.current.IsPointerOverGameObject()) return;
        {
            if (Input.GetMouseButton(0))
            {
                heldDownTimer += Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0) && heldDownTimer < allowedHoldTime)
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
            }
            if (Input.GetMouseButtonUp(0))
            {
                heldDownTimer = 0;
            }
            agent.SetDestination(target);

            //Animation control

            if (transform.position.x > target.x) //Moving left
            {
                animator.SetBool("moving", true);
                animator.SetBool("facingLeft", true);
            }
            if (transform.position.x < target.x) //Moving right
            {
                animator.SetBool("moving", true);
                animator.SetBool("facingLeft", false);
            }
            if (transform.position.x == target.x) //Not moving
            {
                animator.SetBool("moving", false);
            }

            spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
        }
        
    }
}
