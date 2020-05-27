using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookHandler : MonoBehaviour
{
    public GameObject bestPhotoImage;
    public GameObject bestPhotoScore;

    public List<BookEvidence> evidencePhotos;

    public TextMeshProUGUI cryptidDescription;

    //public GameObject hinge;
    //private Animator m_bookAnimator, m_hingeAnimator;

    private void Start()
    {
       // m_bookAnimator = gameObject.GetComponent<Animator>();
        //m_hingeAnimator = hinge.GetComponent<Animator>();

        string _description = "";

        if(GameManager.Instance.currentBuildIndex == 0)
        {
            //m_bookAnimator.Play("slide_menu_book");
            //m_hingeAnimator.Play("open_menu_book");
        }

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

            if(GameManager.Instance.cryptidStats[0].EvidencePhotos.Count > 0)
            {
                for (int i = 0; i < GameManager.Instance.cryptidStats[0].EvidencePhotos.Count; i++)
                {
                    evidencePhotos[i].gameObject.GetComponent<Image>().sprite = GameManager.Instance.cryptidStats[0].EvidencePhotos[i].Image;
                    evidencePhotos[i].gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                }
            }      
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
        bestPhotoScore.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.bestPhotoScore.ToString();
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

    public void OverlayEvidence(string _identifier, Photograph _photo)
    {
        foreach(BookEvidence _evidence in evidencePhotos)
        {
            if(_evidence._evidenceIdentifier == _identifier && _evidence.gameObject.GetComponent<Image>().sprite == null)
            {
                _evidence.gameObject.GetComponent<Image>().sprite = _photo.Image;
                _evidence.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                GameManager.Instance.cryptidStats[0].EvidencePhotos.Add(_photo);
            }
        }
    }
}
