using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivator : MonoBehaviour
{
    public GameObject VictoryPanel;
    public GameObject DefeaturePanel;

    void Start()
    {
        Events.SessionStateChanged += SessionStateChangeHandler;//Подписка на событие
    }
    public void SessionStateChangeHandler(GameSessionState state)
    {
        if (state == GameSessionState.Won)//Обработка события
            VictoryPanel.SetActive(true);
        if (state == GameSessionState.Lost)//Обработка события
            DefeaturePanel.SetActive(true);
    }
    void OnDestroy()
    {
        Events.SessionStateChanged -= SessionStateChangeHandler;//Отписка
    }
}