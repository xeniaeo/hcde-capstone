using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusModeSimulation : MonoBehaviour
{
    public RectTransform HighlightBoxRT;
    public GameObject[] TextByLine;
    
    // Saving positions and width for moving the highlight box across the paragraph, word by word
    // These two arrays must aligns in terms of which word is being highlighted
    private Vector2[] highlightPos = new Vector2[] {new Vector2(-232, 445), new Vector2(-94, 445), new Vector2(49, 445), new Vector2(251, 445), new Vector2(416, 445), new Vector2(548, 445), new Vector2(648, 445),
                                                    new Vector2(711, 445), new Vector2(-232, 350), new Vector2(-93, 350), new Vector2(24, 350), new Vector2(143, 350), new Vector2(248, 350), new Vector2(381, 350),
                                                    new Vector2(479, 350), new Vector2(547, 350), new Vector2(686, 350), new Vector2(831, 350), new Vector2(-291, 254), new Vector2(-180, 254), new Vector2(-62, 254),
                                                    new Vector2(19, 254), new Vector2(152, 254), new Vector2(321, 254), new Vector2(452, 254), new Vector2(554, 254), new Vector2(656, 254), new Vector2(780, 254),
                                                    new Vector2(-284, 155), new Vector2(-174, 155), new Vector2(-78, 155), new Vector2(-12, 155), new Vector2(100, 155), new Vector2(204, 155), new Vector2(270, 155),
                                                    new Vector2(351, 155), new Vector2(454, 155), new Vector2(564, 155), new Vector2(689, 155), new Vector2(-238, 63), new Vector2(-106, 63), new Vector2(25, 63),
                                                    new Vector2(173, 63), new Vector2(291, 63), new Vector2(394, 63), new Vector2(504, 63), new Vector2(673, 63), new Vector2(-261, -33), new Vector2(-143, -33),
                                                    new Vector2(-25, -33), new Vector2(93, -33), new Vector2(203, -33), new Vector2(284, -33), new Vector2(387, -33), new Vector2(526, -33), new Vector2(651, -33),
                                                    new Vector2(776, -33), new Vector2(-273, -125), new Vector2(-148, -125), new Vector2(-23, -125), new Vector2(95, -125), new Vector2(227, -125), new Vector2(351, -125),
                                                    new Vector2(476, -125), new Vector2(608, -125), new Vector2(747, -125), new Vector2(-234, -221), new Vector2(-65, -221), new Vector2(65, -221), new Vector2(174, -221),
                                                    new Vector2(275, -221), new Vector2(341, -221), new Vector2(407, -221), new Vector2(495, -221), new Vector2(607, -221)};
    private int[] highlightWidth = new int[] {190, 85, 200, 200, 130, 140, 60, 80, 190, 100, 120, 100, 110, 140, 50, 85, 190, 120, 70, 150, 80, 80, 170, 170, 110, 95, 110, 120, 70, 140, 50, 100, 120, 70, 70, 90, 100, 120, 130,
                                            180, 90, 180, 100, 120, 90, 130, 190, 135, 100, 125, 125, 110, 60, 140, 130, 130, 130, 110, 130, 110, 130, 130, 130, 130, 150, 130, 180, 150, 130, 130, 70, 70, 70, 120, 120};
    private int wordIndex = 0;

    private void OnEnable()
    {
        MoveHighlightBox(highlightPos[wordIndex], highlightWidth[wordIndex]);
    }

    private void MoveHighlightBox(Vector2 pos, int width)
    {
        HighlightBoxRT.localPosition = new Vector3(pos.x, pos.y, 0);
        HighlightBoxRT.sizeDelta = new Vector2(width, 80);

        // Highlight texts line by line
        switch(pos.y)
        {
            case(445):
                TextByLine[0].SetActive(true);
                break;
            case(350):
                TextByLine[0].SetActive(false);
                TextByLine[1].SetActive(true);
                break;
            case(254):
                TextByLine[1].SetActive(false);
                TextByLine[2].SetActive(true);
                break;
            case(155):
                TextByLine[2].SetActive(false);
                TextByLine[3].SetActive(true);
                break;
            case(63):
                TextByLine[3].SetActive(false);
                TextByLine[4].SetActive(true);
                break;
            case(-33):
                TextByLine[4].SetActive(false);
                TextByLine[5].SetActive(true);
                break;
            case(-125):
                TextByLine[5].SetActive(false);
                TextByLine[6].SetActive(true);
                break;
            case(-221):
                TextByLine[6].SetActive(false);
                TextByLine[7].SetActive(true);
                break;
            default:
                break;
        }   

        // Call DelayedCallback for the next highlight word
        if (wordIndex < highlightPos.Length - 1)
        {
            wordIndex += 1;
            StartCoroutine(DelayedCallback(0.5f, () => MoveHighlightBox(highlightPos[wordIndex], highlightWidth[wordIndex])));
        }
    }

    private IEnumerator DelayedCallback(float delaySeconds, System.Action callback)
    {
        yield return new WaitForSeconds(delaySeconds);
        callback.Invoke();
    }
}

