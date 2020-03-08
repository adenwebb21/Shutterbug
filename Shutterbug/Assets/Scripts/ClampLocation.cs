using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampLocation : MonoBehaviour
{
    private Rigidbody m_rigidBody;
    private Transform m_transform;

    public float forceStrength;

    private void Start()
    {
        m_transform = transform;
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(m_transform.localPosition.x > 5.2f)
        {
            m_rigidBody.AddForce(new Vector3(0, 0, 1f) * forceStrength);
        }
        else if (m_transform.localPosition.x < -7f)
        {
            m_rigidBody.AddForce(new Vector3(0, 0, -1f) * forceStrength);
        }
        else if (m_transform.localPosition.z > 22f)
        {
            m_rigidBody.AddForce(new Vector3(-1f, 0, 0) * forceStrength);
        }
        else if (m_transform.localPosition.z < 1f)
        {
            m_rigidBody.AddForce(new Vector3(1f, 0, 0) * forceStrength);
        }
        else
        {
            m_rigidBody.AddForce(new Vector3(0, 0, 0) * forceStrength);
        }
    }
}
