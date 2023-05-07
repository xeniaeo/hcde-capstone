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
using System;
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

    //-------------------------------------------------------------------------------------------- 
    #region Singleton

    // singleton implementation     
    [NonSerialized]
    private static TriggerController instance;
    public static TriggerController Instance
    {
        get { return instance; }
        private set { }
    }

    #endregion

    //--------------------------------------------------------------------------------------------

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;

            // if we change scenes but want this manager to stay around, uncomment this block: 
            // re-parent to root because that's a requirement for DontDestroyOnLoad
            // which is needed to stay persistent when a new scene is loaded
            // gameObject.transform.SetParent(null);
            // DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        InworldController.Instance.OnPacketReceived += OnPacketEvents;
    }

    public void TriggerActions(string trigger)
    {
        SendTrigger(trigger);
        // OnTriggerSent.Invoke();
        TriggerText.text = trigger;
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
