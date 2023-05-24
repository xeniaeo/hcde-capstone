using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController_SilentRead : MonoBehaviour
{
    public Image BackgroundUI;
    public List<Sprite> BackgroundSpries = new List<Sprite>();
    private int flowIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TriggerFlow(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TriggerFlow(-1);
        }
    }

    private void TriggerFlow(int prevOrNext)
    {
        flowIndex += prevOrNext;
        if (flowIndex < 0)
        {
            flowIndex = 0;
            BackgroundUI.sprite = BackgroundSpries[flowIndex];
        }
        else if (flowIndex <= BackgroundSpries.Count - 1)
        {
            BackgroundUI.sprite = BackgroundSpries[flowIndex];
        }
        else if (flowIndex > BackgroundSpries.Count - 1)
        {
            LoadDashboard();
        }
    }

    private void LoadDashboard()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Dashboard");
    }
}
