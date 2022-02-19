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

        testTile.transform.SetParent(backgraundTiles[3].transform);

    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) { }
        else if (Input.GetKey(KeyCode.DownArrow)) { }
        else if (Input.GetKey(KeyCode.LeftArrow)) { }
        else if (Input.GetKey(KeyCode.RightArrow) && canInput == false)
        {
            canInput = true;
            int nextPosition = testTile.currentPosition + 1;
            testTile.transform.SetParent(backgraundTiles[nextPosition].transform);
            testTile.currentPosition = nextPosition;
            UnityEngine.Debug.Log(nextPosition);
            canInput = false;
        }

    }
}
