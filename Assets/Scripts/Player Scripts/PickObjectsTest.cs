using System;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PickObjectsTest : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Animator animator;

    [Header("Player Behaviour with Interactable Objects")]
    [SerializeField] public bool canPickUp;
    [SerializeField] public bool canThrow;

    readonly GlobalStringVariables variables = new();
    private Controls controls;
    public static event Action ThrowItem;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Enable();
    }
    void Start()
    {
        playerCollider.OnTriggerStay2DAsObservable().Subscribe(_ =>
        {
            if (!canPickUp) { return; }
            if (controls.Player.PickUp.triggered && _.CompareTag(variables.PickObjectTag))
            {
                if (_.TryGetComponent(out WeaponScriptTest weaponScript))
                {
                    weaponScript.PickUp();
                    PickUpObject();
                }
            }
        });

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (!canThrow) { return; }
            if (controls.Player.PickUp.triggered)
            {
                ThrowObject();
            }
        });
    }

    private void PickUpObject()
    {
        animator.Play(variables.PickUpHash);
        canThrow = true;
        canPickUp = false;
        characterMovement.isAttackingWithWeapon = true;
        characterMovement.isRunningWithWeapon = true;
        characterMovement.isRunning = false;
        animator.SetBool(variables.IdleWithWeapon, true);
    }

    private async void ThrowObject()
    {
        animator.Play(variables.ThrowHash);
        await Task.Delay(300);
        ThrowItem?.Invoke();
        canPickUp = true;
        canThrow = false;
        characterMovement.isAttacking = true;
        animator.SetBool(variables.IdleWithWeapon, false);
        animator.SetBool(variables.WalkingWithWeapon, false);
        characterMovement.isAttackingWithWeapon = false;
        characterMovement.isRunningWithWeapon = false;
        //characterMovement.isRunning = true;
    }
}
