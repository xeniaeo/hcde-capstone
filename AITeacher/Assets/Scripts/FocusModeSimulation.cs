using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusModeSimulation : MonoBehaviour
{
    public RectTransform HighlightBoxRT;
    
    // Saving positions and width for moving the highlight box across the paragraph, word by word
    // These two arrays must aligns in terms of which word is being highlighted
    private Vector2[] highlightPos = new Vector2[] {new Vector2(-232, 445), new Vector2(-94, 445), new Vector2(49, 445)};
    private int[] highlightWidth = new int[] {190, 85, 200};
    private int wordIndex = 0;

    private void OnEnable()
    {
        MoveHighlightBox(highlightPos[wordIndex], highlightWidth[wordIndex]);
    }

    private void MoveHighlightBox(Vector2 pos, int width)
    {
        HighlightBoxRT.localPosition = new Vector3(pos.x, pos.y, 0);
        HighlightBoxRT.sizeDelta = new Vector2(width, 80);    

        // Call DelayedCallback for the next word highlight
        if (wordIndex < highlightPos.Length - 1)
        {
            wordIndex += 1;
            StartCoroutine(DelayedCallback(1.0f, () => MoveHighlightBox(highlightPos[wordIndex], highlightWidth[wordIndex])));
        }
    }

    private IEnumerator DelayedCallback(float delaySeconds, System.Action callback)
    {
        yield return new WaitForSeconds(delaySeconds);
        callback.Invoke();
    }
}

