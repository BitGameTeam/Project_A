using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public RoguelikeMap roguelike;
    // Start is called before the first frame update
    private void Awake()
    {
        roguelike = GetComponent<RoguelikeMap>();
    }
    void InitGame()
    {
        roguelike.SetupScene();
    }
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
