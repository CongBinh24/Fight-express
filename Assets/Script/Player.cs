using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 2f; 

    private Vector3 moveInput = Vector3.zero;

    private Rigidbody _rb;
    private Animator _anim;
    
    

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D hoặc ← →
        float v = Input.GetAxisRaw("Vertical");   // W/S hoặc ↑ ↓
        moveInput = new Vector3(h, 0f, v);

        bool isRuning = moveInput.magnitude > 0.1f;
        _anim.SetBool(Const.RUN_ANIM, isRuning);

        if (isRuning)
        {
            transform.forward = moveInput;
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = moveInput.normalized * moveSpeed ;
        _rb.MovePosition(_rb.position + move * Time.fixedDeltaTime);
    }
}
