using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivator : MonoBehaviour
{
    public GameObject VictoryPanel;
    public GameObject DefeaturePanel;

    void Start()
    {
        CustomEvents.SessionStateChanged += SessionStateChangeHandler;//Подписка на событие
    }
    public void SessionStateChangeHandler(GameSessionState state)
    {
        if (state == GameSessionState.Won)//Обработка события
        {
            VictoryPanel.SetActive(true);
            Logger.AddContent(UILogDataType.Monetization, "Ads is ready : " + AdCaller.isReady);
        }
        if (state == GameSessionState.Lost)//Обработка события
            DefeaturePanel.SetActive(true);
    }
    void OnDestroy()
    {
        CustomEvents.SessionStateChanged -= SessionStateChangeHandler;//Отписка
    }
}