using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAI : MonoBehaviour
{
    public GameObject target;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Goplayer()
    {
      transform.position += (target.transform.position - transform.position).normalized* speed * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        Goplayer();
    }
}
