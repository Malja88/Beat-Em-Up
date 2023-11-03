using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScriptTest : MonoBehaviour
{
    [SerializeField] private Transform pipe;
    [SerializeField] private Transform shadow;
    [SerializeField] private Rigidbody2D weaponBody;
    [SerializeField] private Transform player;
    private void Start()
    {
        PickObjectsTest.ThrowItem += ThrowObject;
        PickObjectsTest.OnPickUp += PickUp;
    }

    public void ThrowObject()
    {
        if(transform.position.y > player.position.y)
        {
            transform.SetParent(null);
            shadow.SetParent(pipe) ;


            weaponBody.bodyType = RigidbodyType2D.Dynamic;
            weaponBody.AddForce(new Vector2(0.2f,1) * 5, ForceMode2D.Impulse);
            weaponBody.gravityScale = 0.5f;
        }

        if (transform.position.y < player.position.y)
        {
            Debug.Log("Test");
            // weaponBody.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void PickUp()
    {
        weaponBody.bodyType = RigidbodyType2D.Kinematic;
    }
}
