using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    public static event SessionData SessionStateChanged;
    public static void OnSessionStateChanged(GameSessionState state)
    {
        Debug.Log("Event called: " + state.ToString());
        if (Events.SessionStateChanged != null)
            Events.SessionStateChanged(state);
    }
}
public delegate void SessionData(GameSessionState state);