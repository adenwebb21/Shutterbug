using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton
    private static GameManager s_instance;

    public static GameManager Instance { get => s_instance; set => s_instance = value; }
    #endregion

    public GameObject cryptidPrefab;
    public GameObject currentCryptid;

    void Awake()
    {
        //Singleton Implementation
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            //Destroy(s_instance.gameObject);
            s_instance = this;
        }
    }

    public void AssignCryptid(GameObject _chosenCryptid)
    {
        cryptidPrefab = _chosenCryptid;
    }
}
