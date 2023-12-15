using UnityEngine;
using UniRx;

public class ObstacleSpriteBalance : MonoBehaviour
{
    private SpriteRenderer obstacleSpriteRenderer;

    void Start()
    {
        obstacleSpriteRenderer = GetComponent<SpriteRenderer>();
        Observable.EveryUpdate().Subscribe(_ => { SpriteBalance(); });
    }

    private void SpriteBalance()
    {
        if (obstacleSpriteRenderer != null)
        {
            obstacleSpriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        }
    }
}
