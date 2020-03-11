using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CryptidDetection : MonoBehaviour
{
    private CryptidProperties m_propertyBlock;
    private GameObject m_player;

    private bool m_hasDetectedPlayer;
    private BilboardRotation m_rotator;
    private LocationSwitcher m_mover;

    private float m_distanceToPlayer;
    private float m_distanceModifier;
    private bool m_hasLineOfSight;

    public Transform tr;
    public Transform eye;

    private bool m_playerInRange = false;

    public float maxAmbientDetectionRadius = 10f;

    private float m_detectionTimer = 0f;
    public float m_detectionThreshold = 1f;

    private void Start()
    {
        m_propertyBlock = gameObject.GetComponent<CryptidProperties>();
        m_mover = gameObject.GetComponent<LocationSwitcher>();
        m_rotator = gameObject.GetComponentInChildren<BilboardRotation>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Checking line of sight
        if (m_rotator.lookingAtPlayer)
        {
            m_hasLineOfSight = !EnvironmentInWay(m_player.transform);
        }
        else
        {
            m_hasLineOfSight = false;
        }

        // Checking if player in range
        int _layerMask = 1 << 9;
        Collider[] _hitColliders = Physics.OverlapSphere(tr.position, maxAmbientDetectionRadius, _layerMask);

        if (_hitColliders.Length != 0)
        {
            m_playerInRange = true;
        }
        else
        {
            m_playerInRange = false;
        }

        if (m_playerInRange)
        {
            m_distanceToPlayer = Vector3.Distance(tr.position, _hitColliders[0].gameObject.transform.position);
            m_distanceModifier = 1 - (m_distanceToPlayer / maxAmbientDetectionRadius);
        }
        else
        {
            m_distanceModifier = 0;
        }

        // Checking if detected
        if (m_propertyBlock.currentState == CryptidProperties.cryptidState.SEARCHING && PlayerDetected(m_hasLineOfSight))
        {
            m_hasDetectedPlayer = true;
        }
        else if (!PlayerDetected(m_hasLineOfSight))
        {
            m_hasDetectedPlayer = false;
        }

        // Handling stealth meter
        if (m_detectionTimer <= m_detectionThreshold && m_hasDetectedPlayer)
        {
            float _scalar = 1 - (m_player.GetComponent<PlayerStealth>().stealthValue / m_propertyBlock.stats.CurrentPerception);
            m_detectionTimer += Time.deltaTime * _scalar;
            m_detectionTimer += m_detectionTimer / 200;
            UIManager.Instance.UpdateStealthFillAmount(m_detectionTimer / m_detectionThreshold);

            if (m_detectionTimer >= m_detectionThreshold)
            {
                m_mover.Leave();
                m_detectionTimer = 0f;
                UIManager.Instance.UpdateStealthFillAmount(0f);
            }
        }
        else if (m_detectionTimer > 0f && !m_hasDetectedPlayer)
        {
            float _scalar = m_propertyBlock.stats.CurrentPerception / m_player.GetComponent<PlayerStealth>().stealthValue;
            m_detectionTimer -= Time.deltaTime * _scalar;
            UIManager.Instance.UpdateStealthFillAmount(m_detectionTimer / m_detectionThreshold);
        }

        if(m_propertyBlock.currentState == CryptidProperties.cryptidState.MOVING)
        {
            m_detectionTimer = 0;
            UIManager.Instance.UpdateStealthFillAmount(m_detectionTimer / m_detectionThreshold);
        }

        //int _layerMask = 1 << 9;
        //Collider[] _hitColliders = Physics.OverlapSphere(m_tr.position, maxAmbientDetectionRadius, _layerMask);

        //if(_hitColliders.Length != 0)
        //{
        //    m_playerInRange = true;
        //}
        //else
        //{
        //    m_playerInRange = false;
        //}

        //if(m_playerInRange)
        //{
        //    if(m_rotator.lookingAtPlayer)
        //    {
        //        m_hasLineOfSight = !EnvironmentInWay(_hitColliders[0].gameObject.transform);
        //    }
        //    else
        //    {
        //        m_hasLineOfSight = false;
        //    }

        //    m_distanceToPlayer = Vector3.Distance(m_tr.position, _hitColliders[0].gameObject.transform.position);
        //    m_distanceModifier = 1 - (m_distanceToPlayer / maxAmbientDetectionRadius);
        //}
        //else
        //{
        //    m_hasLineOfSight = false;
        //    m_distanceModifier = 0;
        //}

        //if(m_propertyBlock.currentState == CryptidProperties.cryptidState.SEARCHING && _hitColliders.Length != 0 && PlayerDetected(_hitColliders[0], m_hasLineOfSight))
        //{
        //    m_hasDetectedPlayer = true;


        //}
        //else if(_hitColliders.Length != 0 && !PlayerDetected(_hitColliders[0], m_hasLineOfSight))
        //{
        //    m_hasDetectedPlayer = false;
        //}

        //// check for if detected, increase detection timer based on scalar value above perception
        //if (m_detectionTimer <= m_detectionThreshold && m_hasDetectedPlayer)
        //{
        //    float _scalar = 1 - (m_player.GetComponent<PlayerStealth>().stealthValue / m_propertyBlock.perception);
        //    m_detectionTimer += Time.deltaTime * _scalar;
        //    UIManager.Instance.UpdateStealthFillAmount(m_detectionTimer / m_detectionThreshold);

        //    if(m_detectionTimer >= m_detectionThreshold)
        //    {
        //        m_mover.Leave();
        //        m_detectionTimer = 0f;
        //        UIManager.Instance.UpdateStealthFillAmount(0f);
        //    }
        //}
        //else if (m_detectionTimer > 0f && !m_hasDetectedPlayer)
        //{
        //    float _scalar = m_propertyBlock.perception / m_player.GetComponent<PlayerStealth>().stealthValue;
        //    m_detectionTimer -= Time.deltaTime * _scalar;
        //    UIManager.Instance.UpdateStealthFillAmount(m_detectionTimer / m_detectionThreshold);
        //}
    }

    /// <summary>
    /// Checking if any environment element falls in the way of the line of sight
    /// </summary>
    /// <param name="_player"> The transform of the player (the target) </param>
    /// <returns> True if there is an obstruction, false if not </returns>
    private bool EnvironmentInWay(Transform _player)
    {
        bool _environmentInWay = false;

        RaycastHit _rayHit;
        Vector3 _directionToPoint = -(eye.position - _player.transform.position).normalized;

        if (Physics.Raycast(eye.position, _directionToPoint, out _rayHit, Vector3.Distance(eye.position, _player.position)) && _rayHit.collider.gameObject.tag == "Environment")
        {
            _environmentInWay = true;
            Debug.DrawRay(eye.position, _directionToPoint * 1000f, Color.magenta);
        }
        else
        {
            _environmentInWay = false;
            Debug.DrawRay(eye.position, _directionToPoint * 1000f, Color.green);
        }

        return _environmentInWay;
    }

    /// <summary>
    /// Determining whether or not the cryptid is aware of the player
    /// </summary>
    /// <param name="_lineOfSight"> The line of sight status </param>
    /// <returns> True if player detected, false if not </returns>
    private bool PlayerDetected(bool _lineOfSight)
    {
        float _currentPlayerStealthValue = m_player.GetComponent<PlayerStealth>().stealthValue;   
        m_propertyBlock.stats.CurrentPerception = m_propertyBlock.stats.DefaultPerception;

        if (!_lineOfSight)
        {
            m_propertyBlock.stats.CurrentPerception = m_propertyBlock.stats.DefaultPerception / 4;
        }

        //_currentPlayerStealthValue = Mathf.Clamp(_currentPlayerStealthValue, 0f, 100f);

        //m_player.GetComponent<PlayerStealth>().stealthValue = _currentPlayerStealthValue;

        if(m_distanceModifier < 0.2f)
        {
            m_propertyBlock.stats.CurrentPerception = m_propertyBlock.stats.CurrentPerception * m_distanceModifier;
        }
        else if(m_distanceModifier > 0.8f)
        {
            m_propertyBlock.stats.CurrentPerception = m_propertyBlock.stats.CurrentPerception * (1 + (m_distanceModifier * 4) - 0.5f);
        }
        else
        {
            m_propertyBlock.stats.CurrentPerception = m_propertyBlock.stats.CurrentPerception * (1 + (m_distanceModifier * 2) - 0.5f);
        }
        
        if (_currentPlayerStealthValue < m_propertyBlock.stats.CurrentPerception)
        {       
            return true;
        }
        else
        {
            return false;
        }   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(tr.position, maxAmbientDetectionRadius);
    }


}
