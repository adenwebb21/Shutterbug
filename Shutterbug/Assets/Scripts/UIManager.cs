﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager s_instance;

    public static UIManager Instance { get => s_instance; set => s_instance = value; }
    #endregion

    public Image stealthBar;
    public TextMeshProUGUI pictureCount;
    public TextMeshProUGUI proofCount;
    public GameObject promptText;
    public GameObject entryPrompt;
    public GameObject exitPrompt;

    public TextMeshProUGUI timesFled;

    public GameObject handBook;
    private bool m_handBookOpen = false;

    void Awake()
    {
        //Singleton Implementation
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!m_handBookOpen)
            {
                handBook.GetComponent<Animator>().Play("handbook_in");
                m_handBookOpen = true;
            }
            else
            {
                handBook.GetComponent<Animator>().Play("handbook_out");
                m_handBookOpen = false;
            }
            
        }
    }

    private void Start()
    {
        promptText.SetActive(false);
    }

    public void UpdatePictureCount(int _newPictureCount)
    {
        pictureCount.SetText(_newPictureCount.ToString());
    }

    public void UpdateProofCount(int _newProofCount)
    {
        proofCount.SetText(_newProofCount.ToString());
    }

    public void UpdateStealthFillAmount(float _fillValue)
    {
        stealthBar.fillAmount = _fillValue;
    }

    public void EnablePromptText()
    {
        promptText.SetActive(true);
    }

    public void EntryPrompt()
    {
        entryPrompt.GetComponent<Animator>().Play("entrymessage_fade_in");
    }

    public void ExitPrompt()
    {
        exitPrompt.GetComponent<Animator>().Play("entrymessage_fade_in");
    }

    public void AddTimeFled()
    {
        timesFled.SetText(timesFled.text + "I");
    }


}
