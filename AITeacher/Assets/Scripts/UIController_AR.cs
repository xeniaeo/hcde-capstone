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
    }

    private void LoadDashboard()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Dashboard");
    }
}
