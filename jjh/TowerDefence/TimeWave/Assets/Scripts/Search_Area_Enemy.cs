using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Area_Enemy : MonoBehaviour
{

    Enemy_Control enemy_Ctrl;

    private void Start()
    {
        enemy_Ctrl = transform.root.GetComponent<Enemy_Control>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Wall")
        {
            enemy_Ctrl.move_Target = other.transform;
            Debug.Log("벽에 충돌함");

        }

        Debug.Log("충돌 안함");
    }

}
