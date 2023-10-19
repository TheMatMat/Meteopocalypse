using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineSystem : MonoBehaviour
{

    protected IEnumerator DelayedCoroutine(float delay, System.Action a) {
        yield return new WaitForSeconds(delay);
        a();
    }

    protected IEnumerator DelayedInputCoroutine(Action a)
    {
        yield return new WaitForInput();
        a();
    }

    protected Coroutine RunDelayed(float delay, System.Action a) {
        return StartCoroutine(DelayedCoroutine(delay, a));
    }

    protected Coroutine RunDelayedInput(Action a)
    {
        return StartCoroutine(DelayedInputCoroutine(a));
    }
}
