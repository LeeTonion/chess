using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject controller;
    GameObject reference = null;
    int matrixX;
    int matrixY;
    public bool attack = false;
    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0,1);

        }
    }
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (attack)
        {
            GameObject cp =controller.GetComponent<controll>().GetPosition(matrixX,matrixY);
            if (cp.name == "black_king") controller.GetComponent<controll>().Winner("red");
            if (cp.name == "red_king") controller.GetComponent<controll>().Winner("black");
            Destroy(cp);
        }
        
        controller.GetComponent<controll>().SetPositionEmpty(reference.GetComponent<game>().GetXBoard(),reference.GetComponent<game>().GetYBoard());
        reference.GetComponent<game>().SetXBoard(matrixX);
        reference.GetComponent<game>().SetYBoard(matrixY);
        reference.GetComponent<game>().SetCroords();
        controller.GetComponent<controll>().NextTurn();
        controller.GetComponent<controll>().SetPosition(reference);
        reference.GetComponent<game>().DestroyMovePlates();

    }
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference( GameObject obj)
    {  reference = obj; }
    public GameObject GetReference() { return reference; }
}
