using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class AI_Enemy : TestCode
{
    private Vector3 cPosition;

    private Vector3[] a;

    // Start is called before the first frame update
    void Start()
    {
        cPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Moving_To_Player();

        //Debug.Log(pathArray.Count);
    }

    private void FixedUpdate()
    {
        try
        {
            if (pathArray.Count <= 500)
            {
                int index = 1;


                int i = 0;
                //int indext = 2;
                foreach (Node node in pathArray)
                {

                    Node nextNode = (Node)pathArray[index];
                    //Debug.Log(this.transform.position);

                    //Debug.Log(node.position);

                    transform.position += (nextNode.position - transform.position).normalized * speed;



                    //a[i] = node.position;

                    //i++;
                    //Node nextNodet = (Node)pathArray[indext];
                    //this.transform.position = Vector3.MoveTowards(this.transform.position, nextNode.position, 3);
                    //this.transform.position = Vector3.MoveTowards(this.transform.position, nextNodet.position, 3);

                }




            }
        }
        catch
        {

        }
    }

}