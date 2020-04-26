using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayInteractions : MonoBehaviour
{
    public void NextLevel()
    {
        Engine.LevelDone();
    }
    public void RestartLevel()
    {
        Engine.RestartLevel();
    }
}
