using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class WeaponScriptTest : MonoBehaviour
{
    [SerializeField] private Transform pipe;
    [SerializeField] private Transform shadow;
    [SerializeField] private Rigidbody2D weaponBody;
    [SerializeField] private Rigidbody2D shadowBody;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSpriteAnimation;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private SpriteRenderer weaponSpriteRenderer;
    [SerializeField] private SpriteRenderer shadowSpiteRenderer;
    private bool isSpriteOrder;
    public bool isThrow;
    public bool isPickUp;
    public bool isJumpingWithWeapon;
    private void Start()
    {
        isSpriteOrder = true;
        isThrow = true;
        isPickUp = true;
        PickObjectsTest.ThrowItem += ThrowObject;
        PickObjectsTest.OnPickUp += PickUp;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            Stay();
            SpriteBalance();
            JumpWithWeapon();
            //SpriteEnemyBalance();
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
            isThrow = true;
            pipe.localPosition = new Vector2(1.4f, 2.56f);
        }
        if (Input.GetKeyDown(KeyCode.K) && characterMovement.isJumping)
        {
            Debug.Log("Throw");
            isPickUp = true;
            isJumpingWithWeapon = false;
            shadowSpiteRenderer.enabled = true;
            transform.SetParent(null);
            pipe.SetParent(shadow);
            isSpriteOrder = true;


            //weaponBody.AddForce(new Vector2(3, -1f) * 2, ForceMode2D.Impulse);
            //shadowBody.AddForce(new Vector2(3, 0) * 2, ForceMode2D.Impulse);
            ThrowWeaponDirection(new Vector2(3,1), new Vector2(3,0), 2);
        }
        //if (characterMovement.isJumping && isPickUp)
        //{

        //    isSpriteOrder = false;
        //    weaponBody.bodyType = RigidbodyType2D.Kinematic;
        //    shadowBody.bodyType = RigidbodyType2D.Kinematic;
        //    pipe.SetParent(playerSpriteAnimation);
        //    shadow.SetParent(playerSpriteAnimation);
        //    weaponSpriteRenderer.sortingOrder = 1;
        //    shadowSpiteRenderer.enabled = false;
        //    pipe.localPosition = new Vector2(1.4f, 2.56f);
        //    shadow.transform.localPosition = new Vector2(1.4f, 0);
        //    FlipWeapon();
        //}
        //else if(!characterMovement.isJumping && isPickUp)
        //{
        //    pipe.SetParent(null);
        //    shadow.SetParent(null);
        //   // pipe.localPosition = new Vector2(1.4f, 2.56f);
        //}
    }
    public void PickUp()
    {
        if (!isPickUp) { return; }
        isSpriteOrder = false;
        isJumpingWithWeapon = true;
        isPickUp = false;
        weaponBody.bodyType = RigidbodyType2D.Kinematic;
        shadowBody.bodyType = RigidbodyType2D.Kinematic;
        pipe.SetParent(player);
        shadow.SetParent(player);
        weaponSpriteRenderer.sortingOrder = 1;
        shadowSpiteRenderer.enabled = false;
        pipe.localPosition = new Vector2(1.4f, 2.56f);
        shadow.transform.localPosition = new Vector2(1.4f, 0);
        FlipWeapon();
    }
    public void ThrowObject()
    {
        if (!isThrow) { return; }
        if (player.rotation == Quaternion.Euler(0, 0, 0))
        {
            shadowSpiteRenderer.enabled = true;
            isJumpingWithWeapon = false;
            transform.SetParent(null);
            pipe.SetParent(shadow);
            isSpriteOrder = true;
            isPickUp = true;
            shadowBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.AddForce(new Vector2(3, 1f) * 4, ForceMode2D.Impulse);
            shadowBody.AddForce(new Vector2(3, 0) * 4, ForceMode2D.Impulse);
        }

        else
        {
            shadowSpiteRenderer.enabled = true;
            transform.SetParent(null);
            pipe.SetParent(shadow);
            isSpriteOrder = false;
            isPickUp = true;
            shadowBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.AddForce(new Vector2(3, -1f) * -4, ForceMode2D.Impulse);
            shadowBody.AddForce(new Vector2(3, 0) * -4, ForceMode2D.Impulse);
        }
    }


    private void Stay()
    {
        RaycastHit2D hit = Physics2D.Raycast(pipe.position, Vector2.down, 0.2f, LayerMask.GetMask("Base"));

        if (hit.collider != null)
        {
            weaponBody.bodyType = RigidbodyType2D.Static;
            shadowBody.bodyType = RigidbodyType2D.Static;
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
        if (!isSpriteOrder) { return; }
        if (characterMovement.isJumping && player.position.y < transform.position.y)
        {
            weaponSpriteRenderer.sortingOrder = -10;
        }
        else if (!characterMovement.isJumping)
        {
            weaponSpriteRenderer.sortingOrder = 0;
        }
    }

    private void SpriteEnemyBalance()
    {
        if (transform.position.y > ((gameObject.GetComponent(typeof(EnemyAI))).transform.position.y))
        {
            weaponSpriteRenderer.sortingOrder = -10;
        }
        else
        {
            weaponSpriteRenderer.sortingOrder = 0;
        }
    }

    private void ThrowWeaponDirection(Vector2 pipeThrowDirection, Vector2 shadowThrowDirection, float throwForce)
    {
        if(player.transform.rotation.y < 0)
        {
            shadowBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.AddForce(pipeThrowDirection * -throwForce, ForceMode2D.Impulse);
            shadowBody.AddForce(shadowThrowDirection * -throwForce, ForceMode2D.Impulse);
        }
        else
        {
            shadowBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.AddForce(pipeThrowDirection * throwForce, ForceMode2D.Impulse);
            shadowBody.AddForce(shadowThrowDirection * throwForce, ForceMode2D.Impulse);
        }
    }
}