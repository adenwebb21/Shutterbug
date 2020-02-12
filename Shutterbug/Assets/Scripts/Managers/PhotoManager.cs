using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public List<Photograph> currentPhotographs;

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
}
