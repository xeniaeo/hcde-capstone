using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ARController : MonoBehaviour
{
    public Button loadMainInterfaceBtn;

    void Start()
    {
        loadMainInterfaceBtn.onClick.AddListener(LoadMainInterfaceScene);
    }

    private void LoadMainInterfaceScene()
    {
        SceneManager.LoadScene("MainInterface");
    }
}
