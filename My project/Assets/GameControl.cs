using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public GameObject tileTemplate;
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


        var tile = Instantiate(tileTemplate);
        tile.transform.SetParent(backgraundTiles[3].transform);
    }

    // Update is called once per frame 
    void Update()
    {
        //上矢印キーが押されたとき
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            //tileを動かす。上に動かすのでoffsetは-4
            MoveTile(-4);
        }
        //下矢印キーが押されたとき
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            //tileを動かす。下に動かすのでoffsetは＋4
            MoveTile(4);
        }
        //左矢印キーが押されたとき
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            //tileを動かす。左に動かすのでoffsetは-1
            MoveTile(-1);
        }
        //右矢印キーが押されたとき
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //tileを動かす。右に動かすのでoffsetは＋1
            MoveTile(1);
        }

    }
    /// <summary>
    /// tileを移動させる
    /// </summary>
    /// <param name="offset">どれだけ移動するかの値</param>
    void MoveTile(int offset) 
    {
        //移動する予定の位置を算出
        int nextPosition = testTile.currentPosition + offset ;
        //移動する範囲の指定。移動する予定の位置が0より小さい又は16以上の時なにもしない
        if(nextPosition < 0 || nextPosition >= TILEMAXNUM * TILEMAXNUM) 
        {
            return;
        }
        //親を変えることによって移動する
        testTile.transform.SetParent(backgraundTiles[nextPosition].transform);
        //親基準の位置に合わせる
        testTile.transform.localPosition = new Vector3(0, 0, 0);
        //タイルの現在の位置を保存しているcurrentPositionを更新
        testTile.currentPosition = nextPosition;
        //親の現在のタイプを更新
        backgraundTiles[nextPosition].tileType = testTile.tileType;
        //移動する前の親のタイプを初期化
        backgraundTiles[testTile.currentPosition].tileType = tileType.None;
        CreateTile();
    }

    void CreateTile()
    {
        var noneTypeBackgraunds = backgraundTiles 
            .Where(tile => tile.tileType == tileType.None) //Where(LInq)は一致する要素だけを返す
            .ToArray(); //型をArray型にする（Linqは遅延評価になる。それを評価している）
        
        //typeがNoneのものだけを入れたリスト内で乱数の生成を行う
        var randomNum = Random.Range(0, noneTypeBackgraunds.Length);
        //objectの生成。生成する元となるテンプレートはtileTemplate
        var tile = Instantiate(tileTemplate);
        //生成する親をきめる
        tile.transform.SetParent(noneTypeBackgraunds[randomNum].transform);
        tile.transform.localPosition = new Vector3(0, 0, 0);
    }
}
