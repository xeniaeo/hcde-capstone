using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController_AR : MonoBehaviour
{
    public Button LoadDashboardBtn;

    void Start()
    {
        LoadDashboardBtn.onClick.AddListener(LoadDashboard);

        if (TriggerController.Instance == null)
        {
            Debug.LogError("TriggerController can not be null");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TriggerController.Instance.TriggerActions("just_chat");
        }
    }

    private void LoadDashboard()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Dashboard");
    }
}
