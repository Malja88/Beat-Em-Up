using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class StoreActivityTEST : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject storeDialogueBox;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private KeyCode enterStoreButton;
    readonly GlobalStringVariables variables = new();
    void Update()
    {
        boxCollider.OnTriggerStay2DAsObservable().Where(x => x.CompareTag(variables.StoreTag) && Input.GetKey(enterStoreButton)).Subscribe(_ => 
        {
            storeDialogueBox.SetActive(true);
            characterMovement.isMoving = false;
        });

        if(Input.GetKey(KeyCode.Y) )
        {
            storeDialogueBox.SetActive(false);
            characterMovement.isMoving = true;
        }
    }
}
