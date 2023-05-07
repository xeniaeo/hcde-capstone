using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (TriggerController.Instance == null)
        {
            Debug.LogError("TriggerController is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TriggerController.Instance.TriggerActions("warning");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TriggerController.Instance.TriggerActions("start_conversation");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            TriggerController.Instance.TriggerActions("good_job");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TriggerController.Instance.TriggerActions("start_questions");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerController.Instance.TriggerActions("ask_questions");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerController.Instance.TriggerActions("vocabularies");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            TriggerController.Instance.TriggerActions("give_rewards");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            TriggerController.Instance.TriggerActions("share_results"); 
        }
    }
}
