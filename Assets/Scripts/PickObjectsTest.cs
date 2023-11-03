using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PickObjectsTest : MonoBehaviour
{
    [SerializeField] private GameObject pipe;
    [SerializeField] private GameObject shadow;
    [SerializeField] private Collider2D playerCollider;
    public static event Action ThrowItem;
    public static event Action OnPickUp;
    void Start()
    {
        playerCollider.OnTriggerStay2DAsObservable().Where(trigger => trigger.CompareTag("PickObject")).Subscribe(_ =>
        {
            if ((Input.GetKeyDown(KeyCode.E)))
            {
                OnPickUp?.Invoke();
                pipe.transform.SetParent(transform);
                shadow.transform.SetParent(transform);
                pipe.transform.localPosition = new Vector2(1.35f, 2.7f);
                FlipWeapon();
            }

        });


        Observable.EveryUpdate().Subscribe(_ =>
        {
            if ((Input.GetKeyUp(KeyCode.K)))
            {
                
                ThrowItem?.Invoke();
            }
        });
    }

    private void FlipWeapon()
    {

        if (transform.position.x < pipe.transform.localPosition.x && transform.rotation == Quaternion.Euler(0,0,0))
        {
            pipe.transform.Rotate(0, 0, 0);
        }
        else if (transform.position.x > pipe.transform.localPosition.x && transform.rotation == Quaternion.Euler(0, -180, 0))
        {
            pipe.transform.Rotate(0, 180, 0);
        }
    }

}
