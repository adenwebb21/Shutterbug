using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitUIhandler : MonoBehaviour
{
    public GameObject submitProofButton, submitSightingButton;
    public TextMeshProUGUI boxText;
    public GameObject scoringUIElement;

    public void SubmitButtonOff()
    {
        submitProofButton.SetActive(false);
        submitSightingButton.SetActive(false);
    }

    public void SubmitProofOn()
    {
        submitProofButton.SetActive(true);
    }

    public void SubmitSightingOn()
    {
        submitSightingButton.SetActive(true);
    }

    public void StartScoring()
    {
        scoringUIElement.SetActive(true);
    }

    public void SetBoxText(string _text)
    {
        boxText.SetText("[" + _text + "]");
    }
}
