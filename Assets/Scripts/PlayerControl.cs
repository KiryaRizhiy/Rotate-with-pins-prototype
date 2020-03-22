using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Vector2 previousControlPosition;
    //public GameObject maze;
    public static Vector2 displacement;
    public static Vector2 position;

    void Start()
    {
    }
    void Update()
    {
        displacement = Vector2.zero;
        position = Vector2.zero;
        if (Input.GetMouseButton(0) || Input.touchCount == 1)/*Если пользователь взаимодействует с нами мышкой или пальцем*/
        {
            Logger.UpdateContent(UILogDataType.Controls, "Interaction detected");
            if (
                    Application.platform == RuntimePlatform.WindowsEditor
                    || Application.platform == RuntimePlatform.WindowsPlayer
                    || Application.platform == RuntimePlatform.LinuxEditor
                    || Application.platform == RuntimePlatform.LinuxPlayer
                )//Кусок логики для управления с компа
            {
                position = Functions.ToVector2(Input.mousePosition) - Functions.ToVector2(MazeRotator.ScreenPosition);
                if (previousControlPosition != Vector2.zero)//Если не первое касание
                {
                    displacement = Functions.ToVector2(Input.mousePosition) - previousControlPosition;
                    Logger.AddContent(UILogDataType.Controls, "Displacement:" + displacement); 
                }
                previousControlPosition = Functions.ToVector2(Input.mousePosition);
                Logger.AddContent(UILogDataType.Controls, "Position: " + position);
            }
            if (
                    Application.platform == RuntimePlatform.Android
                    || Application.platform == RuntimePlatform.IPhonePlayer
                )//Кусок логики для управления с телефона
            {
                position = Input.touches[0].position - Functions.ToVector2(MazeRotator.ScreenPosition);
                if (previousControlPosition != Vector2.zero)//Если не первое касание
                {
                    displacement = Input.touches[0].position - previousControlPosition;
                    Logger.AddContent(UILogDataType.Controls, "Displacement:" + displacement);
                }
                previousControlPosition = Input.touches[0].position;
                Logger.AddContent(UILogDataType.Controls, "Position: " + position);
            }
        }
        else //Иначе (т.е. пользователь не взаимодействует с нами)
        {
            previousControlPosition = Vector2.zero;
            displacement = Vector2.zero;
            Logger.UpdateContent(UILogDataType.Controls, "No interaction");
        }
    }
}
