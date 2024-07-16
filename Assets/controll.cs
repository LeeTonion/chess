using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controll : MonoBehaviour
{
    public GameObject controller;
    private GameObject[,] position = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerRed = new GameObject[16];
    private string currentPlayer = "Black";
    private bool gameOver = false;
    public TextMeshProUGUI winner;
    public TextMeshProUGUI start;
    
    void Start()
    {
        playerBlack = new GameObject[] {
        Create("black_rook",0,0),
        Create("black_rook", 7, 0),
        Create("black_knight", 6, 0),
        Create("black_knight", 1, 0),
        Create("black_pawn", 1, 1),
        Create("black_pawn", 2, 1),
        Create("black_pawn", 3, 1),
        Create("black_pawn", 4, 1),
        Create("black_pawn", 5, 1),
        Create("black_pawn", 6, 1),
        Create("black_pawn", 7, 1),
        Create("black_pawn", 0, 1),
        Create("black_bishop", 2, 0),
        Create("black_bishop", 5, 0),
        Create("black_king", 3, 0),
        Create("black_queen", 4, 0)
        };

        playerRed = new GameObject[]
        {
        Create("red_rook",0,7),
        Create("red_rook", 7,7),
        Create("red_knight", 6, 7),
        Create("red_knight", 1, 7),
        Create("red_pawn", 1, 6),
        Create("red_pawn", 2, 6),
        Create("red_pawn", 3, 6),
        Create("red_pawn", 4, 6),
        Create("red_pawn", 5, 6),
        Create("red_pawn", 6, 6),
        Create("red_pawn", 7, 6),
        Create("red_pawn", 0, 6),
        Create("red_bishop", 2, 7),
        Create("red_bishop", 5, 7),
        Create("red_king", 3, 7),
        Create("red_queen", 4, 7)
        };

        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerRed[i]);
        }


    }
    public GameObject Create(string name,int x,int y)
    {
        GameObject obj = Instantiate(controller, new Vector3(0, 0, -1), Quaternion.identity);
        game cm =obj.GetComponent<game>();
        cm.name = name;
        cm.SetXBoard(x); 
        cm.SetYBoard(y);
        cm.activate();
        return obj;
    }
    public void SetPosition(GameObject obj)
    {
        game cm = obj.GetComponent<game>();
     
        position[cm.GetXBoard(), cm.GetYBoard()]=obj;
    }
    public void SetPositionEmpty(int x,int y)
    {
        position[x,y]= null;
    }
    public GameObject GetPosition(int x,int y)
    {
        return position[x,y];
    }
    public bool PositionOnBoard(int x,int y)
    {
        if (x <  0 || y < 0 || x >= position.GetLength(0)  || y >= position.GetLength(1)) return false;
        return true;
    }
    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }
    public bool IsGameOver()
    {
        return gameOver;

    }
    public void NextTurn()
    {
        if(currentPlayer == "Red")
        {
            currentPlayer = "Black";
        }
        else
        {
            currentPlayer = "Red";
        }
    }
    public void Update()
    {
        if (gameOver == true && Input.GetMouseButton(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }
    }
    public void Winner(string playerWinner)
    {
        gameOver = true;
        winner.enabled = true;
        winner.text = playerWinner +" " + " winner";
        start.enabled = true;
        
    }
}

