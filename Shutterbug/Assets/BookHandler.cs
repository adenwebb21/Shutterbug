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

    private void Start()
    {
        UpdateDetails();
    }

    public void UpdateDetails()
    {
        if(GameManager.Instance.bestPhoto != null)
        {
            secondaryCanvas.SetActive(true);
            bestPhotoImage.GetComponent<Image>().sprite = GameManager.Instance.bestPhoto.Image;
            bestPhotoScore.GetComponent<TextMeshProUGUI>().text = "SCORE: " + GameManager.Instance.bestPhotoScore;
        }
        else
        {
            secondaryCanvas.SetActive(false);
        }
        
    }
}
