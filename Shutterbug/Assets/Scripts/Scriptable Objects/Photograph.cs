using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu]
public class Photograph : ScriptableObject
{
    [SerializeField]
    private Sprite image;

    private bool isCryptidInPicture = false;

    public Sprite Image
    {
        get
        {
            return image;
        }
        set
        {
            this.image = value;
        }
    }

    public bool CryptidInPicture
    {
        get
        {
            return isCryptidInPicture;
        }
        set
        {
            this.isCryptidInPicture = value;
        }
    }
}
