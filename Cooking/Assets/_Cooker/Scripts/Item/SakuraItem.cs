using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakuraItem : MonoBehaviour
{
    [SpineAnimation]
    public string IdleAnimationName;

    [SpineAnimation]
    public string RungAnimationName;

    [SerializeField] SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    private void Start()
    {
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }
    
    private void OnMouseDown()
    {
        spineAnimationState.SetAnimation(0, RungAnimationName, false);
        spineAnimationState.AddAnimation(0, IdleAnimationName, true, 0);
    }
}
