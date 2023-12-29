using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public bool isMoving;
    public float speed;
    public Rigidbody2D rb;
    public GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!isMoving) return;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isMoving = false;
            rb.AddForce(new Vector2(1,1) * 15, ForceMode2D.Impulse);
            explosion.SetActive(true);
        }
    }
}
