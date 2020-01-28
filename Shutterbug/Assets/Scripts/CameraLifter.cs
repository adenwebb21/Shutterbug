using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLifter : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.GetComponent<Animator>().SetTrigger("LiftCamera");
        }

        if(Input.GetMouseButtonDown(1))
        {
            this.GetComponent<Animator>().SetTrigger("DropCamera");
        }
    }
}
