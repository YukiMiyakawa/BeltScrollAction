using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ADVƒV[ƒ“‚Ì”wŒi‚ğ“®‚©‚·ƒNƒ‰ƒX
/// íœ—\’è
/// </summary>
public class SceneProcessForTrigger : SceneProcessIF
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void NextSceneProcess()
    {
        animator.SetTrigger("Next");
        base.NextSceneProcess();
    }
}
