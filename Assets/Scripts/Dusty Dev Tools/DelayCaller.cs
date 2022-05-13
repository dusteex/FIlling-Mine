using System.Collections;
using UnityEngine;
using System;


public static class DelayCaller
{
    public static void CallWithDelay(float delay , MonoBehaviour mono , Action callback)
    {
        mono.StartCoroutine(Delay(delay,callback));
    }

    private static IEnumerator Delay(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}
