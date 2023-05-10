using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public GameObject Avatar;
    public GameObject AvatarModel;
    public GameObject ChatScrollView;
    public GameObject InputPanel;

    public bool isHavingConversation = false;

    #region Singleton

    // singleton implementation     
    private static AvatarController instance;
    public static AvatarController Instance
    {
        get { return instance; }
        private set { }
    }

    #endregion

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // this is expected when the game scene is loaded by the load scene
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

    public void StartConversation()
    {
        // Avatar.SetActive(true);
        AvatarModel.SetActive(true); // to-do: need to fix this
        ChatScrollView.SetActive(true);
        InputPanel.SetActive(true);

        isHavingConversation = true;
    }

    public void EndConversation()
    {
        // Avatar.SetActive(false);
        AvatarModel.SetActive(false);
        ChatScrollView.SetActive(false);
        InputPanel.SetActive(false);

        isHavingConversation = false;
    }
}
