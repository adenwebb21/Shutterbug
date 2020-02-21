using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPhotos : MonoBehaviour
{
    public LayerMask layerMask;
    public GameEvent readyForHandIn;
    public GameEvent invalidHandIn;
    public GameEvent startScoring;

    public Scoring scoringScript;

    public Transform target;

    private GameObject[] m_chosenPhotographs = new GameObject[3];

    private Vector3 m_rotationForce = new Vector3(0, 1, 0);
    private Vector3 m_forceVector = new Vector3(1, 0, 0);

    private void FixedUpdate()
    {
        CheckCollisions();
    }

    void CheckCollisions()
    {
        Collider[] _hitColliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(9.5f, 1.5f, 2f), Quaternion.identity, layerMask);

        Debug.Log(_hitColliders.Length);

        if(_hitColliders.Length == 3)
        {
            readyForHandIn.Raise();

            for (int i = 0; i < _hitColliders.Length; i++)
            {
                m_chosenPhotographs[i] = _hitColliders[i].gameObject;
            }
        }
        else
        {
            invalidHandIn.Raise();
        }
    }

    public void ShootSubmittedPhotos()
    {
        for (int i = 0; i < m_chosenPhotographs.Length; i++)
        {
            GameObject _tempPhoto = m_chosenPhotographs[i];
            Vector3 _dir = target.position - _tempPhoto.transform.position;

            _tempPhoto.GetComponent<Rigidbody>().AddTorque(m_rotationForce * Random.Range(-200f, 200f), ForceMode.Acceleration);
            _tempPhoto.GetComponent<Rigidbody>().AddForce(_dir * 600f, ForceMode.Acceleration);
        }

        Invoke("DisablePhotos", 0.6f);
    }

    private void DisablePhotos()
    {
        foreach(GameObject _photo in m_chosenPhotographs)
        {
            _photo.SetActive(false);
        }
        scoringScript.chosenPhotos = m_chosenPhotographs;
        startScoring.Raise();
        scoringScript.ScoreNextPhoto();        
    }
}
