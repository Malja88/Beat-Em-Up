using UniRx;
using UnityEngine;

public class ObstacleSpriteBalance2 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Obstacle"))
    //    {
    //        spriteRenderer.sortingOrder = -1;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    spriteRenderer.sortingOrder = 0;
    //}

    private void Update()
    {
        // Adjust the sorting order based on the Y position of the player
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
