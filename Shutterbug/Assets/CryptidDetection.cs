using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptidDetection : MonoBehaviour
{
    private CryptidProperties m_propertyBlock;

    private float m_distanceToPlayer;
    private float m_distanceModifier;
    private bool m_hasLineOfSight;

    private Transform m_tr;

    private bool m_playerInRange = false;

    public float maxAmbientDetectionRadius = 10f;

    private void Start()
    {
        m_tr = transform;
        m_propertyBlock = gameObject.GetComponent<CryptidProperties>();
    }

    private void Update()
    {
        int _layerMask = 1 << 9;
        Collider[] _hitColliders = Physics.OverlapSphere(m_tr.position, maxAmbientDetectionRadius, _layerMask);

        if(_hitColliders.Length != 0)
        {
            m_playerInRange = true;
        }
        else
        {
            m_playerInRange = false;
        }

        if(m_playerInRange)
        {
            m_distanceToPlayer = Vector3.Distance(m_tr.position, _hitColliders[0].gameObject.transform.position);
            m_distanceModifier = 1 - (m_distanceToPlayer / maxAmbientDetectionRadius);
        }
        else
        {
            m_distanceModifier = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(m_tr.position, maxAmbientDetectionRadius);
    }


}
