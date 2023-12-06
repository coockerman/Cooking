using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    //Intro1
    [SpineAnimation]
    public string IntroStart;
    [SpineAnimation]
    public string IntroIdle;
    [SpineAnimation]
    public string IntroEnd;
    
    [SerializeField] SkeletonAnimation skeletonAnimation1;
    [SerializeField] SkeletonAnimation skeletonAnimation2;
    [SerializeField] SkeletonAnimation skeletonAnimation3;
    [SerializeField] float timeDelayIntro;
    [SerializeField] float timeDelayIdle;

    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    private void Start()
    {
        StartCoroutine(PlayIntro1());
    }
    void SetUpIntro(SkeletonAnimation skeletonAnimation)
    {
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }
    void StartIntro(string start, string idle, string endl)
    {
        spineAnimationState.SetAnimation(0, start, false);
        spineAnimationState.AddAnimation(0, idle, true, timeDelayIdle);
        spineAnimationState.AddAnimation(0, endl, false, 0);
    }
    private IEnumerator PlayIntro1()
    {
        skeletonAnimation1.gameObject.SetActive(true);
        SetUpIntro(skeletonAnimation1);
        StartIntro(IntroStart, IntroIdle, IntroEnd);

        yield return new WaitForSeconds(timeDelayIntro);

        skeletonAnimation1.gameObject.SetActive(false);
        StartCoroutine(PlayIntro2());
    }
    private IEnumerator PlayIntro2()
    {
        skeletonAnimation2.gameObject.SetActive(true);
        SetUpIntro(skeletonAnimation2);
        StartIntro(IntroStart, IntroIdle, IntroEnd);

        yield return new WaitForSeconds(timeDelayIntro);

        skeletonAnimation2.gameObject.SetActive(false);
        StartCoroutine(PlayIntro3());
    }
    private IEnumerator PlayIntro3()
    {
        skeletonAnimation3.gameObject.SetActive(true);
        SetUpIntro(skeletonAnimation3);
        StartIntro(IntroStart, IntroIdle, IntroEnd);

        yield return new WaitForSeconds(timeDelayIntro);

        skeletonAnimation3.gameObject.SetActive(false);
        NextScene();
    }
    void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}
