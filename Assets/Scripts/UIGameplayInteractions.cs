using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayInteractions : MonoBehaviour
{
    public void NextLevel()
    {
        Engine.NextLevel();
    }
    public void RestartLevel()
    {
        Engine.RestartLevel();
    }
}
