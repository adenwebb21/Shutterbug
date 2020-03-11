using System.Collections;
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
        foreach(Cryptid _cryptidStat in GameManager.Instance.cryptidStats)
        {
            if(_cryptidStat.KnownFleeCountThisRound)
            {
                BuildText(cryptidDescription, "Seems to flee after 3 sightings\n");
                _cryptidStat.KnownFleeCountThisRound = false;
            }
            else if(_cryptidStat.KnownFleeCount)
            {
                InstantText(cryptidDescription, "Seems to flee after 3 sightings\n");
            }
        }
    }

    private void Update()
    {
        if (GameManager.Instance.bestPhoto != null)
        {
            UpdateDetails();
        }
        else
        {
            secondaryCanvas.SetActive(false);
        }
    }

    public void UpdateDetails()
    {
        secondaryCanvas.SetActive(true);
        bestPhotoImage.GetComponent<Image>().sprite = GameManager.Instance.bestPhoto.Image;
        bestPhotoScore.GetComponent<TextMeshProUGUI>().text = "SCORE: " + GameManager.Instance.bestPhotoScore;
    }

    public void InstantText(TextMeshProUGUI _textElement, string _textToWrite)
    {
        _textElement.text = _textToWrite;
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
            yield return new WaitForSeconds(0.1f);
        }
    }
}
