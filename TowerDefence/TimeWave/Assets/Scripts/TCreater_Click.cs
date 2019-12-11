using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.TMP_DefaultControls;
using Resources = UnityEngine.Resources;

public class TCreater_Click : MonoBehaviour
{

    // 고른 타겟
    public GameObject target;


    // 타워 제작 트리거
    public bool t_Create = false;

    // 아쳐 타워 프리팹
    public GameObject archer_Tower_Prefab;

    // 타워 건축가
    private GameObject tower_Creater;

    // 상점 UI
    private GameObject t_Shop_UI;

    // 맵 타일
    private GameObject map_Tile;
    
    // 아쳐 타워 아이콘
    private GameObject tower_Archer_Icon;

    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("NomaralTile");
        tower_Creater = GameObject.Find("Tower_Creater");
        t_Shop_UI = GameObject.Find("Tower_Creater").transform.Find("Tower_UI").gameObject;
        map_Tile = GameObject.Find("NomaralTile");
        tower_Archer_Icon = GameObject.Find("Archer_Icon");
    }

    // Update is called once per frame
    void Update()
    {
        Target_State();
    }

    public void Target_State()
    {
        if (target == tower_Creater)
        {
            t_Shop_UI.SetActive(true);
        }
        else if (target == tower_Archer_Icon)
        {
            t_Create = true;
            // 그리드 보여주기

        }
        else if(target == map_Tile && t_Create == true)
        {
            Instantiate(archer_Tower_Prefab as GameObject, new Vector3(0, 0, 0), Quaternion.identity).transform.SetParent(target.transform, false);
        }
    }






    void FixedUpdate()

    {
        if (Input.GetMouseButtonDown(0))
        {

            CastRay();

            if (target == this.gameObject)
            {  //타겟 오브젝트가 스크립트가 붙은 오브젝트라면

                // 여기에 실행할 코드를 적습니다.

            }
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
