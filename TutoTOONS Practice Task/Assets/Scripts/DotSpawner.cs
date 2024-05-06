using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject dotPrefab;
    [SerializeField]
    GameObject[] dots;

    Vector2[] coordinatesList;
    List<int> drawingQue= new List<int>();

    public static DotSpawner Instance;

    public bool isDrawing;

    private void Awake()
    {
        Instance = this;
        coordinatesList = GameData.Instance.levelsCoordinatesList[GameData.Instance.levelToLoadIndex].coordinates;
        dots = new GameObject[coordinatesList.Length];
    }

    private void Start()
    {
        SpawnDots();   
    }

    private void Update()
    {
        if (!isDrawing && drawingQue.Count > 0)
        {
            if (!dots[drawingQue[0]].GetComponent<Dot>().connected)
            {
                isDrawing = true;
                dots[drawingQue[0]].GetComponent<Dot>().StartCoroutine(
                dots[drawingQue[0]].GetComponent<Dot>().DrawRope(dots[drawingQue[0]].transform.position, dots[drawingQue[0] + 1].transform.position));
                drawingQue.RemoveAt(0);
            }

            else if (drawingQue[0] == dots.Length - 2 && dots[drawingQue[0]].GetComponent<Dot>().connected)
            {
                dots[dots.Length - 1].GetComponent<Dot>().StartCoroutine(
                dots[dots.Length - 1].GetComponent<Dot>().DrawRope(dots[dots.Length - 1].transform.position, dots[0].transform.position));
                drawingQue.RemoveAt(0);
            }
        }

        if (dots[dots.Length-1].GetComponent<Dot>().connected)
        {
            UIManager.Instance.ShowWinScreen();
        }
    }

    void SpawnDots()
    {
        for (int i = 0; i < coordinatesList.Length; i++)
        {
            float x;
            float y;

                x = (coordinatesList[i].x / 1000 + 0.05f) * (Screen.width * 0.85f);
                y = (coordinatesList[i].y / 1000 + 0.05f) *(Screen.height * 0.85f);

            GameObject dot = Instantiate(dotPrefab, Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0)), Quaternion.identity);
            dot.transform.position = new Vector3(dot.transform.position.x, dot.transform.position.y, 0);
            dot.GetComponent<Dot>().SetTextNumber(i+1);
            dots[i] = dot;
        }
    }

    public bool PreviousDotClicked(int index)
    {
        if (dots[index].GetComponent<Dot>().clicked)
        {
            drawingQue.Add(index);
            if(index == dots.Length-2) drawingQue.Add(index);
        }

        return dots[index].GetComponent<Dot>().clicked;
    }
}
