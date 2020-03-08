using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPhotos : MonoBehaviour
{
    public LayerMask layerMask;
    public GameEvent readyForProof, readyForSighting;
    public GameEvent invalidHandIn;
    public GameEvent startScoring;
    public GameEvent proofSubmitted;

    public Scoring scoringScript;

    public Transform target;

    private List<GameObject> m_chosenProofs = new List<GameObject>();
    private GameObject m_chosenSighting;

    private Vector3 m_rotationForce = new Vector3(0, 1, 0);
    private Vector3 m_forceVector = new Vector3(1, 0, 0);

    private bool m_proofSubmitted, m_sightingSubmitted = false;

    private void FixedUpdate()
    {
        CheckCollisions();
    }

    void CheckCollisions()
    {
        Collider[] _hitColliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(9.5f, 1.5f, 2f), Quaternion.identity, layerMask);

        if(_hitColliders.Length > 0 && _hitColliders.Length < GameManager.Instance.currentPhotographs.Count && !m_proofSubmitted)
        {
            readyForProof.Raise();

            m_chosenProofs.Clear();

            for (int i = 0; i < _hitColliders.Length; i++)
            {
                m_chosenProofs.Add(_hitColliders[i].gameObject);
            }
        }
        else if(_hitColliders.Length == 1 && !m_sightingSubmitted && m_proofSubmitted)
        {
            readyForSighting.Raise();

            for (int i = 0; i < _hitColliders.Length; i++)
            {
                m_chosenSighting = _hitColliders[i].gameObject;
            }
        }
        else
        {
            invalidHandIn.Raise();
        }
    }

    public void ShootSubmittedProof()
    {
        for (int i = 0; i < m_chosenProofs.Count; i++)
        {
            GameObject _tempPhoto = m_chosenProofs[i];
            Vector3 _dir = target.position - _tempPhoto.transform.position;

            _tempPhoto.GetComponent<Rigidbody>().AddTorque(m_rotationForce * Random.Range(-800f, 800f), ForceMode.Acceleration);
            _tempPhoto.GetComponent<Rigidbody>().AddForce(_dir * 600f, ForceMode.Acceleration);
        }

        m_proofSubmitted = true;
        proofSubmitted.Raise();
    }

    public void ShootSubmittedSighting()
    {
        Vector3 _dir = target.position - m_chosenSighting.transform.position;

        m_chosenSighting.GetComponent<Rigidbody>().AddTorque(m_rotationForce * Random.Range(-800f, 800f), ForceMode.Acceleration);
        m_chosenSighting.GetComponent<Rigidbody>().AddForce(_dir * 600f, ForceMode.Acceleration);

        m_sightingSubmitted = true;

        Invoke("Score", 0.6f);
    }

    private void Score()
    {
        scoringScript.chosenProofs = m_chosenProofs;
        scoringScript.chosenSighting = m_chosenSighting;
        startScoring.Raise();
        scoringScript.ViewSighting();        
    }
}
