using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitUIhandler : MonoBehaviour
{
    public GameObject submitButton;

    public void SubmitButtonOff()
    {
        submitButton.SetActive(false);
    }

    public void SubmitButtonOn()
    {
        submitButton.SetActive(true);
    }
}
