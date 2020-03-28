using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBook : MonoBehaviour
{
    public Animator frontCoverAnimator;

    public void PlayFrontCover()
    {
        frontCoverAnimator.Play("open_front_cover");
    }
}
