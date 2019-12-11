using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    Transform spawn;
    Transform target;
    Transform enemy;

    public float speed = 0.01f;

    public float spawnTrans = 0.5f;
    public float ComeBoss = 5.0f;
    public float SeeBoss = 3.0f;

    bool MoveWait = false;

    private void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        float distance = Vector2.Distance(target.position, transform.position); // 거리 계산

        if(distance <= SeeBoss) //거리 안에 들어왔을 때 따라가기
        {
            MoveWait = true;
        }
        else // 추적거리에 벗어나면 되돌아가기
        {

        }

        if (MoveWait == true)
        {
            if (distance > ComeBoss)
            {
                MoveWait = false;
            }

            Vector2 dir = target.position - transform.position; // 타겟 위치에 따라 시선 확인

            transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;

            if (dir.x >= 0) // 캐릭터 좌우 대칭
            {
                Vector3 scale = transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
            else
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

        }
        else if (MoveWait == false)
        {
            float spawnstop = Vector2.Distance(spawn.position, transform.position); // 거리 계산

            if (spawnstop > spawnTrans)
            {
                transform.position += (spawn.position - transform.position).normalized * speed * Time.deltaTime;
                Vector2 dir = spawn.position - transform.position; // 타겟 위치에 따라 시선 확인

                if (dir.x >= 0) // 캐릭터 좌우 대칭
                {
                    Vector3 scale = transform.localScale;
                    scale.x = -Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
                else
                {
                    Vector3 scale = transform.localScale;
                    scale.x = Mathf.Abs(scale.x);
                    transform.localScale = scale;
                }
            }
            else
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
      
    }
}
