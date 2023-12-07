using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrongEnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private StrongEnemyAi strongEnemyAI;
    [SerializeField] private new Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;
    readonly GlobalStringVariables variables = new();
    public void EnemyMovementOff()
    {
        //strongEnemyAI.canAttack = false;
        strongEnemyAI.isIdle = false;
        strongEnemyAI.isAttacking = false;
        //rigidbody2D.bodyType = RigidbodyType2D.Static;
        rigidbody2D.velocity = Vector2.zero;
        strongEnemyAI.timer = 0;
    }

    public void EnemyMovementOn()
    {
        //strongEnemyAI.canAttack = true;
        strongEnemyAI.isIdle = true;
        //rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public void GetUpAnimationOn()
    {
        animator.Play(variables.EnemyGetUp);
    }

    public void DisableEnemyColldier()
    {
        boxCollider.enabled = false;
    }

    public void EnableEnemyColldier()
    {
        boxCollider.enabled = true;
    }
}
