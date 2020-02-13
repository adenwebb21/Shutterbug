using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public List<Photograph> currentPhotographs;

    public int photoCap = 5;
    private int currentPhotoCount = 0;

    private static PhotoManager s_instance;

    public static PhotoManager Instance { get => s_instance; set => s_instance = value; }

    void Awake()
    {
        //Singleton Implementation
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(s_instance.gameObject);
            s_instance = this;
        }
    }

    public void TakePhoto()
    {
        if(currentPhotoCount < photoCap)
        {
            currentPhotoCount++;
            UIManager.Instance.UpdatePictureCount(currentPhotoCount);
        }
        else
        {
            Debug.Log("Out of film");
        }
    }

    public void ResetPhotos()
    {
        currentPhotoCount = 0;
        currentPhotographs.Clear();
    }
}
