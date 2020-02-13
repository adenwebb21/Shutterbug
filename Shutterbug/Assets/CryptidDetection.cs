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

    public Transform m_tr;

    private bool m_playerInRange = false;

    public float maxAmbientDetectionRadius = 10f;

    private float m_detectionTimer = 0f;
    public float m_detectionThreshold = 2f;

    private void Start()
    {
        m_propertyBlock = gameObject.GetComponent<CryptidProperties>();
        m_mover = gameObject.GetComponent<LocationSwitcher>();
        m_rotator = gameObject.GetComponentInChildren<BilboardRotation>();
        m_player = GameObject.FindGameObjectWithTag("Player");
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
            if(m_rotator.lookingAtPlayer)
            {
                m_hasLineOfSight = !EnvironmentInWay(_hitColliders[0].gameObject.transform);
            }
            else
            {
                m_hasLineOfSight = false;
            }
            
            m_distanceToPlayer = Vector3.Distance(m_tr.position, _hitColliders[0].gameObject.transform.position);
            m_distanceModifier = 1 - (m_distanceToPlayer / maxAmbientDetectionRadius);
        }
        else
        {
            m_hasLineOfSight = false;
            m_distanceModifier = 0;
        }

        if(m_propertyBlock.currentState == CryptidProperties.cryptidState.SEARCHING && _hitColliders.Length != 0 && PlayerDetected(_hitColliders[0], m_hasLineOfSight))
        {
            m_hasDetectedPlayer = true;
            
            
        }
        else if(_hitColliders.Length != 0 && !PlayerDetected(_hitColliders[0], m_hasLineOfSight))
        {
            m_hasDetectedPlayer = false;
        }

        // check for if detected, increase detection timer based on scalar value above perception
        if (m_detectionTimer <= m_detectionThreshold && m_hasDetectedPlayer)
        {
            float _scalar = 1 - (m_player.GetComponent<PlayerStealth>().stealthValue / m_propertyBlock.perception);
            m_detectionTimer += Time.deltaTime * _scalar;
            UIManager.Instance.UpdateStealthFillAmount(m_detectionTimer / m_detectionThreshold);

            if(m_detectionTimer >= m_detectionThreshold)
            {
                m_mover.Leave();
                m_detectionTimer = 0f;
                UIManager.Instance.UpdateStealthFillAmount(0f);
            }
        }
        else if (m_detectionTimer > 0f && !m_hasDetectedPlayer)
        {
            float _scalar = m_propertyBlock.perception / m_player.GetComponent<PlayerStealth>().stealthValue;
            m_detectionTimer -= Time.deltaTime * _scalar;
            UIManager.Instance.UpdateStealthFillAmount(m_detectionTimer / m_detectionThreshold);
        }
    }

    private bool EnvironmentInWay(Transform _player)
    {
        bool _environmentInWay = false;

        RaycastHit _rayHit;
        Vector3 _directionToPoint = -(gameObject.transform.position - _player.transform.position).normalized;

        if (Physics.Raycast(transform.position, _directionToPoint, out _rayHit, Vector3.Distance(transform.position, _player.position)) && _rayHit.collider.gameObject.tag == "Environment")
        {
            _environmentInWay = true;
            Debug.DrawRay(gameObject.transform.position, _directionToPoint * 1000f, Color.magenta);
        }
        else
        {
            _environmentInWay = false;
            Debug.DrawRay(gameObject.transform.position, _directionToPoint * 1000f, Color.green);
        }

        return _environmentInWay;
    }

    private bool PlayerDetected(Collider _playerCollider, bool _lineOfSight)
    {
        float _currentPlayerStealthValue = _playerCollider.gameObject.GetComponent<PlayerStealth>().stealthValue;
        _currentPlayerStealthValue = _currentPlayerStealthValue * (1f - m_distanceModifier);

        if(!_lineOfSight)
        {
            _currentPlayerStealthValue = _currentPlayerStealthValue * 3;
        }

        _currentPlayerStealthValue = Mathf.Clamp(_currentPlayerStealthValue, 0f, 100f);

        _playerCollider.gameObject.GetComponent<PlayerStealth>().stealthValue = _currentPlayerStealthValue;

        if (_currentPlayerStealthValue < m_propertyBlock.perception)
        {       
            //Debug.Log("Seen!!!!");
            return true;
        }
        else
        {
            //Debug.Log("Hidden..");
            return false;
        }   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(m_tr.position, maxAmbientDetectionRadius);
    }


}
