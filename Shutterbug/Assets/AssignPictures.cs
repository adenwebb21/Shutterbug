using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPictures : MonoBehaviour
{
    public GameObject[] physicalPhotos;

    private void Start()
    {
        for (int i = 0; i < physicalPhotos.Length; i++)
        {
            if(GameManager.Instance.currentPhotographs[i] != null)
            {
                physicalPhotos[i].GetComponent<SpriteRenderer>().sprite = GameManager.Instance.currentPhotographs[i].Image;
            }
            else
            {
                Destroy(physicalPhotos[i]);
            }
        }

        // probably want to set up so that I instantiate photo images
    }
}
