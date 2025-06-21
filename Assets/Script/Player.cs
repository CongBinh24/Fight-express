using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    bool isAttacking;
    private NavMeshAgent _agent;
    private Animator _anim;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _agent.updateRotation = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!isAttacking) 
        {
            Movement();
        }
        Combat();

    }

    public void Movement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _agent.SetDestination(hit.point);
                Debug.Log("di chuyen toi " + hit.point);
            }
        }

        if (_agent.hasPath && !_agent.pathPending && _agent.remainingDistance > _agent.stoppingDistance)
        {
            Vector3 dir = _agent.desiredVelocity.normalized;
            dir.y = 0;
            if (dir != Vector3.zero)
            {
                transform.forward = dir;
            }
        }

        bool isRunning = _agent.velocity.magnitude > 0.1f;
        _anim.SetBool(Const.RUN_ANIM, isRunning);
    }

    public void Combat()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isAttacking = true;
            _anim.SetBool(Const.PUNCH_ANIM,true);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isAttacking = true;
            _anim.SetBool(Const.KICK_ANIM, true);
        }
    }

    public void ResetCombatAnim()
    {
        isAttacking = false;
        _anim.SetBool(Const.PUNCH_ANIM, false);
        _anim.SetBool(Const.KICK_ANIM, false);
    }
}
