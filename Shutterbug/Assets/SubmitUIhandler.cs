using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitUIhandler : MonoBehaviour
{
    public GameObject submitButton;
    public GameObject scoringUIElement, scoringPhotoElement;

    public void SubmitButtonOff()
    {
        submitButton.SetActive(false);
    }

    public void SubmitButtonOn()
    {
        submitButton.SetActive(true);
    }

    public void StartScoring()
    {
        scoringUIElement.SetActive(true);
    }
}
