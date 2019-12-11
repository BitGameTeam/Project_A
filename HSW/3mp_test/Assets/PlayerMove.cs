using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movespped;
    // Start is called before the first frame update
    void Start()
    {
        movespped = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movespped * Input.GetAxis("Horizontal") * Time.deltaTime, movespped * Input.GetAxis("Vertical") * Time.deltaTime, 0f);
    }
}
