using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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
    public BuckgraundTile[] backgraundTiles;
    //生成する元となるオブジェクトのテンプレート
    public GameObject tileTemplate;
    private GameObject destroyObj;
    //矢印キーで移動するたびに2か4を生成するときに使う。
    private tileType[] randumInstantiateType = new[] { tileType.Num_2, tileType.Num_4 };
    //生成したオブジェクトのデータ。
    private List<Tile> tileData = new List<Tile>(); 
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
        for (var i = 0; i < 2; i++)
        {
            //親となるオブジェクトをランダムで抽選。
            var enumRandomNum = UnityEngine.Random.Range(0, backgraundTiles.Length);
            //オブジェクトを生成。
            var tileObj = Instantiate(tileTemplate);
            //ランダムで抽選した親に移動。
            tileObj.transform.SetParent(backgraundTiles[enumRandomNum].transform);
            //tileObjの中にtileがあるかを検索して、あれば取得する。
            var tile = tileObj.GetComponent<Tile>();
            //生成するタイルを2に設定
            tile.SetTile(tileType.Num_2);
            //tileTypeを2に更新。
            tile.tileType = tileType.Num_2;
            //子の位置を親の中心に持ってくる。
            tile.transform.localPosition = new Vector3(0, 0, 0);
            //生成したオブジェクトをリストに格納。
            tileData.Add(tile);
        }
    }

    // Update is called once per frame 
    void Update()
    {
        //上矢印キーが押されたとき
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            //tileを動かす。上に動かすのでoffsetは-4
            MoveTiles(-4);
        }
        //下矢印キーが押されたとき
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            //tileを動かす。下に動かすのでoffsetは＋4
            MoveTiles(4);
        }
        //左矢印キーが押されたとき
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            //tileを動かす。左に動かすのでoffsetは-1
            MoveTiles(-1);
        }
        //右矢印キーが押されたとき
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //tileを動かす。右に動かすのでoffsetは＋1
            MoveTiles(1);
        }

    }
    void MoveTiles(int offset)
    {
        //MoveTileの中でtileDataをAddしているので無限ループにならないように一時退避。
        var protoTileData = tileData.ToList();
        foreach (var tile in protoTileData)
        {
            MoveTile(offset,tile);
        }
    }
    /// <summary>
    /// tileを移動させる
    /// </summary>
    /// <param name="offset">どれだけ移動するかの値</param>
    
    void MoveTile(int offset,Tile tile) 
    {
        //移動する予定の位置を算出
        int nextPosition = tile.currentPosition + offset ;
        //移動する範囲の指定。移動する予定の位置が0より小さい又は16以上の時なにもしない
        if(nextPosition < 0 || nextPosition >= TILEMAXNUM * TILEMAXNUM) 
        {
            return;
        }
        //親を変えることによって移動する
        tile.transform.SetParent(backgraundTiles[nextPosition].transform);
        //親基準の位置に合わせる
        tile.transform.localPosition = new Vector3(0, 0, 0);
        //タイルの現在の位置を保存しているcurrentPositionを更新
        tile.currentPosition = nextPosition;
        //親の現在のタイプを更新
        backgraundTiles[nextPosition].tileType = tile.tileType;
        //移動する前の親のタイプを初期化
        backgraundTiles[tile.currentPosition].tileType = tileType.None;
        CreateTile();
    }

    void CreateTile()
    {
        //if(destroyObj != null)
        //{
        //    Destroy(destroyObj);
        //}
        var noneTypeBackgraunds = backgraundTiles 
            .Where(tile => tile.tileType == tileType.None) //Where(LInq)は一致する要素だけを返す
            .ToArray(); //型をArray型にする（Linqは遅延評価になる。それを評価している）
        
        //typeがNoneのものだけを入れたリスト内で乱数の生成を行う
        var randomNum = UnityEngine.Random.Range(0, noneTypeBackgraunds.Length);
        var enumList = Enum.GetValues(typeof(tileType));
        var enumRandomNum = UnityEngine.Random.Range(0, randumInstantiateType.Length);
        //objectの生成。生成する元となるテンプレートはtileTemplate
        var tileObj = Instantiate(tileTemplate);
        //tileObjの中にtileがあるかを検索して、あれば取得する。
        var tile = tileObj.GetComponent<Tile>();
        //生成したオブジェクトをリストに格納。
        tileData.Add(tile);
        //tileTypeを文字列に変換する。
        tile.SetTile(randumInstantiateType[enumRandomNum]);
        //tileTypeを更新。
        tile.tileType = randumInstantiateType[enumRandomNum];
        //生成する親をきめる
        tileObj.transform.SetParent(noneTypeBackgraunds[randomNum].transform);
        //子(tileObj)の位置を親の中心に持ってくる。
        tileObj.transform.localPosition = new Vector3(0, 0, 0);
        destroyObj = tileObj;
    }
}
