using UniRx;
using UnityEngine;
public class ObstacleSpriteBalance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer obstacleSpriteRenderer;
    [SerializeField] private Transform player;
    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ => { SpriteBalance(); });
    }

    private void SpriteBalance()
    {
        obstacleSpriteRenderer.sortingOrder = player.position.y > transform.position.y ? 1 : 0;
    }
}
