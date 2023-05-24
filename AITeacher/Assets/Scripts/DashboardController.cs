using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DashboardController : MonoBehaviour
{
    public Button loadReadAloudSceneBtn;
    public Button loadSilentReadSceneBtn;
    public Button loadARSceneBtn;

    void Start()
    {
        loadReadAloudSceneBtn.onClick.AddListener(LoadReadAloudScene);
        loadSilentReadSceneBtn.onClick.AddListener(LoadSilentReadScene);
        loadARSceneBtn.onClick.AddListener(LoadARScene);
    }

    private void LoadReadAloudScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "VirtualTeacher"));
    }

    private void LoadSilentReadScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "SilentRead"));
    }

    private void LoadARScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        //SceneManager.LoadScene("ARMode");
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "ARMode"));
    }
}