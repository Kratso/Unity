using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        int min;
        int max;
        public Count(int min, int max)
        {
            this.max = max;
            this.min = min;
        }
    }

    public int column = 8;
    public int row = 8;

    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);

    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;

    private List<Vector3> gridPositions = new List<Vector3>();


    void initializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < column - 1; x++)
        {
            for (int y = 1; y < row - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }


    void boardSetup()
    {
        boardHolder = new GameObject("board").transform;

        for (int x = -1; x < column + 1; x++)
        {
            for (int y = -1; y < row + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                if (x == -1 || x == column || y == -1 || y == row)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                GameObject instance =
                    Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
