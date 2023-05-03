using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    public Button teacherEntryBtn;
    public Button studentEntryBtn;
    public Button studentEnterARBtn;

    // Screens for student interface
    public List<GameObject> studentScreens = new List<GameObject>();
    private bool isStudentScreenEnabled = false;
    private int currentStudentScreenIndex = 0;

    // Screens for teacher interface
    public List<GameObject> teacherScreens = new List<GameObject>();
    private bool isTeacherScreenEnabled = false;
    private int currentTeacherScreenIndex = 0;

    void Awake()
    {
        teacherEntryBtn.onClick.AddListener(LaunchTeacherDashboard);
        studentEntryBtn.onClick.AddListener(LaunchStudentDashboard);
        studentEnterARBtn.onClick.AddListener(LoadARScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        DisableAllScreens();
    }

    // Update is called once per frame
    void Update()
    {
        // control student screens
        if (isStudentScreenEnabled)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                studentScreens[currentStudentScreenIndex].SetActive(false);
                if (currentStudentScreenIndex != 0)
                {
                    currentStudentScreenIndex -= 1;
                    studentScreens[currentStudentScreenIndex].SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                studentScreens[currentStudentScreenIndex].SetActive(false);
                if (currentStudentScreenIndex != studentScreens.Count - 1)
                {
                    currentStudentScreenIndex += 1;
                    studentScreens[currentStudentScreenIndex].SetActive(true);
                }
            }
        }
        // control teacher screens
        if (isTeacherScreenEnabled)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                teacherScreens[currentTeacherScreenIndex].SetActive(false);
                if (currentTeacherScreenIndex != 0)
                {
                    currentTeacherScreenIndex -= 1;
                    teacherScreens[currentTeacherScreenIndex].SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                teacherScreens[currentTeacherScreenIndex].SetActive(false);
                if (currentTeacherScreenIndex != teacherScreens.Count - 1)
                {
                    currentTeacherScreenIndex += 1;
                    teacherScreens[currentTeacherScreenIndex].SetActive(true);
                }
            }
        }
    }

    private void DisableAllScreens()
    {
        foreach (GameObject screen in studentScreens)
        {
            screen.SetActive(false);
        }
        isStudentScreenEnabled = false;
        currentStudentScreenIndex = 0;

        foreach (GameObject screen in teacherScreens)
        {
            screen.SetActive(false);
        }
        isTeacherScreenEnabled = false;
        currentTeacherScreenIndex = 0;
    }

    private void LaunchTeacherDashboard()
    {
        DisableAllScreens();
        teacherScreens[0].SetActive(true);
        isTeacherScreenEnabled = true;
        currentTeacherScreenIndex = 0;
    }

    private void LaunchStudentDashboard()
    {
        DisableAllScreens();
        studentScreens[0].SetActive(true);
        isStudentScreenEnabled = true;
        currentStudentScreenIndex = 0;
    }

    private void LoadARScene()
    {
        SceneManager.LoadScene("MissReading");
    }
}
