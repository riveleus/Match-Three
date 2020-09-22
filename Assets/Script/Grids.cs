using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour
{
    public int gridSizeX, gridSizeY;
    public Vector2 startPos, tileSize;
    public GameObject tilePrefab;
    public GameObject[,] tiles;
    public GameObject[] candies;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        tiles = new GameObject[gridSizeX, gridSizeY];
        tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size;
        startPos = transform.position + (Vector3.left * (tileSize.x * gridSizeX / 2)) + (Vector3.down * (tileSize.y * gridSizeY / 2));

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 pos = new Vector3(startPos.x + (x * tileSize.x), startPos.y + (y * tileSize.y));
                GameObject backgroundTile = Instantiate(tilePrefab, pos, tilePrefab.transform.rotation);
                backgroundTile.transform.parent = transform;
                backgroundTile.name = "(" + x + "," + y + ")";
                int index = Random.Range(0, candies.Length);
                int MAX_ITERATION = 0;
                while(MatchesAt(x, y, candies[index]) && MAX_ITERATION < 100)
                {
                    index = Random.Range(0, candies.Length);
                    MAX_ITERATION++;
                }
                MAX_ITERATION = 0;
                GameObject candy = ObjectPooler.instance.SpawnFromPool(index.ToString(), pos, Quaternion.identity);
                candy.name = "(" + x + "," + y + ")";
                tiles[x, y] = candy;
            }
        }
    }

    void DestroyMatchesAt(int column, int row)
    {
        if(tiles[column, row].GetComponent<Tile>().isMatched)
        {
            if(GameManager.instance.scoreMultiplier > 0)
                GameManager.instance.GetScore(10 * GameManager.instance.scoreMultiplier);
            else
                GameManager.instance.GetScore(10);
            tiles[column, row].gameObject.SetActive(false);
            tiles[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        GameManager.instance.scoreMultiplier++;
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                if(tiles[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }

        StartCoroutine(DecreaseRow());
    }

    bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (tiles[column - 1, row].tag == piece.tag && tiles[column - 2, row].tag == piece.tag)
                return true;
            if (tiles[column, row - 1].tag == piece.tag && tiles[column, row - 2].tag == piece.tag)
                return true;
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (tiles[column, row - 1].tag == piece.tag && tiles[column, row - 2].tag == piece.tag)
                    return true;
            }
            if (column > 1)
            {
                if (tiles[column - 1, row].tag == piece.tag && tiles[column - 2, row].tag == piece.tag)
                    return true;
            }
        }

        return false;
    }

    void RefillBoard()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if(tiles[x, y] == null)
                {
                    Vector2 tempPosition = new Vector3(startPos.x + (x * tileSize.x), startPos.y + (y * tileSize.y));
                    int candyToUse = Random.Range(0, candies.Length);
                    GameObject tileToRefill = ObjectPooler.instance.SpawnFromPool(candyToUse.ToString(), tempPosition, Quaternion.identity);
                    tileToRefill.GetComponent<Tile>().Init();
                    tiles[x, y] = tileToRefill;
                }
            }
        }
    }

    bool MatchesOnBoard()
    {
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                if(tiles[i, j] != null)
                {
                    if(tiles[i, j].GetComponent<Tile>().isMatched)
                        return true;
                }
            }
        }

        return false;
    }

    IEnumerator DecreaseRow()
    {
        int nullCount = 0;
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                if(tiles[i, j] == null)
                    nullCount++;
                else if(nullCount > 0)
                {
                    tiles[i, j].GetComponent<Tile>().row -= nullCount;
                    tiles[i, j] = null;
                }
            }

            nullCount = 0;
        }

        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoard());
    }

    IEnumerator FillBoard()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while(MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
    }
}
