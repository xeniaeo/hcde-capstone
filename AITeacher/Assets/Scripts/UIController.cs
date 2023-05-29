using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public Image BackgroundUI;
    public List<Sprite> BackgroundSpries = new List<Sprite>();
    public GameObject ChatCanvas;
    public GameObject TeacherAvatar;
    public Image TipBoxUI;
    public List<Sprite> TipBoxSprites = new List<Sprite>();
    public Image HeaderUI;
    public List<Sprite> HeaderSprites = new List<Sprite>();
    public Button LoadARBtn;
    public GameObject FocusModeParagraph;

    private Vector3 canvasPosNormal = new Vector3(0.01f, 0.12f, 1.0f);
    private Vector3 canvasPosUp = new Vector3(0.01f, 0.63f, 1.0f);
    private RectTransform chatCanvasRT;
    private int currentFlowIndex = 0;
    private Color transparentColor = new Color(0f, 0f, 0f, 0f);

    // adding custom bubbles
    // public RectTransform ChatContent;
    [Header("Custom bubbles")]
    public GameObject CustomBubbleCanvas;
    public RectTransform CustomBubbleContent;
    public GameObject ChatBubble_goodJob;
    public GameObject ChatBubble_whale;
    public GameObject ChatBubble_vocabulary;
    public GameObject ChatBubble_badge;

    void Start()
    {
        // TriggerController sends the trigger strings to the AI avatar. It can not be null.
        if (TriggerController.Instance == null)
        {
            Debug.LogError("TriggerController can not be null");
        }

        chatCanvasRT = ChatCanvas.GetComponent<RectTransform>();
        LoadARBtn.onClick.AddListener(LoadARMode);

        TriggerNextFlow();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TriggerNextFlow();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TriggerController.Instance.TriggerActions("stop_talking"); // trying to force stop the conversation but this doesn't seem to work
        }

        // // [DEBUG-ONLY] Quickly trigger specific flow for debugging purposes. Only enable this for debugging and not for building.
        // if (Input.GetKeyDown(KeyCode.A))
        // {
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
                TriggerFlow(true, true, BackgroundSpries[1], TipBoxSprites[1], Color.white, HeaderSprites[0], canvasPosNormal, "start_conversation");
                break;
            case(2):
                // hide chat
                TriggerFlow(false, false, BackgroundSpries[2], TipBoxSprites[2], transparentColor, null, canvasPosNormal, "");
                StartCoroutine(DelayedCallback(3.0f, () => { FocusModeParagraph.SetActive(true); }));
                break;
            case(3):
                FocusModeParagraph.SetActive(false);
                TriggerFlow(true, true, BackgroundSpries[3], TipBoxSprites[3], transparentColor, HeaderSprites[1], canvasPosNormal, "good_job");
                StartCoroutine(DelayedCallback(12.0f, () => InstantiateCustomChatBubble(ChatBubble_goodJob)));
                break;
            case(4):
                TriggerFlow(true, true, BackgroundSpries[7], TipBoxSprites[7], Color.white, HeaderSprites[1], canvasPosNormal, "vocabularies");
                StartCoroutine(DelayedCallback(12.0f, () => InstantiateCustomChatBubble(ChatBubble_vocabulary)));
                break;
            case(5):
                TriggerFlow(true, true, BackgroundSpries[4], TipBoxSprites[4], Color.white, HeaderSprites[1], canvasPosNormal, "start_questions");
                break;
            case(6):
                TriggerFlow(true, true, BackgroundSpries[5], TipBoxSprites[5], Color.white, HeaderSprites[1], canvasPosNormal, "ask_questions");
                break;
            case(7):
                TriggerFlow(true, true, BackgroundSpries[6], TipBoxSprites[6], Color.white, HeaderSprites[1], canvasPosNormal, "");
                StartCoroutine(DelayedCallback(2.0f, () => InstantiateCustomChatBubble(ChatBubble_whale)));
                break;
            case(8):
                TriggerFlow(true, true, BackgroundSpries[8], TipBoxSprites[8], transparentColor, HeaderSprites[2], canvasPosNormal, "give_rewards");
                StartCoroutine(DelayedCallback(12.0f, () => InstantiateCustomChatBubble(ChatBubble_badge)));
                break;
            case(9):
                // add a case here before gling back to main dashboard to allow previous case to finish
                break;
            default:
                break;
        }
        if (currentFlowIndex >= 0 && currentFlowIndex < 10)
            currentFlowIndex += 1;
        if (currentFlowIndex == 10)
            LoadDashboard();
    }

    IEnumerator EnterWarning()
    {
        yield return new WaitForSeconds(2);
        // Trigger the warning interface before the student starts
        TriggerFlow(true, true, BackgroundSpries[0], TipBoxSprites[0], Color.white, HeaderSprites[0], canvasPosNormal, "warning");
    }

    private void TriggerFlow(bool isChatActive, bool isAvatarActive, Sprite backgroundSprite, Sprite tipBoxSprite, Color tipBoxColor, Sprite headerSprite, Vector3 canvasPos, string triggerString)
    {
        ChatCanvas.SetActive(isChatActive);
        TeacherAvatar.SetActive(isAvatarActive);
        BackgroundUI.sprite = backgroundSprite;
        TipBoxUI.color = tipBoxColor;
        TipBoxUI.sprite = tipBoxSprite;
        HeaderUI.color = Color.white;
        HeaderUI.sprite = headerSprite;
        if (headerSprite == null)
            HeaderUI.color = transparentColor;
        chatCanvasRT.localPosition = canvasPos;

        // Add animation for tip box
        TipBoxUI.transform.DOMove(new Vector3(TipBoxUI.transform.position.x, TipBoxUI.transform.position.y + 15f, TipBoxUI.transform.position.z), 0.6f).SetEase(Ease.OutQuad).OnComplete(
            () => {
                TipBoxUI.transform.DOMove(new Vector3(TipBoxUI.transform.position.x, TipBoxUI.transform.position.y - 15f, TipBoxUI.transform.position.z), 1f).SetEase(Ease.OutQuad);
            }
        );

        if (triggerString != "")
        {
            HideCustomBubbleCanvas();
            TriggerController.Instance.TriggerActions(triggerString);
        }
    }

    private void InstantiateCustomChatBubble(GameObject customBubble)
    {
        // // todo: need to fix chat content height to insert a custom bubble into the chats, which doesn't seem feasible / too difficult to implement now.
        // GameObject clone = Instantiate (customBubble);
        // clone.transform.SetParent(ChatContent, false);
        // ChatContent.sizeDelta = new Vector2(ChatContent.sizeDelta.x, ChatContent.sizeDelta.y + clone.GetComponent<RectTransform>().sizeDelta.y);

        // Alternative: hide the chat content and only show the custom chat bubble on another panel. Add custom bubble to the content
        ChatCanvas.SetActive(false);
        CustomBubbleCanvas.SetActive(true);
        GameObject clone = Instantiate (customBubble);
        clone.transform.SetParent(CustomBubbleContent, false);
        CustomBubbleContent.sizeDelta = new Vector2(CustomBubbleContent.sizeDelta.x, CustomBubbleContent.sizeDelta.y + clone.GetComponent<RectTransform>().sizeDelta.y);

        RectTransform cloneRT = clone.gameObject.GetComponent<RectTransform>();
        cloneRT.DOSizeDelta(new Vector2(cloneRT.sizeDelta.x + 0.0f, cloneRT.sizeDelta.y + 50.0f), 0.5f).SetEase(Ease.OutQuad).OnComplete(
            () => {
                cloneRT.DOSizeDelta(new Vector2(cloneRT.sizeDelta.x - 0.0f, cloneRT.sizeDelta.y - 50.0f), 0.8f).SetEase(Ease.OutQuad);
            }
        );
    }

    private void HideCustomBubbleCanvas()
    {
        CustomBubbleCanvas.SetActive(false);
    }

    private IEnumerator DelayedCallback(float delaySeconds, System.Action callback)
    {
        yield return new WaitForSeconds(delaySeconds);
        callback.Invoke();
    }

    private void LoadDashboard()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Dashboard");
    }

    private void LoadARMode()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("ARMode");
    }

    private void DestroyChildren(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            if (i == childCount - 1)
            {
                Transform child = parent.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}
