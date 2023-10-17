using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDisplay : MonoBehaviour
{
    void Start()
    {
        if (Display.displays.Length >= 2)
        {
            Display.displays[1].Activate();    
        }
    }

}
