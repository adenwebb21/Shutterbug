using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TakePhoto : MonoBehaviour
{
    private int fileCounter = 0;

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Capture();
        }
    }

    private void Capture()
    {
        Camera _cam = GetComponent<Camera>();

        RenderTexture _currentRenderTexture = RenderTexture.active;
        RenderTexture.active = _cam.targetTexture;

        _cam.Render();

        Texture2D _image = new Texture2D(_cam.targetTexture.width, _cam.targetTexture.height);
        _image.ReadPixels(new Rect(0, 0, _cam.targetTexture.width, _cam.targetTexture.height), 0, 0);
        _image.Apply();
        RenderTexture.active = _currentRenderTexture;

        var _bytes = _image.EncodeToPNG();
        Destroy(_image);

        string _filePath = Application.dataPath + "/Pictures/" + fileCounter + ".png";
        File.WriteAllBytes(_filePath, _bytes);
        fileCounter++;

        Sprite _newPhoto = ImageToSprite.LoadNewSprite(_filePath);
        Photograph _tempPhotograph = ScriptableObject.CreateInstance<Photograph>();
        _tempPhotograph.Image = _newPhoto;
        _tempPhotograph.CryptidInPicture = IsVisible(GameManager.Instance.currentCryptid);

        PhotoManager.Instance.currentPhotographs.Add(_tempPhotograph);
        PhotoManager.Instance.TakePhoto();
    }

    private bool IsVisible(GameObject _cryptid)
    {
        bool _inShot = false;

        foreach (Transform _object in _cryptid.GetComponent<CryptidProperties>().bodyParts)
        {
            Vector3 _screenPoint = gameObject.GetComponent<Camera>().WorldToViewportPoint(_object.position);

            if (_screenPoint.z > 0 && _screenPoint.x > 0 && _screenPoint.x < 1 && _screenPoint.y > 0 && _screenPoint.y < 1 && !EnvironmentInWay(_object))
            {
                _inShot = true;
                Debug.Log(_object.name);
                break;
            }
            else
            {
                _inShot = false;
            }
        }

        Debug.Log(_inShot);
        return _inShot;
    }

    private bool EnvironmentInWay(Transform _bodyPart)
    {
        bool _environmentInWay = false;

        RaycastHit _rayHit;
        Vector3 _directionToPoint = -(gameObject.transform.position - _bodyPart.transform.position).normalized;

        if (Physics.Raycast(transform.position, _directionToPoint, out _rayHit, Vector3.Distance(transform.position, _bodyPart.position)) && _rayHit.collider.gameObject.tag == "Environment")
        {
            _environmentInWay = true;
        }
        else
        {
            _environmentInWay = false;
        }

        return _environmentInWay;
    }

    private void Update()
    {
        foreach (Transform _object in GameManager.Instance.currentCryptid.GetComponent<CryptidProperties>().bodyParts)
        {
            RaycastHit _rayHit;
            Vector3 _directionToPoint = -(gameObject.transform.position - _object.transform.position).normalized;

            if (Physics.Raycast(transform.position, _directionToPoint, out _rayHit, Vector3.Distance(transform.position, _object.position)))
            {
                if (_rayHit.collider.gameObject.tag == "Cryptid")
                {
                    Debug.DrawRay(transform.position, _directionToPoint * 1000, Color.green);
                }
                else if (_rayHit.collider.gameObject.tag == "Environment")
                {
                    Debug.DrawRay(transform.position, _directionToPoint * 1000, Color.red);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, _directionToPoint * 1000, Color.white);
            }
        }

    }
}
