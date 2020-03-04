using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoDisabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PhotoObject")
        {
            other.gameObject.SetActive(false);
        }
    }
}
