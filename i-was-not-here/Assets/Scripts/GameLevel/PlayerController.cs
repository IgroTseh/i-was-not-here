using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        Vector3 moveDir = GetDir();
        Move(moveDir);
    }

    private Vector3 GetDir()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            move += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            move += Vector3.right;
        }

        return move;
    }

    private void Move(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            moveDirection = moveDirection.normalized * moveSpeed * Time.deltaTime;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        transform.Translate(moveDirection);
    }
}
