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

    private Vector3 canvasPosNormal = new Vector3(0.01f, 0.26f, 1.0f);
    private Vector3 canvasPosUp = new Vector3(0.01f, 0.63f, 1.0f);
    private RectTransform chatCanvasRT;

    void Start()
    {
        if (TriggerController.Instance == null)
        {
            Debug.LogError("TriggerController is null");
        }
        chatCanvasRT = ChatCanvas.GetComponent<RectTransform>();

        StartCoroutine(EnterWarning());
    }

    // Update is called once per frame
    void Update()
    {
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
            TriggerController.Instance.TriggerActions("ask_questions");
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

    IEnumerator EnterWarning()
    {
        yield return new WaitForSeconds(1);
        // Trigger the warning interface before the student starts
        BackgroundUI.sprite = BackgroundSpries[0];
        chatCanvasRT.localPosition = canvasPosNormal;
        TriggerController.Instance.TriggerActions("warning");
    }
}
