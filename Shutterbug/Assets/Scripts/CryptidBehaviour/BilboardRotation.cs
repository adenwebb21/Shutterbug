using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardRotation : MonoBehaviour
{
    public Sprite front, back;

    private Transform m_parentTransform;
    private Transform m_transform;

    private SpriteRenderer m_childSpriteRenderer;

    private GameObject m_goPlayer;

    public bool lookingAtPlayer = false;

    private void Start()
    {
        m_transform = transform;
        m_parentTransform = m_transform.parent.transform;      
        m_goPlayer = GameObject.FindGameObjectWithTag("Player");

        m_childSpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        Vector3 _vectorToTarget = (m_goPlayer.transform.position - m_parentTransform.position).normalized;

        if (Vector3.Dot(_vectorToTarget, m_parentTransform.forward) > 0)
        {
            //Debug.Log("Player is in front of this game object.");
            lookingAtPlayer = true;
            m_transform.rotation = Quaternion.LookRotation(m_goPlayer.transform.position - m_parentTransform.position);
            m_transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
            m_childSpriteRenderer.sprite = front;
        }
        else
        {
            //Debug.Log("Player is not in front of this game object.");
            lookingAtPlayer = false;
            m_transform.rotation = Quaternion.LookRotation(m_transform.position - m_goPlayer.transform.position);
            m_transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
            m_childSpriteRenderer.sprite = back;
        }
    }
}
