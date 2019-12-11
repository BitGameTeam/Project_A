using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Control : MonoBehaviour
{
    // 좇을 타겟
    private Transform target;

    //private int wave_Point_Index = 0;
    public Transform move_Target;
    public Transform attack_Target;

    // nnavmeshagent
    private NavMeshAgent nvAgent;

    enum State
    {
        crystal_Move,
        wall_Move
    };

    State state = State.crystal_Move;
    State nextState = State.crystal_Move;

    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
        target = move_Target;
    }

    void Update()
    {
        switch(state)
        {
            case State.crystal_Move:
                Target_Move(move_Target);
                break;
            case State.wall_Move:
                Target_Move(move_Target);
                break;

        }


    }

    // 스테이트를 변경한다.
    void ChangeState(State nextState)
    {
        this.nextState = nextState;
    }

    private void Target_Move(Transform move_Target)
    {
        nvAgent.SetDestination(move_Target.position);
    }





}

