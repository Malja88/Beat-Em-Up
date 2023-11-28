using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PickObjectsTest : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Animator animator;
    readonly GlobalStringVariables variables = new();
    public static event Action ThrowItem;
    public static event Action OnPickUp;
    void Start()
    {
        playerCollider.OnTriggerStay2DAsObservable().Subscribe(_ =>
        {
            if (Input.GetKey(KeyCode.E) && _.CompareTag("PickObject"))
            {
                OnPickUp?.Invoke();
                characterMovement.isAttackingWithWeapon = true;
                animator.SetBool(variables.IdleWithWeapon, true);
            }
        });

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if ((Input.GetKeyUp(KeyCode.K)))
            {               
                ThrowItem?.Invoke();
                characterMovement.isAttacking = true;
                animator.SetBool(variables.IdleWithWeapon, false);
                characterMovement.isAttackingWithWeapon = false;
            }
        });
    }
}
