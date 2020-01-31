using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLifter : MonoBehaviour
{
    public Transform head, holder;

    private bool m_rmbHeld = false;
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            m_rmbHeld = true;
            gameObject.transform.parent = head;
        }
        else
        {
            m_rmbHeld = false;
            gameObject.transform.parent = holder;
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
