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
    //�������錳�ƂȂ�I�u�W�F�N�g�̃e���v���[�g
    public GameObject tileTemplate;
    private GameObject destroyObj;
    //���L�[�ňړ����邽�т�2��4�𐶐�����Ƃ��Ɏg���B
    private tileType[] randumInstantiateType = new[] { tileType.Num_2, tileType.Num_4 };
    //���������I�u�W�F�N�g�̃f�[�^�B
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
            //�e�ƂȂ�I�u�W�F�N�g�������_���Œ��I�B
            var enumRandomNum = UnityEngine.Random.Range(0, backgraundTiles.Length);
            //�I�u�W�F�N�g�𐶐��B
            var tileObj = Instantiate(tileTemplate);
            //�����_���Œ��I�����e�Ɉړ��B
            tileObj.transform.SetParent(backgraundTiles[enumRandomNum].transform);
            //tileObj�̒���tile�����邩���������āA����Ύ擾����B
            var tile = tileObj.GetComponent<Tile>();
            //��������^�C����2�ɐݒ�
            tile.SetTile(tileType.Num_2);
            //tileType��2�ɍX�V�B
            tile.tileType = tileType.Num_2;
            //�q�̈ʒu��e�̒��S�Ɏ����Ă���B
            tile.transform.localPosition = new Vector3(0, 0, 0);
            //���������I�u�W�F�N�g�����X�g�Ɋi�[�B
            tileData.Add(tile);
        }
    }

    // Update is called once per frame 
    void Update()
    {
        //����L�[�������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            //tile�𓮂����B��ɓ������̂�offset��-4
            MoveTiles(-4);
        }
        //�����L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            //tile�𓮂����B���ɓ������̂�offset�́{4
            MoveTiles(4);
        }
        //�����L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            //tile�𓮂����B���ɓ������̂�offset��-1
            MoveTiles(-1);
        }
        //�E���L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //tile�𓮂����B�E�ɓ������̂�offset�́{1
            MoveTiles(1);
        }

    }
    void MoveTiles(int offset)
    {
        //MoveTile�̒���tileData��Add���Ă���̂Ŗ������[�v�ɂȂ�Ȃ��悤�Ɉꎞ�ޔ��B
        var protoTileData = tileData.ToList();
        foreach (var tile in protoTileData)
        {
            MoveTile(offset,tile);
        }
    }
    /// <summary>
    /// tile���ړ�������
    /// </summary>
    /// <param name="offset">�ǂꂾ���ړ����邩�̒l</param>
    
    void MoveTile(int offset,Tile tile) 
    {
        //�ړ�����\��̈ʒu���Z�o
        int nextPosition = tile.currentPosition + offset ;
        //�ړ�����͈͂̎w��B�ړ�����\��̈ʒu��0��菬��������16�ȏ�̎��Ȃɂ����Ȃ�
        if(nextPosition < 0 || nextPosition >= TILEMAXNUM * TILEMAXNUM) 
        {
            return;
        }
        //�e��ς��邱�Ƃɂ���Ĉړ�����
        tile.transform.SetParent(backgraundTiles[nextPosition].transform);
        //�e��̈ʒu�ɍ��킹��
        tile.transform.localPosition = new Vector3(0, 0, 0);
        //�^�C���̌��݂̈ʒu��ۑ����Ă���currentPosition���X�V
        tile.currentPosition = nextPosition;
        //�e�̌��݂̃^�C�v���X�V
        backgraundTiles[nextPosition].tileType = tile.tileType;
        //�ړ�����O�̐e�̃^�C�v��������
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
            .Where(tile => tile.tileType == tileType.None) //Where(LInq)�͈�v����v�f������Ԃ�
            .ToArray(); //�^��Array�^�ɂ���iLinq�͒x���]���ɂȂ�B�����]�����Ă���j
        
        //type��None�̂��̂�������ꂽ���X�g���ŗ����̐������s��
        var randomNum = UnityEngine.Random.Range(0, noneTypeBackgraunds.Length);
        var enumList = Enum.GetValues(typeof(tileType));
        var enumRandomNum = UnityEngine.Random.Range(0, randumInstantiateType.Length);
        //object�̐����B�������錳�ƂȂ�e���v���[�g��tileTemplate
        var tileObj = Instantiate(tileTemplate);
        //tileObj�̒���tile�����邩���������āA����Ύ擾����B
        var tile = tileObj.GetComponent<Tile>();
        //���������I�u�W�F�N�g�����X�g�Ɋi�[�B
        tileData.Add(tile);
        //tileType�𕶎���ɕϊ�����B
        tile.SetTile(randumInstantiateType[enumRandomNum]);
        //tileType���X�V�B
        tile.tileType = randumInstantiateType[enumRandomNum];
        //��������e�����߂�
        tileObj.transform.SetParent(noneTypeBackgraunds[randomNum].transform);
        //�q(tileObj)�̈ʒu��e�̒��S�Ɏ����Ă���B
        tileObj.transform.localPosition = new Vector3(0, 0, 0);
        destroyObj = tileObj;
    }
}
