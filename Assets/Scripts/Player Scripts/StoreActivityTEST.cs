using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class StoreActivityTEST : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject storeDialogueBox;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private new Rigidbody2D rigidbody;
    void Start()
    {
        boxCollider.OnCollisionStay2DAsObservable().Where(x => x.gameObject.CompareTag("Store") && Input.GetKey(KeyCode.T)).Subscribe(_ => 
        {
            storeDialogueBox.SetActive(true);
            characterMovement.isMoving = false;
            rigidbody.bodyType = RigidbodyType2D.Static;
        });
    }
}
