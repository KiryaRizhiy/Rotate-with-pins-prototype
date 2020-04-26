using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomEvents
{
    public static event SessionData SessionStateChanged;
    public static void OnSessionStateChanged(GameSessionState state)
    {
        Debug.Log("Event called: " + state.ToString());
        if (CustomEvents.SessionStateChanged != null)
            CustomEvents.SessionStateChanged(state);
    }
}
public delegate void SessionData(GameSessionState state);