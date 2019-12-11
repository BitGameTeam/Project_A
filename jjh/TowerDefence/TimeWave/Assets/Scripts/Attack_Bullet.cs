using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Bullet : Attack_Tower
{
    private Vector3 e_Position;


    private void Start()
    {
        e_Position = t_Attack_Target.transform.position - this.transform.position;
        this.transform.rotation = 
            Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(e_Position), 3 * Time.deltaTime);

        

    }

    private void Update()
    {
        float step = 3 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, e_Position, step);



    }


}
