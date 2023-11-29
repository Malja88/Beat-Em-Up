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

        // Subscribe to EveryUpdate with TakeUntilDestroy
        updateSubscription = Observable.EveryUpdate()
            .TakeUntilDestroy(this)
            .Subscribe(_ => { SpriteBalance(); });
    }

    private void SpriteBalance()
    {
        // Check if the obstacleSpriteRenderer is null before accessing
        if (obstacleSpriteRenderer != null)
        {
            obstacleSpriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        }
    }

    // Implement IDisposable to properly dispose of the subscription
    public void Dispose()
    {
        // Dispose of the subscription
        updateSubscription?.Dispose();
    }
}
