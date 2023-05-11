using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Button loadReadAloudSceneBtn;
    public Button loadSilentReadSceneBtn;

    void Start()
    {
        loadReadAloudSceneBtn.onClick.AddListener(LoadReadAloudScene);
        loadSilentReadSceneBtn.onClick.AddListener(LoadSilentReadScene);
    }

    private void LoadReadAloudScene()
    {
        // SceneManager.LoadScene("VirtualTeacher");
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "VirtualTeacher"));
    }

    private void LoadSilentReadScene()
    {
        // SceneManager.LoadScene("SilentRead");
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "SilentRead"));
    }
}