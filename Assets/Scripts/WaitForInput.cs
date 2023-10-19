using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForInput : CustomYieldInstruction
{
    public override bool keepWaiting
    {
        get
        {
            return !Input.anyKeyDown;
        }
    }

    public WaitForInput()
    {
        
    }
}
