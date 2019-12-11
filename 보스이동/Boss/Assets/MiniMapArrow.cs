using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapArrow : MonoBehaviour
{
    public GameObject Player, Boss;
    Vector3 Player_pos, Boss_pos;
    // Start is called before the first frame update
    void Start()
    {
        Boss_pos = Boss.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Player_pos = Player.transform.position;
        float z = Search_Boss_Position(Player_pos, Boss_pos);

    }
    float Search_Boss_Position(Vector3 player, Vector3 Boss)
    {
        float z_rotate = 45f;
        double radians = 0;
        double angle = 0;


        /*if(Boss.x >= player.x && player.y <= Boss.y)
        {
           radians = Mathf.Atan2(Boss.x - player.x, Boss.y - player.y);
           angle = radians * (180 / Mathf.PI);
            z_rotate = (float)angle;
            Debug.Log(z_rotate);
        }*/

        return z_rotate;
    }
}
