using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class StrongEnemyAi : EnemyAI, IChase
{
    public bool isChasing;
    [SerializeField] private float followDistance;
    [SerializeField] private float chasingSpeed;
    [SerializeField] private int action;
    [SerializeField] private float delay;
    private int kek;
    readonly GlobalStringVariables variables = new();
    void Start()
    {
        action = Random.Range(0, 4);
        Observable.EveryUpdate().Subscribe(_ => 
        {
            Flip();
            DynamicSpriteRender();
        });

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            Idle();
            Chase();
        });
    }

    protected new virtual void Idle()
    {
        base.Idle();
    }

    protected new virtual void Flip()
    {
        base.Flip();
    }

    protected virtual void DynamicSpriteRenderer()
    {
        DynamicSpriteRender();
    }

    public void Chase()
    {
        if(isChasing) { return; }
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < followDistance)
        {
            isIdle = false;
            isChasing = true;
            Vector2 direction = (player.position - transform.position).normalized;
            rb2d.velocity = direction * chasingSpeed;
        }

        if (distanceToPlayer < attackDistance)
        {
            isAttacking = true;
            isChasing = false;
            isChasing = false;
        }
    }    
}
