using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

// 1. int 2차 배열 생성
// 2. 2차 배열 요소마다 번호 지정(0~3)
// 3. 번호가 즉 타일 종류임
// 4. 번호에 맞게 타일 생성
public class MapCreater : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int mouse;
    public GameObject player; 
    public GameObject Enemy;
    public GameObject FogOfWar;
    public GameObject[] NormalTiles;
    public GameObject[] BlackTiles;
    public GameObject[] LavaTiles;
    public GameObject[] WallTiles;
    public GameObject[] DeonJeonTiles;
    public GameObject[] Monster; 

    private Transform boardHolder;

    private List<Vector3> gridPositions = new List<Vector3>();
    private List<GameObject> AllMapTiles = new List<GameObject>();
    private List<GameObject> Wall_Object = new List<GameObject>();

    public int width;
    public int height;

    //몬스터 소환 랜덤비율
    public int randomFillPercent;

    int[,] map;
    int BaseCamp_Width;
    int BaseCamp_Height;
    int DeonJeon_width;
    int DeonJeon_height;

    //맵 생성 메서드(종합)
    public void GenerateMap()
    {
     
        foreach (GameObject g in AllMapTiles)
        {
           Destroy(g);
        }
        foreach (GameObject g in Wall_Object)
        {
            Destroy(g);
        }
        map = new int[width, height];
        RandomFillMap();
        CreateMap();
        InitialiseList();
        LayoutObjectAtRandom(WallTiles);
    }
    //베이스 캠프 위치 지정 메서드
    int[] Base_Camp(int position)
    {
        int[] BaseCamp_size = new int[2];
        switch (position)
        {
            case 0: //2 /2
                BaseCamp_size[0] = width / 4;
                BaseCamp_size[1] = height / 4;               
                break;
            case 1:
                BaseCamp_size[0] = width / 4;
                BaseCamp_size[1] = height / 4;
                break;
            case 2:
                BaseCamp_size[0] = width / 4;
                BaseCamp_size[1] = (height / 4);
                break;
            case 3:
                BaseCamp_size[0] = (width / 4)*3;
                BaseCamp_size[1] = height / 4;
                break;
            case 4:
                BaseCamp_size[0] = (width / 4)*3;
                BaseCamp_size[1] = (height / 4)*3;
                break;
        }
        return BaseCamp_size;
    }

    //던전 보스방 위치 지정
    int[] BossRoom_Camp(int position)
    {
        int[] BossRoom_size = new int[2];
        switch (position)
        {
            case 0: //2 /2
                BossRoom_size[0] = width / 4;
                BossRoom_size[1] = (height / 4)*3;
                break;
            case 1:
                BossRoom_size[0] = (width / 4)*3;
                BossRoom_size[1] = height / 4;
                break;
            case 2:
                BossRoom_size[0] = (width / 4)*3;
                BossRoom_size[1] = (height / 4)*3;
                break;
        }
        return BossRoom_size;
    }
    //맵 랜덤 타일 종류 지정 메서드
    void RandomFillMap()
    {
        int Base_camp_position = Random.Range(0, 5);
        int[] basecamp_size = Base_Camp(Base_camp_position);
        BaseCamp_Width = basecamp_size[0];
        BaseCamp_Height = basecamp_size[1];

        player.transform.position = new Vector3(2, 2, -0.5f);
        int Boss_Room_Position = Random.Range(0, 3);
        int[] BossRoom_Size = BossRoom_Camp(Boss_Room_Position);
        DeonJeon_width = BossRoom_Size[0];
        DeonJeon_height = BossRoom_Size[1];
        Enemy.transform.position = new Vector3(DeonJeon_width, DeonJeon_height, -0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 0;
                }
                //베이스 캠프 태두리 / 입구 범위 조건식(안쪽)
                //else if (((x >= BaseCamp_Width - 6 && x <= BaseCamp_Width + 6) && (y == BaseCamp_Height -6 || y==BaseCamp_Height +6)) || 
                //    ((y >= BaseCamp_Height - 6 && y <= BaseCamp_Height + 6) && (x == BaseCamp_Width - 6 || x == BaseCamp_Width + 6)))
                //{
                //    if((x >= BaseCamp_Width -1 && x <= BaseCamp_Width +1) || (y>=BaseCamp_Height -1 && y <= BaseCamp_Height +1))
                //    {
                //        map[x, y] = 1;
                //    }
                //    else
                //    map[x, y] = 0;
                //}
                //베이스캠프 넓이 조건식
                //else if(x >= BaseCamp_Width -5 && x<= BaseCamp_Width+5 && y >= BaseCamp_Height - 5 && y <= BaseCamp_Height+5)
                //{
                //    map[x, y] = 1;
                //}
                //던전 넓이 조건식
                else if (x >= DeonJeon_width - 5 && x <= DeonJeon_width + 5 && y >= DeonJeon_height - 5 && y <= DeonJeon_height + 5)
                {
                    map[x, y] = 3;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }
    }

    //배열 요소의 값(번호)에 맞게 맵 타일 생성
    public void CreateMap()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map[x, y] == 0)
                    {
                        GameObject toInstantiate = LavaTiles[Random.Range(0, LavaTiles.Length)];
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), transform.rotation) as GameObject;
                        instance.transform.parent = this.transform;
                        AllMapTiles.Add(instance);
                        //instance.transform.SetParent(boardHolder);
                    }
                    else if (map[x, y] == 1)
                    {
                        GameObject toInstantiate = NormalTiles[Random.Range(0, NormalTiles.Length)];
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), transform.rotation) as GameObject;
                        instance.transform.parent = this.transform;
                        AllMapTiles.Add(instance);
                        //instance.transform.SetParent(boardHolder);
                    }
                    else if (map[x, y] == 2)
                    {
                        GameObject toInstantiate = BlackTiles[Random.Range(0, BlackTiles.Length)];
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), transform.rotation) as GameObject;
                        instance.transform.parent = this.transform;
                        AllMapTiles.Add(instance);
                        GameObject toInstantiate1 = WallTiles[Random.Range(0, WallTiles.Length)];
                        GameObject instance1 = Instantiate(toInstantiate1, new Vector3(x, y, 0f), transform.rotation) as GameObject;
                        instance1.transform.parent = this.transform;
                        AllMapTiles.Add(instance1);
                        // instance.transform.SetParent(boardHolder);
                        //instance1.transform.SetParent(boardHolder);
                    }
                    else if(map[x, y] == 3)
                    {
                        GameObject toInstantiate = DeonJeonTiles[Random.Range(0, DeonJeonTiles.Length)];
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), transform.rotation) as GameObject;
                        instance.transform.parent = this.transform;
                        AllMapTiles.Add(instance);
                    }
                }
            }
            //전장의안개
            for (int i = 0; i < width; i++)
            {
                for (int i1 = 0; i1 < height; i1++)
                {
                    if (i >= DeonJeon_width - 6 && i <= DeonJeon_width + 6 && i1 + 2 >= DeonJeon_height - 6 && i1 <= DeonJeon_height + 6)
                    {
                        continue;
                    }
                    else if (i < 2 && i1 < 2)
                    {
                        continue;
                    }
                    else
                    {
                        GameObject instance = Instantiate(FogOfWar, new Vector3(i, i1, -0.2f), transform.rotation) as GameObject;
                        instance.transform.parent = this.transform;
                        AllMapTiles.Add(instance);
                    }
                }
            }
        }
    }
    #region 오브젝트(맵벽 3*3형태) 생성
    Vector3[,] wall_pattern(Vector3[] wall_position)
    {
        Vector3[,] wall_position_pattern = new Vector3[13, 9];
        //ㄴ
        wall_position_pattern[0, 0] = wall_position[4];
        /*wall_position_pattern[0, 1] = wall_position[3];
        wall_position_pattern[0, 2] = wall_position[7];
        wall_position_pattern[0, 3] = wall_position[8];*/
        //ㄱ
        wall_position_pattern[1, 0] = wall_position[4];
        /*wall_position_pattern[1, 1] = wall_position[2];
        wall_position_pattern[1, 2] = wall_position[5];
        wall_position_pattern[1, 3] = wall_position[8];*/
        //「
        wall_position_pattern[2, 0] = wall_position[4];
        /*wall_position_pattern[2, 1] = wall_position[1];
        wall_position_pattern[2, 2] = wall_position[2];
        wall_position_pattern[2, 3] = wall_position[3];
        wall_position_pattern[2, 4] = wall_position[6];*/
        //』
        wall_position_pattern[3, 0] = wall_position[4];
        /*wall_position_pattern[3, 1] = wall_position[5];
        wall_position_pattern[3, 2] = wall_position[6];
        wall_position_pattern[3, 3] = wall_position[7];
        wall_position_pattern[3, 4] = wall_position[8];*/
        //ㅣ(왼)
        wall_position_pattern[4, 0] = wall_position[4];
        /*wall_position_pattern[4, 1] = wall_position[3];
        wall_position_pattern[4, 2] = wall_position[6];*/
        //ㅡ(밑)
        wall_position_pattern[5, 0] = wall_position[4];
        /*wall_position_pattern[5, 1] = wall_position[7];
        wall_position_pattern[5, 2] = wall_position[8];*/
        //ㅣ (오른)
        wall_position_pattern[6, 0] = wall_position[4];
       /* wall_position_pattern[6, 1] = wall_position[5];
        wall_position_pattern[6, 2] = wall_position[8];*/
        //T
        wall_position_pattern[7, 0] = wall_position[4];
        /*wall_position_pattern[7, 1] = wall_position[1];
        wall_position_pattern[7, 2] = wall_position[2];
        wall_position_pattern[7, 3] = wall_position[4];*/
        //가운데 점 하나
        wall_position_pattern[8, 0] = wall_position[4];
        //ㅏ
        wall_position_pattern[9, 0] = wall_position[4];
        //ㅗ
        wall_position_pattern[10, 0] = wall_position[4];
        //ㅓ
        wall_position_pattern[11, 0] = wall_position[4];

        wall_position_pattern[12, 0] = wall_position[0];
        wall_position_pattern[12, 1] = wall_position[1];
        wall_position_pattern[12, 2] = wall_position[2];
        wall_position_pattern[12, 3] = wall_position[3];
        wall_position_pattern[12, 4] = wall_position[4];
        wall_position_pattern[12, 5] = wall_position[5];
        wall_position_pattern[12, 6] = wall_position[6];
        wall_position_pattern[12, 7] = wall_position[7];
        wall_position_pattern[12, 8] = wall_position[8];

        return wall_position_pattern;
    }

    void Wall_Create(int position , GameObject[] tileArray, int x, int y)
    {
        Vector3[] wall_position = new Vector3[12];     
        int xvector = 1;
        int yvector = 3;
        for (int i = 0; i < wall_position.Length; i++)
        {
            wall_position[i] = new Vector3((x + xvector) + (x * 2), (y + yvector) + (y * 2), -0.1f);
            if(xvector == 3)
            {
                xvector = 1;
                yvector--;
            }
            else
            xvector++;
        }
        Vector3[,] wall_position_pattern = wall_pattern(wall_position);

        //GameObject tileChoice = null;
        GameObject Wall, MonsterChoice = null;
        //tileChoice = tileArray[Random.Range(0, tileArray.Length)];
        switch (position)
        {
            case 0: //ㄴ
                //for (int i = 0; i < 5; i++)
                //{
                    Wall = Instantiate(tileArray[0], wall_position_pattern[0, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                //} 
                break;
            case 1: //ㄱ
                //for (int i = 0; i < 5; i++)
                //{
                    Wall = Instantiate(tileArray[1], wall_position_pattern[1, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                //}
                break;
            case 2:  //「
                //for (int i = 0; i < 5; i++)
                //{
                    Wall = Instantiate(tileArray[2], wall_position_pattern[2, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                //}
                break;
            case 3: //』
                //for (int i = 0; i < 5; i++)
                //{
                    Wall = Instantiate(tileArray[3], wall_position_pattern[3, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                //}
                break;
            case 4:  // ㅣ(왼)
                //for (int i = 0; i < 3; i++)
                //{
                    Wall = Instantiate(tileArray[4], wall_position_pattern[4, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                //}
                break;
            case 5:  // ㅡ(밑)
                //for (int i = 0; i < 3; i++)
                //{
                    Wall = Instantiate(tileArray[5], wall_position_pattern[5, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                //}
                break;
            case 6: // ㅣ (오른)
                //for (int i = 0; i < 3; i++)
                //{
                    Wall = Instantiate(tileArray[6], wall_position_pattern[6, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                //}
                break;
            case 7: // T
                //for (int i = 0; i < 5; i++)
                //{
                    Wall = Instantiate(tileArray[7], wall_position_pattern[7, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                //}
                break;
            case 8: // 가운데 점
                Wall = Instantiate(tileArray[8], wall_position_pattern[8, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                break;
            case 9: // ㅏ
                Wall = Instantiate(tileArray[9], wall_position_pattern[9, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                break;
            case 10: // ㅗ
                Wall = Instantiate(tileArray[10], wall_position_pattern[10, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                break;
            case 11: // ㅓ
                Wall = Instantiate(tileArray[11], wall_position_pattern[11, 0], transform.rotation) as GameObject;
                Wall.transform.parent = this.transform;
                Wall_Object.Add(Wall);
                break;
            case 12: //몬스터 소환
                for (int i = 0; i < 9; i++)
                {
                    int Spawn_Percent = Random.Range(0, 100);
                    if (Spawn_Percent <= randomFillPercent)
                    {
                        MonsterChoice = Monster[Random.Range(0, Monster.Length)];
                        GameObject Monster_spawn = Instantiate(MonsterChoice, wall_position_pattern[12, i], Quaternion.Euler(-90, 0, 0));
                        Wall_Object.Add(Monster_spawn);
                    }
                }
                break;
            case 13:
                break;
        }
    }
    int Update_Waypoint(int prev_positionx, int prev_positiony)
    {
        int wall_position = 0;

        if(prev_positiony == 0)
        {
           wall_position = Random.Range(0, 13);
        }
        else if (prev_positiony == 1)
        {
            while (true)
            {
                wall_position = Random.Range(0, 13);
                if (wall_position == 0 || wall_position == 4 || wall_position == 9 || wall_position == 10)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 2)
        {
            while (true)
            {
                wall_position = Random.Range(0, 12);
                if (wall_position == 3 || wall_position == 4 || wall_position == 6 || wall_position == 9 || wall_position == 11)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 3)
        {
            while (true)
            {
                wall_position = Random.Range(0, 13);
                if (wall_position == 3 || wall_position == 5 || wall_position == 6 || wall_position == 10 || wall_position == 11)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 4)
        {
            while (true)
            {
                wall_position = Random.Range(0, 13);
                if (wall_position == 0 || wall_position == 3 || wall_position == 5 || wall_position == 6 || wall_position == 10 || wall_position == 11)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 5)
        {
            while (true)
            {
                wall_position = Random.Range(0, 12);
                if (wall_position == 3 || wall_position == 5 || wall_position == 6 || wall_position == 10 || wall_position == 11)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 6)
        {
            while (true)
            {
                wall_position = Random.Range(0, 13);
                if (wall_position == 3 || wall_position == 5 || wall_position == 6 || wall_position == 10 || wall_position == 11)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 9)
        {
            while (true)
            {
                wall_position = Random.Range(0, 12);
                if (wall_position == 3 || wall_position == 4 || wall_position == 5 || wall_position == 9 || wall_position == 11)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 10)
        {
            while (true)
            {
                wall_position = Random.Range(0, 13);
                if (wall_position == 3 || wall_position == 5 || wall_position == 6 || wall_position == 10 || wall_position == 11)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 11)
        {
            while (true)
            {
                wall_position = Random.Range(0, 13);
                if (wall_position == 3 || wall_position == 4 || wall_position == 10 )
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else if (prev_positiony == 12)
        {
            while (true)
            {
                wall_position = Random.Range(0, 13);
                if (wall_position == 3 || wall_position == 5 || wall_position == 6 || wall_position == 10 || wall_position == 11)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
           wall_position = Random.Range(0, 13);
        }

        return wall_position;
    }
    void LayoutObjectAtRandom(GameObject[] tileArray)
    {
        int Wall_Position = 0;
        List<int> Wall_Position_List = new List<int>();
        int count = 0;
        int prev_position_x = 0;
        int prev_position_y = 0;
        //int objectCount = Random.Range(minimum, maximum + 1);
        for (int x = 0; x <(width-2)/3; x++)
        {  
            for(int y = 0; y < (height-2)/3; y++)
            {
                if (count == 0)
                {
                   prev_position_y = -1;
                   prev_position_x = -1;
                }
                else if(count > 0 && count < 21)
                {
                   prev_position_y = Wall_Position_List[count - 1];
                   prev_position_x = -1;
                }
                else if(count > 21)
                {
                    prev_position_y = Wall_Position_List[count - 1];
                    prev_position_x = Wall_Position_List[count - 21];
                }

                Wall_Position = Update_Waypoint(prev_position_x, prev_position_y);
                if ((x * 3)+2 >= DeonJeon_width - 6 && (x * 3)+2 <= DeonJeon_width + 6 && (y * 3)+2 >= DeonJeon_height - 6 && (y * 3)+2 <= DeonJeon_height + 6)
                {
                    Wall_Position = 13;
                }
                if(x<2 && y< 2)
                {
                    Wall_Position = 13; 
                }
                Wall_Position_List.Add(Wall_Position);
                Wall_Create(Wall_Position, tileArray, x, y);
                count++;
            }
        }
        /*for (int i = 0; i < objectCount; i++)
        // {
        //Vector3 randomPosition = RandomPosition();
        //tileChoice = tileArray[Random.Range(0, tileArray.Length)];
        //Instantiate(tileChoice, randomPosition, transform.rotation);
        //if (tileChoice == tileArray[1])
        //{
        //    tileChoice.GetComponent<Transform>().localScale = new Vector3(2f, 2f, 2f);
        //}
        //else if (tileChoice == tileArray[2])
        //{
        //    tileChoice.GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //}
        //}*/
    }
    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mouse = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
            mouse++;
        }
    }
}
