using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.TMP_DefaultControls;
using Resources = UnityEngine.Resources;

public class Click_TowerCreater : Mouse_Click
{
    // 타워 부모 오브젝트
    private GameObject t_Parent;

    // 타워 제작 트리거
    private bool t_Create = false;

    // 아쳐 타워 프리팹
    public GameObject archer_Tower_Prefab;

    // 타워 건축가
    private GameObject t_Creater;

    // 상점 UI
    private GameObject t_Shop_UI;

    // 맵 타일
    private GameObject[] map_Tile = null;
    private GameObject map_Tile_Grid;

    // 아쳐 타워 아이콘
    private GameObject t_Archer_Icon;

    
    // Start is called before the first frame update
    void Start()
    {
        t_Create = false;
        moust_Target = null;
        t_Parent = GameObject.Find("Tower");
        t_Creater = GameObject.Find("Tower_Creater");
        t_Shop_UI = GameObject.Find("Tower_Creater").transform.Find("Tower_UI").gameObject;
        map_Tile = GameObject.FindGameObjectsWithTag("Floor");
        t_Archer_Icon = GameObject.Find("Tower_Creater").transform.Find("Tower_UI").transform.Find("Tower_Archer_Icon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        t_State();
    }

    // 타워 상태
    public void t_State()
    {
        if(moust_Target == t_Creater)
        {
            t_Shop_UI.SetActive(true);
        }
        else if(moust_Target == t_Archer_Icon)
        {
            t_Create = true;
            //그리드 보여주기
            t_Show_Create_Grid(t_Create);
        }
        else if(Map_Tile_Array() && t_Create == true)
        {
            // 타워 생성
            if (t_Already_Have_Target(moust_Target) == true)
            {
                Instantiate(archer_Tower_Prefab as GameObject, new Vector3(0, 0 ,0), Quaternion.identity).transform.SetParent(moust_Target.transform, false);
                t_Create = false;
                t_Show_Create_Grid(t_Create);
            }
        }
    }

    // 타워 있는지 확인
    private bool t_Already_Have_Target(GameObject t_Check)
    {
        if(t_Check.transform.Find("Archer_Tower_Prefab(Clone)") == false)
        {
            return true;
        }
        return false;
    }

    // 그리드 보기
    private void t_Show_Create_Grid(bool t_Create_Trigger)
    {
        try
        {
            if (t_Create_Trigger == true)
            {
                for (int i = 0; i <= map_Tile.Length; i++)
                {
                    map_Tile_Grid = map_Tile[i].transform.Find("Grid").gameObject;
                    map_Tile_Grid.SetActive(true);
                }
            }
            else if (t_Create_Trigger == false)
            {
                for (int i = 0; i <= map_Tile.Length; i++)
                {
                    map_Tile_Grid = map_Tile[i].transform.Find("Grid").gameObject;
                    map_Tile_Grid.SetActive(false);
                }
            }
        }
        catch
        {

        }

    }

    // 바닥 배열 map_Tile 내부에 있는지 확인
    private bool Map_Tile_Array()
    {try
        {
            for (int i = 0; i <= map_Tile.Length; i++)
            {
                if (moust_Target == map_Tile[i])
                {
                    return true;
                }
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
