using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisplay : MonoBehaviour
{
    void Start()
    {
        Display.displays[1].Activate();
        Debug.developerConsoleEnabled = true;
    }

}
