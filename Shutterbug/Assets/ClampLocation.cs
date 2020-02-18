using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampLocation : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 _tempLocation;
        _tempLocation = transform.position;
        _tempLocation.x = Mathf.Clamp(_tempLocation.x, -2f, 6f);
        _tempLocation.z = Mathf.Clamp(_tempLocation.z, 4f, 19.5f);

        transform.position = _tempLocation;
    }
}
