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
    public List<Sprite> TipBoxSprites = new List<Sprite>();

    private Vector3 canvasPosNormal = new Vector3(0.01f, 0.034f, 1.0f);
    private Vector3 canvasPosUp = new Vector3(0.01f, 0.63f, 1.0f);
    private RectTransform chatCanvasRT;
    private int currentFlowIndex = 0;
    private Color transparentColor = new Color(0f, 0f, 0f, 0f);

    // adding items to chat bubble scroll views
    public RectTransform ChatContent;
    public GameObject ChatBubble_goodJob;
    public GameObject ChatBubble_whale;

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
        //     TriggerFlow(true, true, BackgroundSpries[0], TipBoxSprites[0], Color.white, canvasPosNormal, "warning");
        // }

        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     TriggerFlow(true, true, BackgroundSpries[1], TipBoxSprites[1], Color.white, canvasPosNormal, "start_conversation");
        // }

        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     TriggerFlow(false, false, BackgroundSpries[2], TipBoxSprites[2], transparentColor, canvasPosNormal, "");
        // }

        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     TriggerFlow(true, true, BackgroundSpries[3], TipBoxSprites[3], transparentColor, canvasPosNormal, "good_job");
        // }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     TriggerFlow(true, true, BackgroundSpries[4], TipBoxSprites[4], Color.white, canvasPosNormal, "start_questions");
        // }

        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     TriggerFlow(true, true, BackgroundSpries[5], TipBoxSprites[5], Color.white, canvasPosNormal, "ask_questions");
        // }

        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     TriggerFlow(true, true, BackgroundSpries[6], TipBoxSprites[6], Color.white, canvasPosNormal, "");
        // }

        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     TriggerFlow(true, true, BackgroundSpries[7], TipBoxSprites[7], Color.white, canvasPosNormal, "vocabularies");
        // }

        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     TriggerFlow(true, true, BackgroundSpries[8], TipBoxSprites[8], transparentColor, canvasPosNormal, "give_rewards");
        // }
    }

    // Enter next flow through pressing the right arrow key
    public void TriggerNextFlow()
    {
        switch(currentFlowIndex)
        {
            case(0):
                StartCoroutine(EnterWarning());
                break;
            case(1):
                TriggerFlow(true, true, BackgroundSpries[1], TipBoxSprites[1], Color.white, canvasPosNormal, "start_conversation");
                break;
            case(2):
                // hide chat
                TriggerFlow(false, false, BackgroundSpries[2], TipBoxSprites[2], transparentColor, canvasPosNormal, "");
                break;
            case(3):
                TriggerFlow(true, true, BackgroundSpries[3], TipBoxSprites[3], transparentColor, canvasPosNormal, "good_job");
                // InstantiateCustomChatBubble(ChatBubble_goodJob); // todo: can I wait for the trigger to complete? (callback or wait for a bit with coroutine)
                break;
            case(4):
                TriggerFlow(true, true, BackgroundSpries[4], TipBoxSprites[4], Color.white, canvasPosNormal, "start_questions");
                break;
            case(5):
                TriggerFlow(true, true, BackgroundSpries[5], TipBoxSprites[5], Color.white, canvasPosNormal, "ask_questions");
                break;
            case(6):
                TriggerFlow(true, true, BackgroundSpries[6], TipBoxSprites[6], Color.white, canvasPosNormal, "");
                // InstantiateCustomChatBubble(ChatBubble_whale);
                break;
            case(7):
                TriggerFlow(true, true, BackgroundSpries[7], TipBoxSprites[7], Color.white, canvasPosNormal, "vocabularies");
                break;
            case(8):
                TriggerFlow(true, true, BackgroundSpries[8], TipBoxSprites[8], transparentColor, canvasPosNormal, "give_rewards");
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
        TriggerFlow(true, true, BackgroundSpries[0], TipBoxSprites[0], Color.white, canvasPosNormal, "warning");
    }

    private void TriggerFlow(bool isChatActive, bool isAvatarActive, Sprite backgroundSprite, Sprite tipBoxSprite, Color tipBoxColor, Vector3 canvasPos, string triggerString)
    {
        ChatCanvas.SetActive(isChatActive);
        TeacherAvatar.SetActive(isAvatarActive);
        BackgroundUI.sprite = backgroundSprite;
        TipBoxUI.color = tipBoxColor;
        TipBoxUI.sprite = tipBoxSprite;
        chatCanvasRT.localPosition = canvasPos;

        if (triggerString != "")
            TriggerController.Instance.TriggerActions(triggerString);
    }

    private void InstantiateCustomChatBubble(GameObject customBubble)
    {
        GameObject clone = Instantiate (customBubble);
        clone.transform.SetParent(ChatContent, false);
        ChatContent.sizeDelta = new Vector2(ChatContent.sizeDelta.x, ChatContent.sizeDelta.y + clone.GetComponent<RectTransform>().sizeDelta.y); // chatBubble height 90 + 20 = 110
        // todo: fix content height
    }
}
