using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public bool isMoving;
    public bool isMovingBackwards;
    public float speed;
    public Rigidbody2D rb;
    public GameObject explosion;
    public GameObject[] enemies;
    public GameObject player;
    public GameObject dialogue;
    readonly GlobalStringVariables variables = new();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MoveBackwards();
    }

    private void MoveBackwards()
    {
        if (!isMoving) return;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void Move()
    {
        if (!isMovingBackwards) return;
        transform.Translate(Vector3.right * -speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemiesOnStart();

        }
    }

    private async void EnemiesOnStart()
    {
        foreach (var enemy in enemies)
        {
            isMoving = false;
            rb.AddForce(new Vector2(1, 1) * 10, ForceMode2D.Impulse);
            explosion.SetActive(true);
            var animator = enemy.GetComponentInChildren<Animator>();
            animator.Play("KnockDownByCar");
        }
        await Task.Delay(500);
        player.SetActive(true);
        await Task.Delay(500);
        isMovingBackwards = true;
        await Task.Delay(500);
        dialogue.SetActive(true);
        await Task.Delay(500);
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
