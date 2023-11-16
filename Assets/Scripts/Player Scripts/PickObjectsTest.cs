using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PickObjectsTest : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;
    public static event Action ThrowItem;
    public static event Action OnPickUp;
    void Start()
    {
        playerCollider.OnTriggerStay2DAsObservable().Subscribe(_ =>
        {
            if ((Input.GetKey(KeyCode.E) || Input.GetButtonDown("Fire1") && _.gameObject.CompareTag("PickObject")))
            {
                OnPickUp?.Invoke();
            }

        });

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if ((Input.GetKeyUp(KeyCode.K)) || Input.GetButtonDown("Fire2"))
            {               
                ThrowItem?.Invoke();
            }
        });
    }
}
