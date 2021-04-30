using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BoardManager : MonoBehaviour
{

    public class Count
    {
        public int minmum;
        public int maximum;

        public Count(int min, int max)
        {
            minmum = min;
            maximum = max;
        }
    }

    static int MapWidth = 50;
    static int MapHeight = 50;

   public int[,] Map;

    const int wall = 9;
    const int room = 5;
    const int road = 0;

    public GameObject[] wallTiles;
    public GameObject[] floorTiles;
    public GameObject[] foodTiles;
    public GameObject[] itemTiles;
    public GameObject[] enemyTiles;
    public GameObject[] bossTiles;
    public GameObject[] Exit;
    public GameObject meadowStage;
    public GameObject titleStage;
    public GameObject player;

    public Count foodcount = new Count(2,5);
    public Count itemcount = new Count(5, 10);
    public Count enemycount = new Count(5,10);
    public Count exitcount = new Count(1, 1);

    public List<(Vector2 start, Vector2 end)> roomSize = new List<(Vector2 start, Vector2 end)>();    //タプル
    public List<Vector2> roomData = new List<Vector2>();  //LoadされるたびにListに新しい座標を入れる
    public int roomMinHeight = 5;  
    public int roomMaxHeight = 10;

    public int roomMinWidth = 5;
    public int roomMaxWidth = 10;

    public int RoomCountMin = 10;
    public int RoomCountMax = 15;

    private Transform polygonCollider2;
  
    //道の集合点を増やしたいならこれを増やす
    public int meetPointCount = 1;
    
    public void SetupScene()
    {
        polygonCollider2 = GameObject.Find("CameraCollider").transform;
        if (SaveSystem.Instance.UserData.currentStage%10 != 0 && GManager.instance.isGameOver == false)
        {
            ResetMapData();

            CreateSpaceData();

            CreateDangeon();

            Instantiate(player, RandomPosition(), Quaternion.identity);

            LayoutObjectAtRandom(foodTiles, foodcount.minmum, foodcount.maximum);

            LayoutObjectAtRandom(itemTiles, itemcount.minmum, itemcount.maximum);

            LayoutObjectAtRandom(enemyTiles, enemycount.minmum, enemycount.maximum);

            LayoutObjectAtRandom(Exit, exitcount.minmum, exitcount.maximum);

            InitialiseList();

            polygonCollider2.localPosition = new Vector3(0, 0, 0);
            polygonCollider2.localScale = new Vector2(25, 25);
        }
        else if(SaveSystem.Instance.UserData.currentStage % 10 == 0 && GManager.instance.isGameOver == false)
        {

            Transform startPos = GameObject.Find("StartPosition").transform;
            Transform bossPos = GameObject.Find("BossPosition").transform;
            Instantiate(meadowStage, Vector3.zero, Quaternion.identity);
            Instantiate(player, startPos.position,Quaternion.identity);
            Instantiate(bossTiles[0], bossPos.position,Quaternion.identity);
            polygonCollider2.localPosition = new Vector3(0.5f, 10, 0);
            polygonCollider2.localScale = new Vector2(14, 16);
        }
        else if(GManager.instance.isGameOver == true)
        {
            Transform titlePos = GameObject.Find("TitlePosition").transform;
            Instantiate(titleStage, Vector3.zero, Quaternion.identity);
            Instantiate(player, titlePos.position, Quaternion.identity);
            polygonCollider2.localPosition = new Vector3(1, 0.5f, 0);
            polygonCollider2.localScale = new Vector2(8, 8.5f);
        }

    }

    

    /// <summary>
    /// Mapの二次元配列の初期化
    /// </summary>
    void ResetMapData()
    {
        Map = new int[MapHeight, MapWidth];
        roomSize.Clear();
        for (int i = 0; i < MapHeight; i++)
        {
            for (int j = 0; j < MapWidth; j++)
            {
                Map[i, j] = wall;
            }
        }
    }

    /// <summary>
    /// 空白部分のデータを変更
    /// </summary>
    private void CreateSpaceData()
    {
        int roomCount = Random.Range(RoomCountMin, RoomCountMax);

        int[] meetPointsX = new int[meetPointCount];
        int[] meetPointsY = new int[meetPointCount];
        for (int i = 0; i < meetPointsX.Length; i++)
        {
            meetPointsX[i] = Random.Range(MapWidth / 4, MapWidth * 3 / 4);
            meetPointsY[i] = Random.Range(MapHeight / 4, MapHeight * 3 / 4);
            Map[meetPointsY[i], meetPointsX[i]] = road;
        }

        for (int i = 0; i < roomCount; i++)
        {
            int roomHeight = Random.Range(roomMinHeight, roomMaxHeight);
            int roomWidth = Random.Range(roomMinWidth, roomMaxWidth);
            int roomPointX = Random.Range(1, MapWidth - roomMaxWidth - 2);
            int roomPointY = Random.Range(1, MapWidth - roomMaxWidth - 2);

            int roadStartPointX = Random.Range(roomPointX, roomPointX + roomWidth);
            int roadStartPointY = Random.Range(roomPointY, roomPointY + roomHeight);

            bool isRoad = CreateRoomData(roomHeight, roomWidth, roomPointX, roomPointY);

            if (isRoad == false)
            {
                CreateRoadData(roadStartPointX, roadStartPointY, meetPointsX[Random.Range(0, 0)], meetPointsY[Random.Range(0, 0)]);
            }
        }


    }

    /// <summary>
    /// 部屋データを生成。すでに部屋がある場合はtrueを返し、道を作らないようにする
    /// </summary>
    /// <param name="roomHeight">部屋の高さ</param>
    /// <param name="roomWidth">部屋の横幅</param>
    /// <param name="roomPointX">部屋の始点(x)</param>
    /// <param name="roomPointY">部屋の始点(y)</param>
    /// <returns></returns>
    private bool CreateRoomData(int roomHeight, int roomWidth, int roomPointX, int roomPointY)
    {

        bool isRoad = false;
        for (int i = 0; i < roomHeight; i++)
        {
            for (int j = 0; j < roomWidth; j++)
            {
                if(roomData != null)
                {
                    for(int k = 0; k < roomData.Count; k++)
                    {
                        if(roomData[k] == new Vector2(roomPointX + j, roomPointY + i))
                        {
                            roomData.Remove(roomData[k]);
                        }
                    }
                }
                roomData.Add(new Vector2(roomPointX + j, roomPointY + i));
               
               

                if (Map[roomPointY + i, roomPointX + j] == room)
                {
                    isRoad = true;
                   
                }
                else
                {
                    Map[roomPointY + i, roomPointX + j] = room;
                }

            }
        }
     
        return isRoad;
    }

    /// <summary>
    /// 道データを生成
    /// </summary>
    /// <param name="roadStartPointX"></param>
    /// <param name="roadStartPointY"></param>
    /// <param name="meetPointX"></param>
    /// <param name="meetPointY"></param>

    private void CreateRoadData(int roadStartPointX, int roadStartPointY, int meetPointX, int meetPointY)
    {

        bool isRight;
        if (roadStartPointX > meetPointX)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        bool isUnder;
        if (roadStartPointY > meetPointY)
        {
            isUnder = false;
        }
        else
        {
            isUnder = true;
        }

        if (Random.Range(0, 2) == 0)//最初に横から掘るか縦から掘るかランダムに決めている
        {

            while (roadStartPointX != meetPointX)
            {

                Map[roadStartPointY, roadStartPointX] = room;
                if (isRight == true)
                {
                    roadStartPointX--;
                }
                else
                {
                    roadStartPointX++;
                }

            }

            while (roadStartPointY != meetPointY)
            {

                Map[roadStartPointY, roadStartPointX] = room;
                if (isUnder == true)
                {
                    roadStartPointY++;
                }
                else
                {
                    roadStartPointY--;
                }

            }

        }
        else
        {

            while (roadStartPointY != meetPointY)
            {

                Map[roadStartPointY, roadStartPointX] = room;
                if (isUnder == true)
                {
                    roadStartPointY++;
                }
                else
                {
                    roadStartPointY--;
                }
                Map[roadStartPointX, roadStartPointY] = road;
                Map[meetPointX, meetPointY] = road;
            }

            while (roadStartPointX != meetPointX)
            {

                Map[roadStartPointY, roadStartPointX] = room;
                if (isRight == true)
                {
                    roadStartPointX--;
                }
                else
                {
                    roadStartPointX++;
                }
                Map[roadStartPointX, roadStartPointY] = road;
                Map[meetPointX, meetPointY] = road;

            }


        }

    }

    /// <summary>
    /// マップデータをもとにダンジョンを生成
    /// </summary>
    private void CreateDangeon()
    {
        for (int i = 0; i < MapHeight; i++)
        {
            for (int j = 0; j < MapWidth; j++)
            {
                if (Map[i, j] == wall)
                {
                    Instantiate(wallTiles[Random.Range(0,wallTiles.Length)], new Vector3(j - MapWidth / 2, i - MapHeight / 2, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(floorTiles[Random.Range(0,floorTiles.Length)], new Vector3(j - MapWidth / 2, i - MapHeight / 2, 0), Quaternion.identity);

                }
            }
        }
    }

    private void InitialiseList()
    {
        roomData.Clear();
    }

    Vector3 RandomPosition()
    {
       
        Vector3 randomPosition;
       
       
        //randomIndexを宣言して、gridPositionsの数から数値をランダムで入れる
        int randomIndex = Random.Range(0,roomData.Count-1);
      
        //randomPositionを宣言して、gridPositionsのrandomIndexに設定する
         randomPosition = new Vector3(roomData[randomIndex].x - MapWidth / 2, roomData[randomIndex].y - MapHeight / 2, 0);
       

        //使用したgridPositionsの要素を削除
        roomData.Remove(roomData[randomIndex]);

        return randomPosition;
    }

    //Mapにランダムで引数のものを配置する(敵、壁、アイテム)
   private void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //生成するアイテムの個数を最小最大値からランダムに決め、objectCountに設定する
        int objectCount = Random.Range(minimum, maximum);

        //設置するオブジェクトの数分ループで回す
        for (int i = 0; i < objectCount; i++)
        {
            //現在オブジェクトが置かれていない、ランダムな位置を取得
            Vector3 randomPosition = RandomPosition();

            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];　//生成するアイテムのタイル番号を獲得している


            //生成
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
}



