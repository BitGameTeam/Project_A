using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MazeMap : MonoBehaviour
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

    public GameObject[] NormalTiles;
    public GameObject[] BlackTiles;
    public GameObject[] LavaTiles;
    public GameObject[] WallTiles;
    public Count wallCount = new Count(1, 9);

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    private List<GameObject> Lava_land = new List<GameObject>();
    private List<GameObject> Normal_land = new List<GameObject>();
    private List<GameObject> Black_land = new List<GameObject>();

    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(1, 100)]
    public int randomFillPercent;

    int[,] map;

    void GenerateMap()
    {
       foreach(GameObject g in Lava_land)
        {
            Destroy(g);
        }
        Lava_land.Clear();
        foreach (GameObject g in Normal_land)
        {
            Destroy(g);
        }
        Normal_land.Clear();
        foreach (GameObject g in Black_land)
        {
            Destroy(g);
        }
        Black_land.Clear();

        map = new int[width, height];
        RandomFillMap();
        //for (int i = 0; i < 5; i++)
        //{
        //    SmoothMap();
        //}
        CreateMap();
        InitialiseList();
        
    }
    void RandomFillMap()
    {
        if(useRandomSeed)
        {
            seed = Time.time.ToString();
        }

        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {

                if(x==0 || x== width -1 || y ==0 || y == height -1)
                {
                    map[x, y] = 0;
                }
                else
                {
                   
                    int psuedoRandom = Random.Range(0, 100);
                    map[x, y] = (psuedoRandom  < randomFillPercent)? 1 : 2;
                   
                }
            }
        }
    }
    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);
                if (neighbourWallTiles > 1)
                    map[x, y] = 2;
                else if (neighbourWallTiles < 1)
                    map[x, y] = 1;
            }
        }
    }
    int GetSurroundingWallCount(int gridx, int gridy)
    {
        int wallCount = 0;
        for(int neighborX = gridx-1; neighborX <= gridx+1; neighborX++)
        {
            for(int neighbourY = gridy-1; neighbourY <= gridy+1; neighbourY++)
            {
                if(neighborX >= 0 && neighborX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if(neighborX != gridx || neighbourY != gridy)
                    {
                        wallCount += map[neighborX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPositions = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPositions;
    }
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        GameObject tileChoice = null;
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, transform.rotation);
            //if (tileChoice == tileArray[1])
            //{
            //    tileChoice.GetComponent<Transform>().localScale = new Vector3(2f, 2f, 2f);
            //}
            //else if (tileChoice == tileArray[2])
            //{
            //    tileChoice.GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
            //}
        }
    }
    //private void OnDrawGizmos()
    //{
    //    if (map != null)
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            for (int y = 0; y < height; y++)
    //            {
    //                Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
    //                Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
    //                Gizmos.DrawCube(pos, Vector3.one);
    //            }
    //        }
    //    }
    //}
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
                        Lava_land.Add(instance);
                        //instance.transform.SetParent(boardHolder);
                    }
                    else if(map[x, y] == 1)
                    {
                        GameObject toInstantiate = NormalTiles[Random.Range(0, NormalTiles.Length)];
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), transform.rotation) as GameObject;
                        Normal_land.Add(instance);
                        //instance.transform.SetParent(boardHolder);
                    } 
                    else if(map[x, y] == 2)
                    {
                        GameObject toInstantiate = BlackTiles[Random.Range(0, BlackTiles.Length)];
                        GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), transform.rotation) as GameObject;
                        GameObject toInstantiate1 = WallTiles[Random.Range(0, WallTiles.Length)];
                        GameObject instance1 = Instantiate(toInstantiate1, new Vector3(x, y, 0f), transform.rotation) as GameObject;
                        Black_land.Add(instance);
                       // instance.transform.SetParent(boardHolder);
                        //instance1.transform.SetParent(boardHolder);
                    }
                }
            }
        }
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
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }
}
