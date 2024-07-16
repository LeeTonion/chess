using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public GameObject controller;
    public GameObject move;
    private int xBoard =  - 1;
    private int yBoard = - 1;
    public string player;
    public Sprite black_king, red_king, black_queen, red_queen, black_bishop, red_bishop, black_rook, red_rook, black_pawn, red_pawn, black_knight,red_knight;
    public void activate()
    {
        SetCroords();
        controller = GameObject.FindGameObjectWithTag("GameController");
        switch(this.name)
        {
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king;player = "Black"; break;
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "Black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "Black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "Black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "Black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "Black"; break;

            case "red_king": this.GetComponent<SpriteRenderer>().sprite = red_king; player = "Red"; break;
            case "red_queen": this.GetComponent<SpriteRenderer>().sprite = red_queen; player = "Red"; break;
            case "red_bishop": this.GetComponent<SpriteRenderer>().sprite = red_bishop; player = "Red"; break;
            case "red_rook": this.GetComponent<SpriteRenderer>().sprite = red_rook; player = "Red"; break;
            case "red_pawn": this.GetComponent<SpriteRenderer>().sprite = red_pawn; player = "Red"; break;
            case "red_knight": this.GetComponent<SpriteRenderer>().sprite = red_knight; player = "Red"; break;



            
        }


    }
    public void SetCroords()
    {
        float x = xBoard;
        float y = yBoard;
        x *= 1f;
        y *= 1f;
        x += -3.5f;
        y += -3.5f;
        this.transform.position =  new Vector3(x, y, -1);

    }
    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard() {  return yBoard; }
    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    public void SetYBoard(int y)
    {
        yBoard = y;
    }
    private void OnMouseUp()
    {
        if (!controller.GetComponent<controll>().IsGameOver() && controller.GetComponent<controll>().GetCurrentPlayer() == player)
        { 
        DestroyMovePlates();
        InitiateMovePlates();
        }

       
    }
    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }
    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "red_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(1, 1);
                break;
            case "black_knight":
            case "red_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "red_bishop":
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(1, 1);
                break;
            case "black_king":
            case "red_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "red_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "red_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
        }
    }
    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        controll sc = controller.GetComponent<controll>();
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;
        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x,y)== null)
        {
            MovePlateSpawn(x,y);
            x += xIncrement;
            y += yIncrement;
        }
        if (sc.PositionOnBoard(x,y) && sc.GetPosition(x, y).GetComponent<game>().player != player)
        {
            MovePlateAttackSpawn(x,y);

        }

    }
    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard  + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1 );
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);

    }
    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard , yBoard + 1);
        PointMovePlate(xBoard , yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard );
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard -1);
        PointMovePlate(xBoard + 1, yBoard );
    }
    public void PointMovePlate(int x, int y)
    {
        controll sc = controller.GetComponent<controll>();
        if (sc.PositionOnBoard(x, y))
        {
             
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);

            }
            else if (sc.GetPosition(x, y).GetComponent<game>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x , int y)
    {
        controll sc = controller.GetComponent<controll>();
        if (sc.PositionOnBoard(x,y))
        {
            if (sc.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x, y);
            }
            if (sc.PositionOnBoard(x+1,y) && sc.GetPosition(x+1,y) != null && sc.GetPosition(x+1,y).GetComponent<game>().player != player)
            {
                MovePlateAttackSpawn(x+1, y);
            }
            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<game>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }
    public void MovePlateAttackSpawn(int matrixX , int matrixY)
    {
        float x = matrixX;
        float y = matrixY;
        x *= 1f;
        y *= 1f;
        x += -3.5f;
        y += -3.5f;
        GameObject mp =Instantiate(move, new Vector3(x,y,-3),Quaternion.identity);
        move mpScript = mp.GetComponent<move>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX,matrixY);


    }
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;
        x *= 1f;
        y *= 1f;
        x += -3.5f;
        y += -3.5f;
        GameObject mp = Instantiate(move, new Vector3(x, y, -3), Quaternion.identity);
        move mpScript = mp.GetComponent<move>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);


    }
}
