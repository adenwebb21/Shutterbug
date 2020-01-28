using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardRotation : MonoBehaviour
{
    private Transform m_transform;

    private GameObject m_goPlayer;

    private void Start()
    {
        m_transform = transform;
        m_goPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.LookAt(m_goPlayer.transform);
    }
}
