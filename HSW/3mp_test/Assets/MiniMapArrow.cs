using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapArrow : MonoBehaviour
{
    public GameObject Player, Boss, RedCircle;
    public float move_speed;
    public float arrow_Zposition;
    Vector3 Player_pos, Boss_pos;
    // Start is called before the first frame update
    void Start()
    {
        
        arrow_Zposition = this.transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        Player_pos = Player.transform.position;
        Boss_pos = Boss.transform.position;
        float z = Search_Boss_Position(Player_pos, Boss_pos);
        this.transform.rotation = Quaternion.Euler(0, 0, -z);
    }
    float Search_Boss_Position(Vector3 player, Vector3 Boss)
    {
        float z_rotate = 0f;
        double radians = 0;
        double angle = 0;
        if((Boss.y - player.y <=5 && Boss.y - player.y >= -5) && (Boss.x - player.x <= 5 && Boss.x - player.x >= -5))
        {
            RedCircle.SetActive(true);
        }
        else
        {
            RedCircle.SetActive(false);
        }
        radians = Mathf.Atan2(Boss.x - player.x, Boss.y - player.y);
        angle = radians * (180 / Mathf.PI);
        z_rotate = (float)angle;


        return z_rotate;
    }
}
