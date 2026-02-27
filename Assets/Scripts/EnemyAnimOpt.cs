using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimOpt : MonoBehaviour
{
    private Animator parentAnimator;

    void Awake()
    {
        parentAnimator = GetComponentInParent<Animator>();
    }

    void OnBecameVisible()
    {
        parentAnimator.enabled = true;
    }

    void OnBecameInvisible()
    {
        parentAnimator.enabled = false;
    }
}