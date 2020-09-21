using System;
using System.Collections;
using UnityEngine;

public static class Utils
{
    public static IEnumerator ExecuteAfterTime(float time, Action task)
    {
        yield return new WaitForSeconds(time);

        task();
    }
}
