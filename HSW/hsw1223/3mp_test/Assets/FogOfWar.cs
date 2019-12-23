using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Box"))
        {
            SpriteRenderer wall_color = collision.gameObject.GetComponent<SpriteRenderer>();
            wall_color.color = new Color(0, 0, 0, .0f);
            wall_color.sortingOrder = 10;
           // Destroy(collision.gameObject);
        }
    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Box"))
        {
            SpriteRenderer wall_color = collision.gameObject.GetComponent<SpriteRenderer>();
            wall_color.color = new Color(0, 0, 0, .5f);
            wall_color.sortingOrder = 10;
            // Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
