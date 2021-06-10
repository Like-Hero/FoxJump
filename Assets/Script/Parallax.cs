using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;//摄像机

    public float xMoveRate;//移动比例
    public float yMoveRate;//移动比例


    private float startPointX;//初始X坐标
    private float startPointY;//初始Y坐标

    public bool lockY;//如果希望Y轴移动，则为false，否则为true
    private void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }
    private void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX + cam.transform.position.x * xMoveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPointX + cam.transform.position.x * xMoveRate, startPointY + cam.transform.position.y * yMoveRate);
        }
        
    }
}
