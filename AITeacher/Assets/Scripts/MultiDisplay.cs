using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Useful;

public class MultiDisplay : MonoBehaviour
{
    void Start()
    {
        Display.displays[0].Activate(Display.main.systemWidth, Display.main.systemHeight, 60);

        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate(3840, 1080, 60);
        }
    }
}
