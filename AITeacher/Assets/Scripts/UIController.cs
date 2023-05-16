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
    public Image TipBoxUI;
    public List<Sprite> TipBoxSpries = new List<Sprite>();

    private Vector3 canvasPosNormal = new Vector3(0.01f, 0.034f, 1.0f);
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TriggerNextFlow();
        }

        // // [DEBUG-ONLY] Quickly trigger specific flow for debugging purposes. Only enable this for debugging and not for building.
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     BackgroundUI.sprite = BackgroundSpries[0];
        //     TipBoxUI.sprite = TipBoxSpries[0];
        //     chatCanvasRT.localPosition = canvasPosNormal;
        //     TriggerController.Instance.TriggerActions("warning");
        // }

        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     BackgroundUI.sprite = BackgroundSpries[1];
        //     TipBoxUI.sprite = TipBoxSpries[1];
        //     chatCanvasRT.localPosition = canvasPosNormal;
        //     TriggerController.Instance.TriggerActions("start_conversation");
        // }

        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     // hide chat
        //     ChatCanvas.SetActive(false);
        //     TeacherAvatar.SetActive(false);
        //     BackgroundUI.sprite = BackgroundSpries[2];
        //     TipBoxUI.sprite = TipBoxSpries[2];
        // }

        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     ChatCanvas.SetActive(true);
        //     TeacherAvatar.SetActive(true);
        //     BackgroundUI.sprite = BackgroundSpries[3];
        //     TipBoxUI.sprite = TipBoxSpries[3];
        //     chatCanvasRT.localPosition = canvasPosNormal;
        //     TriggerController.Instance.TriggerActions("good_job");
        // }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     BackgroundUI.sprite = BackgroundSpries[4];
        //     TipBoxUI.sprite = TipBoxSpries[4];
        //     chatCanvasRT.localPosition = canvasPosNormal;
        //     TriggerController.Instance.TriggerActions("start_questions");
        // }

        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     BackgroundUI.sprite = BackgroundSpries[5];
        //     TipBoxUI.sprite = TipBoxSpries[5];
        //     chatCanvasRT.localPosition = canvasPosNormal;
        //     TriggerController.Instance.TriggerActions("ask_questions");
        // }

        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     BackgroundUI.sprite = BackgroundSpries[6];
        //     TipBoxUI.sprite = TipBoxSpries[6];
        //     chatCanvasRT.localPosition = canvasPosNormal;
        // }

        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     BackgroundUI.sprite = BackgroundSpries[7];
        //     TipBoxUI.sprite = TipBoxSpries[7];
        //     chatCanvasRT.localPosition = canvasPosNormal;
        //     TriggerController.Instance.TriggerActions("vocabularies");
        // }

        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     BackgroundUI.sprite = BackgroundSpries[8];
        //     TipBoxUI.sprite = TipBoxSpries[8];
        //     chatCanvasRT.localPosition = canvasPosNormal;
        //     TriggerController.Instance.TriggerActions("give_rewards");
        // }
    }

    // Enter next flow through pressing the right arrow key
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
                TipBoxUI.sprite = TipBoxSpries[1];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("start_conversation");
                break;
            case(2):
                // hide chat
                ChatCanvas.SetActive(false);
                TeacherAvatar.SetActive(false);
                BackgroundUI.sprite = BackgroundSpries[2];
                TipBoxUI.sprite = TipBoxSpries[2];
                break;
            case(3):
                ChatCanvas.SetActive(true);
                TeacherAvatar.SetActive(true);
                BackgroundUI.sprite = BackgroundSpries[3];
                TipBoxUI.sprite = TipBoxSpries[3];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("good_job");
                break;
            case(4):
                BackgroundUI.sprite = BackgroundSpries[4];
                TipBoxUI.sprite = TipBoxSpries[4];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("start_questions");
                break;
            case(5):
                BackgroundUI.sprite = BackgroundSpries[5];
                TipBoxUI.sprite = TipBoxSpries[5];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("ask_questions");
                break;
            case(6):
                BackgroundUI.sprite = BackgroundSpries[6];
                TipBoxUI.sprite = TipBoxSpries[6];
                chatCanvasRT.localPosition = canvasPosNormal;
                break;
            case(7):
                BackgroundUI.sprite = BackgroundSpries[7];
                TipBoxUI.sprite = TipBoxSpries[7];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("vocabularies");
                break;
            case(8):
                BackgroundUI.sprite = BackgroundSpries[8];
                TipBoxUI.sprite = TipBoxSpries[8];
                chatCanvasRT.localPosition = canvasPosNormal;
                TriggerController.Instance.TriggerActions("give_rewards");
                break;
            default:
                break;
        }
        if (currentFlowIndex >= 0 && currentFlowIndex < 9)
            currentFlowIndex += 1;
        if (currentFlowIndex == 9)
            currentFlowIndex = 0;
    }

    IEnumerator EnterWarning()
    {
        yield return new WaitForSeconds(1);
        // Trigger the warning interface before the student starts
        BackgroundUI.sprite = BackgroundSpries[0];
        TipBoxUI.sprite = TipBoxSpries[0];
        chatCanvasRT.localPosition = canvasPosNormal;
        TriggerController.Instance.TriggerActions("warning");
    }
}
