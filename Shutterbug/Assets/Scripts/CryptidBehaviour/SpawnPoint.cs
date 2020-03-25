using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public enum EnterFrom
    {
        Right,
        Left,
        Up,
        Down,
        Either
    }

    public enum ExitTo
    {
        Right,
        Left,
        Up,
        Down,
        Either
    }

    public EnterFrom enterDirection;
    public ExitTo exitDirection;
    public Region spawnRegion;

    public void Spawn(GameObject _cryptid)
    {
        _cryptid.transform.position = transform.position;
        _cryptid.transform.rotation = transform.rotation;


        switch (enterDirection)
        {
            case EnterFrom.Right:
                _cryptid.GetComponentInChildren<Animator>().SetTrigger("EnterFromRight");
                break;
            case EnterFrom.Left:
                _cryptid.GetComponentInChildren<Animator>().SetTrigger("EnterFromLeft");
                break;
            case EnterFrom.Either:
                if(Random.Range(0f, 1f) >= 0.5f)
                {
                    goto case EnterFrom.Right;
                }
                else
                {
                    goto case EnterFrom.Left;
                }
            case EnterFrom.Up:
                {
                    //todo: actual implementation here
                    goto case default;
                }
            case EnterFrom.Down:
                {
                    //todo: actual implementation here
                    goto case default;
                }
            default:
                Debug.Log("Invalid enum type");
                break;
        }   
    }

    public void Leave(GameObject _cryptid)
    {
        switch (exitDirection)
        {
            case ExitTo.Right:
                _cryptid.GetComponentInChildren<Animator>().SetTrigger("ExitToRight");
                break;
            case ExitTo.Left:
                _cryptid.GetComponentInChildren<Animator>().SetTrigger("ExitToLeft");
                break;
            case ExitTo.Either:
                if (Random.Range(0f, 1f) >= 0.5f)
                {
                    goto case ExitTo.Right;
                }
                else
                {
                    goto case ExitTo.Left;
                }
            case ExitTo.Up:
                {
                    //todo: actual implementation here
                    goto case default;
                }
            case ExitTo.Down:
                {
                    //todo: actual implementation here
                    goto case default;
                }
            default:
                Debug.Log("Invalid enum type");
                break;
        }
    }
}
