using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions 
{
    public static Vector3 ToVector3(Vector2 input)
    {
        return (Vector3.right * input.x + Vector3.up * input.y);
    }
    public static Vector2 ToVector2(Vector3 input)
    {
        return (Vector3.right * input.x + Vector3.up * input.y);
    }
}
