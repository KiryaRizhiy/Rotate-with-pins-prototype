﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotator : MonoBehaviour
{
    public static Vector3 ScreenPosition;
    private float alpha, beta, displacementProjection, previousRotation;
    void Start()
    {
        ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerControl.displacement != Vector2.zero)
        {
            //Находим расстояние  от position до прямой O^position+displacement
            Vector2 movePoint = PlayerControl.displacement + PlayerControl.position;
            displacementProjection = Mathf.Abs(
                    (movePoint.y / movePoint.x) * PlayerControl.position.x - PlayerControl.position.y
                ) /
                Mathf.Sqrt(
                    Mathf.Pow(movePoint.y / movePoint.x, 2f) + 1
                );
            //Находим углы alpha и beta
            //Вычисляем угол между векторами (позиция, ось Х)
            alpha = Mathf.Acos(PlayerControl.position.x / PlayerControl.position.magnitude);
            //Если позиция ниже 0 по Y, прибавляем к углу 180
            if (PlayerControl.position.y < 0)
                alpha += Mathf.PI;
            //Вычисляем угол между векторами (позиция, ось Х)
            beta = Mathf.Acos(movePoint.x / movePoint.magnitude);
            //Если позиция ниже 0 по Y, прибавляем к углу 180
            if (movePoint.y < 0)
                beta += Mathf.PI;
            if ((PlayerControl.position.y < 0 && alpha < beta) || (PlayerControl.position.y > 0 && alpha > beta))
                displacementProjection = (-1) * displacementProjection;
            //Logger.AddContent(UILogDataType.Computations,"Move point: " + movePoint + "Distance " + distance + "Alpha " + alpha + "Beta " + beta );
        }
        else
            displacementProjection = 0f;
        Rotate(displacementProjection);
    }
    void Rotate(float rotation)
    {
        if (Mathf.Abs(rotation - previousRotation) > Settings.accelerationLimit)//Предельное ускорение
        {
            Logger.AddContent(UILogDataType.Computations, "Rotation acceleration limit reached!", true);
            rotation = previousRotation + Settings.accelerationLimit * Mathf.Sign(rotation - previousRotation);
        }
        if (Mathf.Abs(rotation) > Settings.rotationLimit)//Предельная скорость
        {
            Logger.UpdateContent(UILogDataType.Computations, "Rotation speed limit reached!", true);
            rotation = Settings.rotationLimit * Mathf.Sign(rotation);
        }
        transform.Rotate(Vector3.forward, rotation);
        previousRotation = rotation;
    }
    void DrawLine(Vector3 start, Vector3 end, float time)
    {
        GameObject line = new GameObject();
        LineRenderer rend = line.AddComponent<LineRenderer>();
        rend.SetPosition(0, start);
        rend.SetPosition(1, end);
        rend.startWidth = 0.05f;
        rend.endWidth = 0.05f;
        rend.startColor = Color.red;
        rend.endColor = Color.green;
        Destroy(line, time);
    }
}