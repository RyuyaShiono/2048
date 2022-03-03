using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public enum tileType 
    { 
       None,
       Num_2,
       Num_4,
       Num_8,
       Num_16,
       Num_32,
       Num_64,
       Num_128,
       Num_256,
       Num_512,
       Num_1024,
       Num_2048,
    }
    const int TILEMAXNUM = 4;
    public Tile testTile;
    public BuckgraundTile[] backgraundTiles;
    private bool canInput = false;
    // Start is called before the first frame update
    void Start()
    {
        int roopCount = 0;
        for (int x = 0; x < TILEMAXNUM; x++)
        {
            for (int y = 0; y < TILEMAXNUM; y++)
            {
                roopCount++;
            }
        }

        //testTile.transform.SetParent(backgraundTiles[3].transform);

    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            MoveTile(-4); 
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            MoveTile(4);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            MoveTile(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTile(1);
        }

    }
    void MoveTile(int offset) 
    {
        int nextPosition = testTile.currentPosition + offset ;
        if(nextPosition < 0 || nextPosition >= TILEMAXNUM * TILEMAXNUM) 
        {
            return;
        }
        testTile.transform.SetParent(backgraundTiles[nextPosition].transform);
        testTile.transform.localPosition = new Vector3(0, 0, 0);
        testTile.currentPosition = nextPosition;
        UnityEngine.Debug.Log(nextPosition);
    }
}
