using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class WeaponScriptTest : MonoBehaviour
{
    [Header("Body Componenets")]
    [SerializeField] private Transform pipe, shadow, player, playerSpriteAnimation;
    [SerializeField] private Rigidbody2D weaponBody, shadowBody;
    [SerializeField] private BoxCollider2D shadowCollider, damageCollider;
    [SerializeField] private SpriteRenderer weaponSpriteRenderer, shadowSpiteRenderer;
    [SerializeField] private CharacterMovement characterMovement;

    [Header("Weapon Position on Pick Up")]
    [SerializeField] private Vector2 pipePositionOnPlayer, shadowPositionOnPlayer;

    [Header("Throwing Item Settings, while jumping")]
    [SerializeField] private Vector2 pipeLeftThrowDirection, pipeRightThrowDirection, shadowLeftThrowDirection, shadowRightThrowDirection;
    [SerializeField] private float itemJumpingThrowForce;

    [Header("Throwing Item Settings, while walking")]
    [SerializeField] private Vector2 pipeLeftDirection, pipeRightDirection, shadowLeftDirection, shadowRightDirection;
    [SerializeField] private float itemThrowingForce;
    [SerializeField] private float recoilPower;

    public bool isSpriteOrder, isThrow, isJumpingThrow, isPickUp, isJumpingWithWeapon;
    readonly GlobalStringVariables variables = new();
    private void Start()
    {
        isSpriteOrder = isPickUp = isJumpingThrow = true;

        PickObjectsTest.ThrowItem += ThrowObject;


        Observable.EveryUpdate().Subscribe(_ =>
        {
            Stay();
            JumpWithWeapon();
            DynamicSpriteBalance();
        });

        shadowCollider.OnCollisionEnter2DAsObservable().Subscribe(_ =>
        {
            if(_.gameObject.CompareTag(variables.PipeTag))
            {
                weaponBody.bodyType = shadowBody.bodyType = RigidbodyType2D.Static;
            }
            else
            {
                ApplyRecoil();
            }
                             
        });

        damageCollider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag(variables.EnemyTag)).Subscribe(x =>
        {
            ApplyRecoil();
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
            isThrow = true;
            pipe.localPosition = pipePositionOnPlayer;
        }
        //if (Input.GetKeyDown(KeyCode.K) && characterMovement.isJumping)
        //{
        //    if(!isJumpingThrow) { return; }
        //    weaponSpriteRenderer.enabled = true;
        //    isJumpingThrow = isJumpingWithWeapon = false;
        //    isPickUp = isSpriteOrder = true;
        //    shadowSpiteRenderer.enabled = true;
        //    shadowCollider.enabled = true;
        //    damageCollider.enabled = true;
        //    transform.SetParent(null);
        //    pipe.SetParent(shadow);
        //    ThrowWeaponDirection(pipeLeftThrowDirection, shadowLeftThrowDirection, pipeRightThrowDirection, shadowRightThrowDirection, itemJumpingThrowForce);
        //}
    }

    public void PickUp()
    {
        if (!isPickUp) { return; }
        isThrow = isJumpingWithWeapon = true;
        isSpriteOrder = isPickUp = false;
        weaponBody.bodyType = shadowBody.bodyType = RigidbodyType2D.Kinematic;
        weaponSpriteRenderer.enabled = false ;
        pipe.SetParent(player);
        shadow.SetParent(player);
        weaponSpriteRenderer.sortingOrder = 1;
        shadowSpiteRenderer.enabled = false;
        shadowCollider.enabled = false;
        pipe.localPosition = pipePositionOnPlayer;
        shadow.transform.localPosition = shadowPositionOnPlayer;
        //FlipWeapon();
    }

    public void ThrowObject()
    {
         if (!isThrow) { return; }
        shadowSpiteRenderer.enabled = true;
        weaponSpriteRenderer.enabled = true;
        isPickUp = true;
        isJumpingWithWeapon = isThrow = false;
        isSpriteOrder = isPickUp =true;
        transform.SetParent(null);
        pipe.SetParent(shadow);
        shadowCollider.enabled = true;
        damageCollider.enabled = true;
        ThrowWeaponDirection(pipeLeftDirection, shadowLeftDirection, pipeRightDirection, shadowRightDirection, itemThrowingForce);
    }


    private void Stay()
    {
        RaycastHit2D hit = Physics2D.Raycast(pipe.position, Vector2.down, 0.1f, LayerMask.GetMask(variables.BaseTag));

        if (hit.collider != null)
        {
            damageCollider.enabled = false;
        }
    }

    //private void FlipWeapon()
    //{
    //    if (transform.position.x < pipe.transform.localPosition.x && transform.rotation == Quaternion.Euler(0, 0, 0))
    //    {
    //        pipe.transform.Rotate(0, 0, 0);
    //    }
    //    else if (transform.position.x > pipe.transform.localPosition.x && transform.rotation == Quaternion.Euler(0, -180, 0))
    //    {
    //        pipe.transform.Rotate(0, 180, 0);
    //    }
    //}

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
        if(player.rotation.y > 0 && !isThrow)
        {
            var recoilPos = new Vector2(-recoilPower, 0);
            shadowBody.velocity = weaponBody.velocity = recoilPos;
            pipe.position = new Vector2(transform.position.x, pipe.position.y);
            await Task.Delay(400);
            shadowBody.velocity = weaponBody.velocity = Vector2.zero;
        }

        else
        {
            var recoilPos = new Vector2(recoilPower, 0);
            shadowBody.velocity = weaponBody.velocity = recoilPos;
            pipe.position = new Vector2(transform.position.x, pipe.position.y);
            await Task.Delay(400);
            shadowBody.velocity = weaponBody.velocity = Vector2.zero;
        }
    }

    private void DynamicSpriteBalance()
    {
        int sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        weaponSpriteRenderer.sortingOrder = sortingOrder + 1;
        shadowSpiteRenderer.sortingOrder = sortingOrder;
    }
}