using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCtrol : MonoBehaviour
{
    public GameObject Player;
    Transform trs;
    // Start is called before the first frame update
    void Start()
    {
        trs = Player.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(trs.position.x, trs.position.y, -8);
    }
}
