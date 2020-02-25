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
    public GameObject promptText;

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

    private void Start()
    {
        promptText.SetActive(false);
    }

    public void UpdatePictureCount(int _newPictureCount)
    {
        pictureCount.SetText(_newPictureCount.ToString());
    }

    public void UpdateStealthFillAmount(float _fillValue)
    {
        stealthBar.fillAmount = _fillValue;
    }

    public void EnablePromptText()
    {
        promptText.SetActive(true);
    }


}