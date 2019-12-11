using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.TMP_DefaultControls;
using Resources = UnityEngine.Resources;

public class TCreater_Click : MonoBehaviour
{

    private List<GameObject> map_Tile_List = new List<GameObject>();

    // 고른 타겟
    public GameObject target;

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
        target = null;
        t_Parent = GameObject.Find("Tower");
        t_Creater = GameObject.Find("Tower_Creater");
        t_Shop_UI = GameObject.Find("Tower_Creater").transform.Find("Tower_UI").gameObject;
        map_Tile = GameObject.FindGameObjectsWithTag("Floor");
        t_Archer_Icon = GameObject.Find("Tower_Creater").transform.Find("Tower_UI").transform.Find("Tower_Archer_Icon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(target);
        Target_State();

    }

    public void Target_State()
    {
        Debug.Log(t_Create);
        if(target == t_Creater)
        {
            t_Shop_UI.SetActive(true);
            Debug.Log(t_Create);
        }
        else if(target == t_Archer_Icon)
        {
            t_Create = true;
            Debug.Log(t_Create);
            //그리드 보여주기
            Show_Tower_Create_Grid(t_Create);
        }
        else if(Check_Array() && t_Create == true)
        {
            // 타워 생성
            if (Already_On_Target(target) == true)
            {
                Instantiate(archer_Tower_Prefab as GameObject, target.transform.position, Quaternion.identity).transform.SetParent(target.transform, false);
                t_Create = false;
                Show_Tower_Create_Grid(t_Create);
            }
            Debug.Log(t_Create);
        }
        Debug.Log(t_Create);
    }

    private bool Already_On_Target(GameObject t_Check)
    {
        if(t_Check.transform.Find("Archer_Tower_Prefab(Clone)") == false)
        {
            return true;
        }
        return false;
    }

    private void Show_Tower_Create_Grid(bool t_Create_Trigger)
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

    private bool Check_Array()
    {try
        {
            for (int i = 0; i <= map_Tile.Length; i++)
            {
                if (target == map_Tile[i])
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



    void FixedUpdate()

    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }



    void CastRay() // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 
    {

        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        { //히트되었다면 여기서 실행

            //Debug.Log (hit.collider.name);  //이 부분을 활성화 하면, 선택된 오브젝트의 이름이 찍혀 나옵니다. 

            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정

        }
    }

}
