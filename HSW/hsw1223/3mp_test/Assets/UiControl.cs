using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControl : MonoBehaviour
{
    public GameObject Worldmap;
    public bool Count;
    // Start is called before the first frame update
    void Start()
    {
        Count = false;
    }
    void MapOn(bool count)
    {
        if(count==true)
        {
            Worldmap.SetActive(true);
        }
        else
        {
            Worldmap.SetActive(false);
        }
    }
        
        

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && Count == false)
        {
            MapOn(Count);
            Count = true;
        }
        else if(Input.GetKeyDown(KeyCode.M) && Count == true)
        {
            MapOn(Count);
            Count = false;
        }
    }
}
