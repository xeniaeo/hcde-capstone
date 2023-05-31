using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Reqs;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Requests _requests = new Requests();
    private Text inputText;
    private Text chatGPTText;

    public UnityEvent<string> onButtonClick = new UnityEvent<string>();

    private void Start()
    {
        inputText = FindChildRecursive(this.transform, "InputText").gameObject.GetComponent<Text>();
        chatGPTText = FindChildRecursive(this.transform, "ChatGPTText").gameObject.GetComponent<Text>();

        UnityEngine.UI.Button requestOne = FindChildRecursive(this.transform, "SendBtn").GetComponent<UnityEngine.UI.Button>();
        requestOne.onClick.AddListener(() => SendMessage());

        SendMessage(); // Trigger initial message
    }

    // Initial prompt:
    // Act like you are an elementary school teacher who is passionate about inspiring a love of reading for her students.
    // Start with a greeting to a student named Jamie. Jamie is a 10-year-old student who has learning disabilities, so he has trouble focusing.
    // Try to help him with that. Keep your response short and simple to understand.
    void SendMessage()
    {
        string message = inputText.text;
        if (message == null || message == "")
        {
            message = "Hello";
        }
        onButtonClick.Invoke(message);
        inputText.text = "";
        message = "";
    }

    // Send a pre-defined prompt by clicking a button
    public void SendMessageByButton(string message)
    {
        string prompt = "";
        switch(message)
        {
            case("tell me more"):
                prompt = "Can you tell me more?";
                break;
            case("recommend"):
                prompt = "Can you give me a recommendation?";
                break;
            default:
                break;
        }
        onButtonClick.Invoke(prompt);
    }

    public void SetChatGPTText(string msg)
    {
        chatGPTText.text = msg;
        Debug.Log(msg);
    }

    public static Transform FindChildRecursive(Transform parent, string name)
    {
        if (parent.name.Equals(name))
            return parent;
        foreach (Transform child in parent)
        {
            if (child.name.Equals(name))
                return child;

            var result = FindChildRecursive(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
}
