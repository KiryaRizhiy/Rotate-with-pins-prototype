using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {
        Engine.AddBall();
    }
    void OnTriggerEnter(Collider _c)
    {
        if (_c.tag == "VictoryZone")
            Engine.BallInVictoryZone();
    }
}
