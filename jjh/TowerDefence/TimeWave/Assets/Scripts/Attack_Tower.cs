using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Tower : MonoBehaviour
{
    // 근처 적 배열
    private List<GameObject> on_Enemy_List = new List<GameObject>();

    // 타워가 공격할, 중인 적
    public static GameObject t_Attack_Target;

    // 발사체 프리팹
    public GameObject t_Bullet_Prefab;

    // 시간 지연용
    private float timer;
    
    // 기다리는 시간
    public float waiting_Time;

    private void Start()
    {
        timer = 0;
        on_Enemy_List.Clear();
        t_Attack_Target = null;
    }

    private void Update()
    {
        Attack_Enemy();
    }

    // 공격
    private void Attack_Enemy()
    {
        try
        {
            t_Attack_Target = on_Enemy_List[0] as GameObject;

            timer += Time.deltaTime;


            if (timer > waiting_Time)
            {
                Instantiate(t_Bullet_Prefab, new Vector3(0, 0, 0), Quaternion.identity);


                if (timer > waiting_Time)
                {
                    timer = 0;
                }
            }
            
        }
        catch (Exception)
        {

        }

    }

    // 콜라이더 내부 들어왔을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Enemy_Check(collision) == true)
        {
            on_Enemy_List.Add(collision.gameObject);
        }
    }

    // 콜라이더에서 나갔을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Enemy_Check(collision) == true)
        {
            on_Enemy_List.Remove(collision.gameObject);
        }
    }

    // 콜라이더에 들어온 것 중에 Wave_Enemy태그가 있는지 확인하는 코드
    private bool Enemy_Check(Collider2D collision)
    {
        if (collision.tag == "Wave_Enemy")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
