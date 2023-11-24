using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class WeaponScriptTest : MonoBehaviour
{
    [SerializeField] private Transform pipe, shadow, player, playerSpriteAnimation;
    [SerializeField] private Rigidbody2D weaponBody, shadowBody;
    [SerializeField] private BoxCollider2D shadowCollider;
    [SerializeField] private BoxCollider2D pipeCollider;
    [SerializeField] private SpriteRenderer weaponSpriteRenderer, shadowSpiteRenderer;
    [SerializeField] private CharacterMovement characterMovement;

    [Header("Throwing Item Settings, while jumping")]
    [SerializeField] private Vector2 pipeLeftThrowDirection, pipeRightThrowDirection, shadowLeftThrowDirection, shadowRightThrowDirection;
    [SerializeField] private float itemJumpingThrowForce;

    [Header("Throwing Item Settings, while walking")]
    [SerializeField] private Vector2 pipeLeftDirection, pipeRightDirection, shadowLeftDirection, shadowRightDirection;
    [SerializeField] private float itemThrowingForce;

    private bool isSpriteOrder, isThrow, isJumpingThrow, isPickUp, isJumpingWithWeapon;

    private void Start()
    {
        isSpriteOrder = isThrow = isPickUp = isJumpingThrow = true;

        PickObjectsTest.ThrowItem += ThrowObject;
        PickObjectsTest.OnPickUp += PickUp;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            Stay();
            SpriteBalance();
            JumpWithWeapon();
        });

        shadowCollider.OnCollisionEnter2DAsObservable().Subscribe(_ =>
        {
            if(_.gameObject.CompareTag("Enemy") || _.gameObject.CompareTag("Wall") || _.gameObject.CompareTag("Obstacle"))
            {
                ApplyRecoil();
            }

        });
    }

    private void JumpWithWeapon()
    {
        if(characterMovement.isJumping && isJumpingWithWeapon)
        {
            pipe.SetParent(playerSpriteAnimation);
            isThrow = false;
        }
        if(!isPickUp && isJumpingWithWeapon && !characterMovement.isJumping)
        {
            pipe.SetParent(player);
            isJumpingThrow = true;
            pipe.localPosition = new Vector2(1.4f, 2.56f);
        }
        if (Input.GetKeyDown(KeyCode.K) && characterMovement.isJumping)
        {
            if(!isJumpingThrow) { return; }
            isJumpingThrow = isJumpingWithWeapon = false;
            isPickUp = isSpriteOrder = true;
            shadowSpiteRenderer.enabled = true;
            shadowCollider.enabled = true;
            transform.SetParent(null);
            pipe.SetParent(shadow);
            ThrowWeaponDirection(pipeLeftThrowDirection, shadowLeftThrowDirection, pipeRightThrowDirection, shadowRightThrowDirection, itemJumpingThrowForce);
        }
    }

    public void PickUp()
    {
        if (!isPickUp) { return; }
        isThrow = isJumpingWithWeapon = true;
        isSpriteOrder = isPickUp = false;
        weaponBody.bodyType = shadowBody.bodyType = RigidbodyType2D.Kinematic;
        pipe.SetParent(player);
        shadow.SetParent(player);
        weaponSpriteRenderer.sortingOrder = 1;
        shadowSpiteRenderer.enabled = false;
        shadowCollider.enabled = false;
        pipe.localPosition = new Vector2(1.4f, 2.56f);
        shadow.transform.localPosition = new Vector2(1.4f, 0);
        FlipWeapon();
    }

    public void ThrowObject()
    {
         if (!isThrow) { return; }
        shadowSpiteRenderer.enabled = true;
        isJumpingWithWeapon = isThrow = false;
        isSpriteOrder = isPickUp =true;
        transform.SetParent(null);
        pipe.SetParent(shadow);
        shadowCollider.enabled = true;
        ThrowWeaponDirection(pipeLeftDirection, shadowLeftDirection, pipeRightDirection, shadowRightDirection, itemThrowingForce);
    }


    private void Stay()
    {
        RaycastHit2D hit = Physics2D.Raycast(pipe.position, Vector2.down, 0.1f, LayerMask.GetMask("Base"));

        if (hit.collider != null)
        {
            weaponBody.bodyType = shadowBody.bodyType = RigidbodyType2D.Static;
        }
    }

    private void FlipWeapon()
    {
        if (transform.position.x < pipe.transform.localPosition.x && transform.rotation == Quaternion.Euler(0, 0, 0))
        {
            pipe.transform.Rotate(0, 0, 0);
        }
        else if (transform.position.x > pipe.transform.localPosition.x && transform.rotation == Quaternion.Euler(0, -180, 0))
        {
            pipe.transform.Rotate(0, 180, 0);
        }
    }

    private void SpriteBalance()
    {
        if (!isSpriteOrder) return;
        weaponSpriteRenderer.sortingOrder = characterMovement.isJumping && player.position.y < transform.position.y ? -10 : 0;
    }

    private void ThrowWeaponDirection(Vector2 pipeThrowDirection, Vector2 shadowThrowDirection, Vector2 pipeThrowDirection2, Vector2 shadowThrowDirection2, float throwForce)
    {
        if (player.transform.rotation.y < 0)
        {
            shadowBody.bodyType = weaponBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.AddForce(pipeThrowDirection * throwForce, ForceMode2D.Impulse);
            shadowBody.AddForce(shadowThrowDirection * throwForce, ForceMode2D.Impulse);
        }
        else
        {
            shadowBody.bodyType = weaponBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.AddForce(pipeThrowDirection2 * throwForce, ForceMode2D.Impulse);
            shadowBody.AddForce(shadowThrowDirection2 * throwForce, ForceMode2D.Impulse);
        }
    }

    private async void ApplyRecoil()
    {
        var recoilPos = new Vector2(-0.5f, 0);
        shadowBody.velocity = weaponBody.velocity = recoilPos;
        pipe.position = new Vector2(transform.position.x, pipe.position.y);
        await Task.Delay(400);
        shadowBody.velocity = weaponBody.velocity = Vector2.zero;
        /// Make it work by Axises
    }
}