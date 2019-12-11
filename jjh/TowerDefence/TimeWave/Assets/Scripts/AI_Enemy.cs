using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class AI_Enemy : MonoBehaviour
{
    public TestCode tCode;

    private GameObject[] a;

    // Start is called before the first frame update
    void Start()
    {
        a = tCode.pathArray.ToArray(typeof(GameObject)) as GameObject[];


    }

    // Update is called once per frame
    void Update()
    {
        Moving_To_Player();
    }

    private void Moving_To_Player()
    {
        //if(tCode.pathArray.Count <= 5)
        //{
            
        //}
    }
}
