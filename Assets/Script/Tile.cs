using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector3 firstPosition;
    private Vector3 finalPosition;
    private float swipeAngle;
    private Vector3 tempPosition;
    public float xPosition;
    public float yPosition;
    public int column;
    public int row;
    private Grids grid;
    private GameObject otherTile;
    public bool isMatched = false;
    private int previousColumn;
    private int previousRow;

    void Start()
    {
        grid = FindObjectOfType<Grids>();
        xPosition = transform.position.x;
        yPosition = transform.position.y;
        column = Mathf.RoundToInt((xPosition - grid.startPos.x) / grid.tileSize.x);
        row = Mathf.RoundToInt((yPosition - grid.startPos.y) / grid.tileSize.y);
    }

    void Update()
    {
        CheckMatches();
        xPosition = (column * grid.tileSize.x) + grid.startPos.x;
        yPosition = (row * grid.tileSize.y) + grid.startPos.y;
        SwipeTile();
    }

    public void Init()
    {
        
    }

    void CheckMatches()
    {
        if(column > 0 && column < grid.gridSizeX - 1)
        {
            GameObject leftTile = grid.tiles[column - 1, row];
            GameObject rightTile = grid.tiles[column + 1, row];
            if(leftTile != null && rightTile != null)
            {
                if(leftTile.CompareTag(gameObject.tag) && rightTile.CompareTag(gameObject.tag))
                {
                    isMatched = true;
                    rightTile.GetComponent<Tile>().isMatched = true;
                    leftTile.GetComponent<Tile>().isMatched = true;
                }
            }
        }

        if(row > 0 && row < grid.gridSizeY - 1)
        {
            GameObject downTile = grid.tiles[column, row - 1];
            GameObject upTile = grid.tiles[column, row + 1];
            if(downTile != null && upTile != null)
            {
                if(downTile.CompareTag(gameObject.tag) && upTile.CompareTag(gameObject.tag))
                {
                    isMatched = true;
                    upTile.GetComponent<Tile>().isMatched = true;
                    downTile.GetComponent<Tile>().isMatched = true;
                }
            }
        }

        if(isMatched)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.gray;
        }
    }

    IEnumerator CheckMove()
    {
        yield return new WaitForSeconds(.5f);

        if(otherTile != null)
        {
            if(!isMatched && !otherTile.GetComponent<Tile>().isMatched)
            {
                otherTile.GetComponent<Tile>().row = row;
                otherTile.GetComponent<Tile>().column = column;
                row = previousRow;
                column = previousColumn;
                GameManager.instance.scoreMultiplier = 1;
            }
            else
            {
                grid.DestroyMatches();
            }
        }

        otherTile = null;
    }

    void SwipeTile()
    {
        if(Mathf.Abs(xPosition - transform.position.x) > .1)
        {
            tempPosition = new Vector2(xPosition, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            tempPosition = new Vector2(xPosition, transform.position.y);
            transform.position = tempPosition;
            grid.tiles[column, row] = this.gameObject;
        }

        if(Mathf.Abs(yPosition - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, yPosition);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else 
        {
            tempPosition = new Vector2(transform.position.x, yPosition);
            transform.position = tempPosition;
            grid.tiles[column, row] = this.gameObject;
        }
    }

    void MoveTile()
    {
        previousColumn = column;
        previousRow = row;

        if(swipeAngle > -45 && swipeAngle <= 45 && column < grid.gridSizeX)
        {
            SwipeRightMove();
        }
        else if(swipeAngle > 45 && swipeAngle <= 135 && row < grid.gridSizeY)
        {
            SwipeUpMove();
        }
        else if(swipeAngle > 135 || swipeAngle <= -135 && column > 0)
        {
            SwipeLeftMove();
        }
        else if(swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            SwipeDownMove();
        }

        StartCoroutine(CheckMove());
    }

    void SwipeRightMove()
    {
        if(column + 1 < grid.gridSizeX)
        {
            //Menukar posisi dengan sebelah kanan nya
            otherTile = grid.tiles[column + 1, row];
            otherTile.GetComponent<Tile>().column -= 1;
            column += 1;
        }
    }

    void SwipeUpMove()
    {
        if(row + 1 < grid.gridSizeY)
        {
            //Menukar posisi dengan sebelah atas nya
            otherTile = grid.tiles[column, row + 1];
            otherTile.GetComponent<Tile>().row -= 1;
            row += 1;
        }
    }

    void SwipeLeftMove()
    {
        if(column - 1 >= 0)
        {
            //menukar posisi dengan sebelah kiri nya
            otherTile = grid.tiles[column - 1, row];
            otherTile.GetComponent<Tile>().column += 1;
            column -= 1;
        }
    }

    void SwipeDownMove()
    {
        if(row - 1 >= 0)
        {
            //menukar posisi dengan sebelah bawah nya
            otherTile = grid.tiles[column, row - 1];
            otherTile.GetComponent<Tile>().row += 1;
            row -= 1;
        }
    }

    void OnMouseDown()
    {
        firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalPosition.y - firstPosition.y, finalPosition.x - firstPosition.x) * 180 / Mathf.PI;
        MoveTile();
    }
}
