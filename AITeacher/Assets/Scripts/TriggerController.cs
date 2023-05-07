/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using Inworld;
using Inworld.Model;
using Inworld.Packets;
using Inworld.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TriggerController : MonoBehaviour
{
    [SerializeField] private InworldCharacter Character;
    [SerializeField] InworldCharacterData CharData;
    [SerializeField] TextMeshProUGUI TriggerText;
    public List<string> keywords;
    public string trigger;
    public string triggerResponse;
    public UnityEvent OnTriggerSent;
    public UnityEvent OnTriggerReceived;

    private void Start()
    {
        InworldController.Instance.OnPacketReceived += OnPacketEvents;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SendTrigger("warning");
            //OnTriggerSent.Invoke();
            TriggerText.text = "warning";
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SendTrigger("start_conversation");
            TriggerText.text = "start_conversation";
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SendTrigger("good_job");
            TriggerText.text = "good_job";
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SendTrigger("start_questions");
            TriggerText.text = "start_questions";
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SendTrigger("ask_questions");
            TriggerText.text = "ask_questions";
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SendTrigger("vocabularies");
            TriggerText.text = "vocabularies";
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SendTrigger("give_rewards");
            TriggerText.text = "give_rewards";
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SendTrigger("share_results");
            TriggerText.text = "share_results"; 
        }
    }

    private void OnPacketEvents(InworldPacket packet)
    {
        if (!InworldController.Instance.CurrentCharacter)
            return;
        var charID = InworldController.Instance.CurrentCharacter.ID;
        if (packet.Routing.Target.Id != charID && packet.Routing.Source.Id != charID)
            return;
        switch (packet)
        {
            case CustomEvent customEvent:
                _HandleCustomEvent(customEvent);
                break;
            case TextEvent textEvent:
                _HandleTextEvent(textEvent);
                break;
        }
    }

    private void _HandleTextEvent(TextEvent textEvent)
    {
        if (string.IsNullOrEmpty(textEvent.Text))
            return;

        foreach (var keyword in keywords)
            if (textEvent.Text.ToLower().Contains(keyword.ToLower()))
            {
                Debug.Log("Sending trigger for keyword " + textEvent.Text);
                SendTrigger(trigger);
                OnTriggerSent.Invoke();
                break;
            }
    }

    private void _HandleCustomEvent(CustomEvent customEvent)
    {
        if (customEvent.Name == triggerResponse)
            OnTriggerReceived.Invoke();
    }

    /// <summary>
    ///     Send target character's trigger via InworldPacket.
    /// </summary>
    /// <param name="triggerName">
    ///     The trigger to send. Both formats are acceptable.
    ///     You could send either whole string from CharacterData.trigger, or the trigger's shortName.
    /// </param>
    public void SendTrigger(string triggerName)
    {
        var triggerArray = triggerName.Split("triggers/");
        SendEventToAgent(triggerArray.Length == 2 ? new CustomEvent(triggerArray[1]) : new CustomEvent(triggerName));
    }

    /// <summary>
    ///     Set general events to this Character.
    /// </summary>
    /// <param name="packet">The InworldPacket to send.</param>
    public void SendEventToAgent(InworldPacket packet)
    {
        //need to confirm what id to use for scene triggers
        var ID = Character != null ? Character.ID : InworldController.CurrentScene.name;
        packet.Routing = Routing.FromPlayerToAgent(ID);
        InworldController.Instance.SendEvent(packet);
        Debug.Log(packet);
    }
}
