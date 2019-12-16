using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    public enum CurrentState { idle, trace, attack, skill, oneoff, dead};
    public CurrentState curstate = CurrentState.idle;

    Transform spawn;
    Transform target;
    Transform enemy;

    Animator anime;

    public GameObject playerTrans;
    public GameObject spawnImage;

    public float HP = 100; // 체력

    public float attack_point = 20; // 공격력
    public float attack_speed = 500; // 공격속도

    public string getingSkill; // 스킬이름

    public float speed = 0.01f; // 이동 속도

    public int Gold_Point = 100;
    public float attack_Lange = 2.0f; // 공격 거리

    public float spawnTrans = 0.3f; // 스폰지점과 몬스터 오차거리
    public float comingBoss = 5.0f; // 추적 거리
    public float seeingBoss = 4.0f; // 감시 거리
    
    bool moveOrWait = false; // 쫓아오는지 아닌지 체크
    bool attackCheck = false; // 공격 이동 체크

    private void Start()
    {
        //spawn = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        target = playerTrans.transform;
        spawn = spawnImage.transform;
        anime = transform.GetComponent<Animator>();
    }
    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        float distance = Vector2.Distance(target.position, transform.position); // 거리 계산

        if(distance <= seeingBoss) //거리 안에 들어왔을 때 따라가기
        {
            curstate = CurrentState.trace;
            moveOrWait = true;
        }
        else if(distance > comingBoss)// 추적거리에 벗어나면 되돌아가기
        {
            curstate = CurrentState.idle;
            moveOrWait = false;
        }

        if (moveOrWait == true) // 추적들어감
        {
            anime.SetFloat("Speed", 1);

            if (attackCheck == false)
            {
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

             if (distance < attack_Lange) // 공격 사거리 확인
            {
                anime.SetFloat("Speed", 0);
                Attack();
                 curstate = CurrentState.attack;
                 attackCheck = true;
            }
             else
             {
                getingSkill = "";
                 attackCheck = false;
             }
                

        }
        else if (moveOrWait == false) // 몬스터 스폰 지점으로 돌아가기
        {
            float spawnstop = Vector2.Distance(spawn.position, transform.position); // 거리 계산

            if (spawnstop > spawnTrans)
            {
                transform.position += (spawn.position - transform.position).normalized * speed * Time.deltaTime;
                Vector2 dir = spawn.position - transform.position; // 타겟 위치에 따라 시선 확인

                if (dir.x >= 0) // 시선에 따라 캐릭터 좌우 대칭
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
                anime.SetFloat("Speed", 0);
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

        }
    }

    void Attack() // 평타
    {
        getingSkill = "평타";
    }

    void skillAttack1() //스킬 1번
    {

    }

    void Oneoff() // 필살기
    {

    }

    private void OnTriggerEnter(Collider other)
    {
      
    }
}
