using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilentReadUIController : MonoBehaviour
{
    public Image BackgroundUI;
    public List<Sprite> BackgroundSpries = new List<Sprite>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BackgroundUI.sprite = BackgroundSpries[0];
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BackgroundUI.sprite = BackgroundSpries[1];
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            BackgroundUI.sprite = BackgroundSpries[2];
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            BackgroundUI.sprite = BackgroundSpries[3];
        }
    }
}
