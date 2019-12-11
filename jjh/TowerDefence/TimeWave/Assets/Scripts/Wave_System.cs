using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Wave_System : MonoBehaviour
{
    //적 수 저장할 List
    public List<GameObject> wave_Enemy;

    //웨이브의 수
    public int count;

    //웨이브 시간
    private float start_Time;
    //웨이브 남은 시간
    private float wave_Time;

    //시간 표시할 Text 오브젝트 Time 지정
    public Text obj_Time;

    //적 프리팹
    private GameObject wave_Enemy_Prefab = Resources.Load<GameObject>("Prefab/Enemy/Enemy");
    //적 스폰 포인트
    private GameObject wave_Enemy_Spawn_Point = GameObject.Find("Spawn_Point");

    private void Start()
    {
        start_Time = 180;
        count = 0;
        wave_Time = start_Time;
        
    }

    private void Update()
    {
        if(wave_Time == 0)
        {
            Wave_Enemy_Spwan(count);
        }

        if (wave_Enemy.Count == 0)
        {
            Wave_Timer();
        }

    }



    private void Time_Text_Print()
    {
        string wave_Time_Minute = GetMinute(wave_Time).ToString();
        string wave_Time_Second = GetSecond(wave_Time).ToString();
        obj_Time.text = wave_Time_Minute + ":" + wave_Time_Second;
    }


    private void Wave_Timer()
    {
        Time_Text_Print();

        wave_Time -= Time.deltaTime;

        if(wave_Time <= 0)
        {
            count++;
            wave_Time = start_Time;
        }

    }

    private void Wave_Enemy_Spwan(int count)
    {
        int enemy_Count = Random.Range(10, 15);

        for (int i = 0; i < enemy_Count; i++)
        {
            GameObject enemy_Prefab_Clone =
                Instantiate(wave_Enemy_Prefab, new Vector3(wave_Enemy_Spawn_Point.transform.position.x, wave_Enemy_Spawn_Point.transform.position.y, wave_Enemy_Spawn_Point.transform.position.z), Quaternion.identity);

            wave_Enemy.Add(enemy_Prefab_Clone);
        }
    }

    public void Wave_Skip()
    {
        wave_Time = 0;
    }

    private static float GetMinute(float _time)
    {
        return (int)((_time / 60) % 60);
    }

    private static float GetSecond(float _time)
    {
        return (int)(_time % 60);
    }

}
