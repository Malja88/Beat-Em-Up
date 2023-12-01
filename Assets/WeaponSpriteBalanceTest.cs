using UniRx;
using UnityEngine;

public class WeaponSpriteBalanceTest : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ => { spriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder; });       
    }

}
