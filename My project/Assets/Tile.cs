using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public GameControl.tileType tileType = GameControl.tileType.Num_2;
    public int currentPosition = 0;
    public Text text;
    public void SetTile(GameControl.tileType type) 
    {
        text.text = GetString(type);
    }

    private string GetString(GameControl.tileType type)
    {
        switch (type) 
        {
            case GameControl.tileType.None:
                return "0";

            case GameControl.tileType.Num_2:
                return "2";

            case GameControl.tileType.Num_4:
                return "4";

            case GameControl.tileType.Num_8:
                return "8";

            case GameControl.tileType.Num_16:
                return "16";

            case GameControl.tileType.Num_32:
                return "32";

            case GameControl.tileType.Num_64:
                return "64";

            case GameControl.tileType.Num_128:
                return "128";

            case GameControl.tileType.Num_256:
                return "256";

            case GameControl.tileType.Num_512:
                return "512";

            case GameControl.tileType.Num_1024:
                return "1024";

            case GameControl.tileType.Num_2048:
                return "2048";

            default:
                return "0";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
