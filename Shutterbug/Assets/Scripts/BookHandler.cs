﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookHandler : MonoBehaviour
{
    public GameObject bestPhotoImage;
    public GameObject bestPhotoScore;

    public GameObject secondaryCanvas;

    public TextMeshProUGUI cryptidDescription;

    private void Start()
    {
        string _description = "";

        foreach(Cryptid _cryptidStat in GameManager.Instance.cryptidStats)
        {
            if(_cryptidStat.KnownFleeCountThisRound)
            {
                _description += "Seems to flee after 3 sightings\n";
                _cryptidStat.KnownFleeCountThisRound = false;
            }
            else if(_cryptidStat.KnownFleeCount)
            {
                InstantText(cryptidDescription, "Seems to flee after 3 sightings\n");
            }

            if (_cryptidStat.KnownPreferredRegionsThisRound[0])
            {
                _description += "This cryptid seems to enjoy the " + _cryptidStat.PreferredRegions[0].ToString() + " area\n";
                _cryptidStat.KnownPreferredRegionsThisRound[0] = false;
            }
            else if (_cryptidStat.KnownPreferredRegions[0])
            {
                InstantText(cryptidDescription, "This cryptid seems to enjoy the " + _cryptidStat.PreferredRegions[0].ToString() + " area\n");
            }

            if (_cryptidStat.KnownPreferredRegionsThisRound[1])
            {
                _description += "This cryptid seems to enjoy the " + _cryptidStat.PreferredRegions[1].ToString() + " area\n";
                _cryptidStat.KnownPreferredRegionsThisRound[1] = false;
            }
            else if (_cryptidStat.KnownPreferredRegions[1])
            {
                InstantText(cryptidDescription, "This cryptid seems to enjoy the " + _cryptidStat.PreferredRegions[1].ToString() + " area\n");
            }

            BuildText(cryptidDescription, _description);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.bestPhoto != null)
        {
            UpdateDetails();
        }
    }

    public void UpdateDetails()
    {
        bestPhotoImage.GetComponent<Image>().color = new Color(255, 255, 255);
        bestPhotoImage.GetComponent<Image>().sprite = GameManager.Instance.bestPhoto.Image;
        bestPhotoScore.GetComponent<TextMeshProUGUI>().text = "SCORE: " + GameManager.Instance.bestPhotoScore;
    }

    public void InstantText(TextMeshProUGUI _textElement, string _textToWrite)
    {
        _textElement.text += _textToWrite;
    }

    public void BuildText(TextMeshProUGUI _textElement, string _textToWrite)
    {
        StartCoroutine(PopulateText(_textElement, _textToWrite));
    }

    private IEnumerator PopulateText(TextMeshProUGUI _textElement, string _textToWrite)
    {
        for (int i = 0; i < _textToWrite.Length; i++)
        {
            _textElement.text = string.Concat(_textElement.text, _textToWrite[i]);
            yield return new WaitForSeconds(0.08f);
        }
    }
}
