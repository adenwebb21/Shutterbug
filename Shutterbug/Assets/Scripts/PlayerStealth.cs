using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerStealth : MonoBehaviour
{
    private Keyboard m_keyboard;

    private CharacterController m_characterController;
    private FirstPersonController m_firstPersonController;

    private float m_defaultWalkSpeed;
    private float m_defaultRunSpeed;

    private Vector3 m_normalHeadPos = new Vector3(0f, 0.8f, 0f);
    private Vector3 m_sneakingHeadPos = new Vector3(0f, 0.415f, 0f);

    public GameObject head;

    public float sneakingHeight = 0.9f;
    public float normalHeight = 1.8f;

    private void Start()
    {
        m_keyboard = Keyboard.current;

        m_defaultWalkSpeed = gameObject.GetComponent<FirstPersonController>().WalkSpeed;
        m_defaultRunSpeed = gameObject.GetComponent<FirstPersonController>().RunSpeed;

        m_characterController = gameObject.GetComponent<CharacterController>();
        m_firstPersonController = gameObject.GetComponent<FirstPersonController>();
    }

    private void Update()
    {        
        if (m_keyboard.leftCtrlKey.wasPressedThisFrame || m_keyboard.cKey.wasPressedThisFrame)
        {
            StartSneaking();
        }
        else if(m_keyboard.leftCtrlKey.wasReleasedThisFrame || m_keyboard.cKey.wasReleasedThisFrame)
        {
            EndSneaking();
        }
    }

    public void StartSneaking()
    {
        StartCoroutine(LerpHead(head.transform.localPosition, m_sneakingHeadPos, 0.2f));
        StartCoroutine(LerpHeight(m_characterController.height, sneakingHeight, 0.2f));

        m_firstPersonController.WalkSpeed = m_defaultWalkSpeed / 2;
        m_firstPersonController.RunSpeed = m_defaultRunSpeed / 2;
    }

    public void EndSneaking()
    {
        StartCoroutine(LerpHead(head.transform.localPosition, m_normalHeadPos, 0.2f));
        StartCoroutine(LerpHeight(m_characterController.height, normalHeight, 0.2f));

        m_firstPersonController.WalkSpeed = m_defaultWalkSpeed;
        m_firstPersonController.RunSpeed = m_defaultRunSpeed;
    }

    IEnumerator LerpHead(Vector3 _startingPos, Vector3 _newPosition, float _time)
    {
        float _elapsedTime = 0;

        while (_elapsedTime < _time)
        {
            head.transform.localPosition = Vector3.Lerp(_startingPos, _newPosition, (_elapsedTime / _time));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator LerpHeight(float _startingPos, float _newPosition, float _time)
    {
        float _elapsedTime = 0;

        while (_elapsedTime < _time)
        {
            m_characterController.height = Mathf.Lerp(_startingPos, _newPosition, (_elapsedTime / _time));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
