using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class RoguelikeMap : MonoBehaviour
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

    public int columns = 100;
    public int rows = 100;
    public Count wallCount = new Count(1, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] NormalTiles;
    public GameObject[] FireTiles;
    public GameObject[] LavaTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();
        for(int x = 1; x< columns -1; x++)
        {
            for(int y = 1; y < rows -1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = NormalTiles[Random.Range(0, NormalTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = LavaTiles[Random.Range(0, LavaTiles.Length)];

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), transform.rotation) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
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
        for (int i =0; i<objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, transform.rotation);
            if (tileChoice == tileArray[1])
            {
                tileChoice.GetComponent<Transform>().localScale = new Vector3(2f,2f, 2f);
            }
            else if (tileChoice == tileArray[2])
            {
                tileChoice.GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }      
    }

    public void SetupScene()
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(FireTiles, wallCount.minimum, wallCount.maximum);
       // LayoutObjectAtRandom(LavaTiles, foodCount.minimum, foodCount.maximum);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
