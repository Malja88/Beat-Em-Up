using System;
using UnityEngine;
using UniRx;

public class ObstacleSpriteBalance : MonoBehaviour, IDisposable
{
    private SpriteRenderer obstacleSpriteRenderer;
    private IDisposable updateSubscription;

    void Start()
    {
        obstacleSpriteRenderer = GetComponent<SpriteRenderer>();

        updateSubscription = Observable.EveryUpdate()
            .TakeUntilDestroy(this)
            .Subscribe(_ => { SpriteBalance(); });
    }

    private void SpriteBalance()
    {
        if (obstacleSpriteRenderer != null)
        {
            obstacleSpriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        }
    }

    public void Dispose()
    {
        updateSubscription?.Dispose();
    }
}
