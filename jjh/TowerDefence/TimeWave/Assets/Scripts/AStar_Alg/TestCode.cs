using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    public float speed = 2;

    private Transform startPos, endPos;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }

    public static ArrayList pathArray;

    GameObject objStartCube, objEndCube;
    private float elapsedTime = 0f;

    // 경로탐색 사이의 시간 간격
    public float intervalTime = 0.1f;

    private Rigidbody2D rb;

    void Start()
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");

        pathArray = new ArrayList();
        FindPath();
    }

    private void FindPath()
    {
        startPos = objStartCube.transform;
        endPos = objEndCube.transform;

        startNode = new Node(GridManager.instance.GetGridCellCenter(
            GridManager.instance.GetGridIndex(startPos.position)));

        goalNode = new Node(GridManager.instance.GetGridCellCenter(
            GridManager.instance.GetGridIndex(endPos.position)));

       

        
        pathArray = AStar.FindPath(startNode, goalNode);


        Debug.Log("" + pathArray.Count);









        //if (pathArray.Count <= 500)
        //{
        //    int index = 1;
        //    int indext = 2;
        //    foreach (Node node in pathArray)
        //    {
        //        Chacking_Cardinalpoints(node);


        //        Node nextNode = (Node)pathArray[index];
        //        Node nextNodet = (Node)pathArray[indext];
        //        this.transform.position = Vector3.MoveTowards(this.transform.position, nextNode.position, 3);
        //        this.transform.position = Vector3.MoveTowards(this.transform.position, nextNodet.position, 3);

        //    }
        //}



    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            elapsedTime = 0f;
            FindPath();
        }
    }

    private void FixedUpdate()
    {
        //if (pathArray.Count <= 500)
        //{
        //    int index = 1;

            

        //    //int indext = 2;
        //    foreach (Node node in pathArray)
        //    {
                
        //        Node nextNode = (Node)pathArray[index];
        //        //Debug.Log(this.transform.position);

        //        //Debug.Log(node.position);

        //        transform.position += /*(node.position - transform.position).normalized * speed * Time.deltaTime*/node.position.normalized * speed * Time.deltaTime;

        //        //Node nextNodet = (Node)pathArray[indext];
        //        //this.transform.position = Vector3.MoveTowards(this.transform.position, nextNode.position, 3);
        //        //this.transform.position = Vector3.MoveTowards(this.transform.position, nextNodet.position, 3);

        //    }


        //}
    }


    void OnDrawGizmos()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach(Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = (Node)pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.green);
                    ++index;
                }
            }
        }
    }
}
