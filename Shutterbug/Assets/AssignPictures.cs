using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPictures : MonoBehaviour
{
    public GameObject photoPrefab;

    private void Start()
    {
        for (int i = 0; i < GameManager.Instance.currentPhotographs.Count; i++)
        {
            GameObject _tempPhoto = Instantiate(photoPrefab);
            photoPrefab.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.currentPhotographs[i].Image;
        }
    }
}
