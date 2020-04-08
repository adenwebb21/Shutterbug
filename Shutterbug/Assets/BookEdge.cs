using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookEdge : MonoBehaviour
{
    public Animator edgeAnimator;

    public void PlayFrontCover()
    {
        edgeAnimator.Play("cover_edge_close");
    }
}
