using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardRotation : MonoBehaviour
{
    private Transform m_parentTransform;
    private Transform m_transform;

    private GameObject m_goPlayer;

    private void Start()
    {
        m_transform = transform;
        m_parentTransform = m_transform.parent.transform;      
        m_goPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 _vectorToTarget = (m_goPlayer.transform.position - m_parentTransform.position).normalized;

        if (Vector3.Dot(_vectorToTarget, m_parentTransform.forward) > 0)
        {
            //Debug.Log("Player is in front of this game object.");
            m_transform.rotation = Quaternion.LookRotation(m_goPlayer.transform.position - m_parentTransform.position);

        }
        else
        {
            //Debug.Log("Player is not in front of this game object.");
            m_transform.rotation = Quaternion.LookRotation(m_transform.position - m_goPlayer.transform.position);
        }

        //transform.LookAt(m_goPlayer.transform);
    }
}
