using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLifter : MonoBehaviour
{
    private bool m_rmbHeld = false;
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            m_rmbHeld = true;
        }
        else
        {
            m_rmbHeld = false;
        }

        if(m_rmbHeld)
        {
            this.GetComponent<Animator>().SetBool("LiftingCamera", true);
        }
        else
        {
            this.GetComponent<Animator>().SetBool("LiftingCamera", false);
        }
    }
}
