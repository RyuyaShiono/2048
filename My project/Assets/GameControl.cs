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
        //����L�[�������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            //tile�𓮂����B��ɓ������̂�offset��-4
            MoveTile(-4);
        }
        //�����L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            //tile�𓮂����B���ɓ������̂�offset�́{4
            MoveTile(4);
        }
        //�����L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            //tile�𓮂����B���ɓ������̂�offset��-1
            MoveTile(-1);
        }
        //�E���L�[�������ꂽ�Ƃ�
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //tile�𓮂����B�E�ɓ������̂�offset�́{1
            MoveTile(1);
        }

    }
    /// <summary>
    /// tile���ړ�������
    /// </summary>
    /// <param name="offset">�ǂꂾ���ړ����邩�̒l</param>
    void MoveTile(int offset) 
    {
        //�ړ�����\��̈ʒu���Z�o
        int nextPosition = testTile.currentPosition + offset ;
        //�ړ�����͈͂̎w��B�ړ�����\��̈ʒu��0��菬��������16�ȏ�̎��Ȃɂ����Ȃ�
        if(nextPosition < 0 || nextPosition >= TILEMAXNUM * TILEMAXNUM) 
        {
            return;
        }
        //�e��ς��邱�Ƃɂ���Ĉړ�����
        testTile.transform.SetParent(backgraundTiles[nextPosition].transform);
        //�e��̈ʒu�ɍ��킹��
        testTile.transform.localPosition = new Vector3(0, 0, 0);
        //�^�C���̌��݂̈ʒu��ۑ����Ă���currentPosition���X�V
        testTile.currentPosition = nextPosition;
        //�e�̌��݂̃^�C�v���X�V
        backgraundTiles[nextPosition].tileType = testTile.tileType;
        //�ړ�����O�̐e�̃^�C�v��������
        backgraundTiles[testTile.currentPosition].tileType = tileType.None;
        CreateTile();
    }

    void CreateTile()
    {
        var noneTypeBackgraunds = backgraundTiles 
            .Where(tile => tile.tileType == tileType.None) //Where(LInq)�͈�v����v�f������Ԃ�
            .ToArray(); //�^��Array�^�ɂ���iLinq�͒x���]���ɂȂ�B�����]�����Ă���j
        
        //type��None�̂��̂�������ꂽ���X�g���ŗ����̐������s��
        var randomNum = Random.Range(0, noneTypeBackgraunds.Length);
        //object�̐����B�������錳�ƂȂ�e���v���[�g��tileTemplate
        var tile = Instantiate(tileTemplate);
        //��������e�����߂�
        tile.transform.SetParent(noneTypeBackgraunds[randomNum].transform);
        tile.transform.localPosition = new Vector3(0, 0, 0);
    }
}
