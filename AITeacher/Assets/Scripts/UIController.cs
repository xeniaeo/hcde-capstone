using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Image BackgroundUI;
    public List<Sprite> BackgroundSpries = new List<Sprite>();
    public GameObject ChatCanvas;
    public GameObject TeacherAvatar;
    public Button NextBtn;

    private Vector3 canvasPosNormal = new Vector3(0.01f, 0.26f, 1.0f);
    private Vector3 canvasPosUp = new Vector3(0.01f, 0.63f, 1.0f);
    private RectTransform chatCanvasRT;
    private int currentFlowIndex = 0;

    void Start()
    {
        if (TriggerController.Instance == null)
        {
            Debug.LogError("TriggerController is null");
        }
        chatCanvasRT = ChatCanvas.GetComponent<RectTransform>();

        TriggerNextFlow();
    }

    void Update()
    {
        // Quickly trigger specific flow for debugging purposes
        if (Input.GetKeyDown(KeyCode.A))
        {
            BackgroundUI.sprite = BackgroundSpries[0];
            chatCanvasRT.localPosition = canvasPosNormal;
            TriggerController.Instance.TriggerActions("warning");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BackgroundUI.sprite = BackgroundSpries[1];
            chatCanvasRT.localPosition = canvasPosNormal;
            TriggerController.Instance.TriggerActions("start_conversation");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // hide chat
            ChatCanvas.SetActive(false);
            TeacherAvatar.SetActive(false);
            BackgroundUI.sprite = BackgroundSpries[2];
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ChatCanvas.SetActive(true);
            TeacherAvatar.SetActive(true);
            BackgroundUI.sprite = BackgroundSpries[3];
            chatCanvasRT.localPosition = canvasPosNormal;
            TriggerController.Instance.TriggerActions("good_job");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            BackgroundUI.sprite = BackgroundSpries[4];
            chatCanvasRT.localPosition = canvasPosNormal;
            TriggerController.Instance.TriggerActions("start_questions");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            BackgroundUI.sprite = BackgroundSpries[5];
            chatCanvasRT.localPosition = canvasPosNormal;
            TriggerController.Instance.TriggerActions("ask_questions");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            BackgroundUI.sprite = BackgroundSpries[6];
            chatCanvasRT.localPosition = canvasPosNormal;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            BackgroundUI.sprite = BackgroundSpries[7];
            chatCanvasRT.localPosition = canvasPosUp; // move chat up to accommodate UI
            TriggerController.Instance.TriggerActions("vocabularies");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            BackgroundUI.sprite = BackgroundSpries[8];
            chatCanvasRT.localPosition = canvasPosNormal;
            TriggerController.Instance.TriggerActions("give_rewards");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            chatCanvasRT.localPosition = canvasPosNormal;
            TriggerController.Instance.TriggerActions("share_results"); 
        }
    }

    // Enter next flow through button click
    public void TriggerNextFlow()
    {
        Debug.Log(currentFlowIndex);
        switch(currentFlowIndex)
        {
            case(0):
                StartCoroutine(EnterWarning());
                break;
            case(1):
                BackgroundUI.sprite = BackgroundSpries[1];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("start_conversation");
                break;
            case(2):
                // hide chat
                ChatCanvas.SetActive(false);
                TeacherAvatar.SetActive(false);
                BackgroundUI.sprite = BackgroundSpries[2];
                break;
            case(3):
                ChatCanvas.SetActive(true);
                TeacherAvatar.SetActive(true);
                BackgroundUI.sprite = BackgroundSpries[3];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("good_job");
                break;
            case(4):
                BackgroundUI.sprite = BackgroundSpries[4];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("start_questions");
                break;
            case(5):
                BackgroundUI.sprite = BackgroundSpries[5];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("ask_questions");
                break;
            case(6):
                BackgroundUI.sprite = BackgroundSpries[6];
                chatCanvasRT.localPosition = canvasPosNormal;
                break;
            case(7):
                BackgroundUI.sprite = BackgroundSpries[7];
                chatCanvasRT.localPosition = canvasPosUp; // move chat up to accommodate UI
                TriggerController.Instance.TriggerActions("vocabularies");
                break;
            case(8):
                BackgroundUI.sprite = BackgroundSpries[8];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("give_rewards");
                break;
            case(9):
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("share_results");
                break;
            default:
                break;
        }
        if (currentFlowIndex >= 0 && currentFlowIndex < 10)
            currentFlowIndex += 1;
        if (currentFlowIndex == 10)
            currentFlowIndex = 0;
    }

    IEnumerator EnterWarning()
    {
        yield return new WaitForSeconds(1);
        // Trigger the warning interface before the student starts
        BackgroundUI.sprite = BackgroundSpries[0];
        chatCanvasRT.localPosition = canvasPosNormal;
        TriggerController.Instance.TriggerActions("warning");
    }
}
