using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private RectTransform m_rectTransform;

    private void Start()
    {
        m_rectTransform = gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 _mousePos = Input.mousePosition;

        m_rectTransform.position = new Vector3(Mathf.Clamp(_mousePos.x, 0, Screen.width), Mathf.Clamp(_mousePos.y, 0, Screen.height), 0f);
        Cursor.visible = false;
    }
}
