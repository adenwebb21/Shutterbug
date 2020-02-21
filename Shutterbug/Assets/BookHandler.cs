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
        secondaryCanvas.SetActive(true);
        bestPhotoImage.GetComponent<Image>().sprite = GameManager.Instance.bestPhoto.Image;
        bestPhotoScore.GetComponent<TextMeshProUGUI>().text = "SCORE: " + GameManager.Instance.bestPhotoScore;

        //if (GameManager.Instance.bestPhoto != null)
        //{
            
        //}
        //else
        //{
        //    secondaryCanvas.SetActive(false);
        //}
    }
}
