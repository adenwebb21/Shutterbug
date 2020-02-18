using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPictures : MonoBehaviour
{
    public GameObject photoPrefab;
    public GameObject pictureSpawn;

    public Sprite defaultPhoto;

    private Vector3 m_forceVector, m_rotationForce;

    private void Start()
    {
        m_rotationForce = new Vector3(0, 1, 0);
        m_forceVector = new Vector3(1, 0, 0);


        StartSpawning();    
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnPhotos(0.2f));
    }

    IEnumerator SpawnPhotos(float _delayTime)
    {
        

        if (GameManager.Instance)
        {
            for (int i = 0; i < GameManager.Instance.currentPhotographs.Count; i++)
            {
                yield return new WaitForSeconds(_delayTime);
                GameObject _tempPhoto = Instantiate(photoPrefab, pictureSpawn.transform);
                _tempPhoto.transform.position = pictureSpawn.transform.position;

                try
                {
                    _tempPhoto.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.currentPhotographs[i].Image;
                }
                catch
                {
                    _tempPhoto.GetComponent<SpriteRenderer>().sprite = defaultPhoto;
                }

                _tempPhoto.GetComponent<Rigidbody>().AddTorque(m_rotationForce * Random.Range(-2f, 2f), ForceMode.Impulse);
                m_forceVector.z = Random.Range(-0.25f, 0.25f);
                _tempPhoto.GetComponent<Rigidbody>().AddForce(m_forceVector * Random.Range(20f, 25f), ForceMode.Impulse);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(_delayTime);
                GameObject _tempPhoto = Instantiate(photoPrefab, pictureSpawn.transform);
                _tempPhoto.transform.position = pictureSpawn.transform.position;

                try
                {
                    _tempPhoto.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.currentPhotographs[i].Image;
                }
                catch
                {
                    _tempPhoto.GetComponent<SpriteRenderer>().sprite = defaultPhoto;
                }

                _tempPhoto.GetComponent<Rigidbody>().AddTorque(m_rotationForce * Random.Range(-10f, 10f), ForceMode.Impulse);
                m_forceVector.z = Random.Range(-0.25f, 0.25f);
                _tempPhoto.GetComponent<Rigidbody>().AddForce(m_forceVector * Random.Range(20f, 35f), ForceMode.Impulse);
            }
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GameObject[] _temp = GameObject.FindGameObjectsWithTag("PhotoObject");
            foreach (GameObject _obj in _temp)
                Destroy(_obj);
            StartSpawning();
        }
    }
}
