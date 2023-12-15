using DG.Tweening;
using UniRx;
using UnityEngine;

public class UIArrowManager : MonoBehaviour
{
    readonly GlobalStringVariables variables = new();
    [SerializeField] private RectTransform rect;
    [SerializeField] private GameObject storeBox;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        Observable.EveryUpdate().Subscribe(_ => 
        {
            if (Input.GetButtonDown(variables.VerticalAxis))
            {
                MoveCursor();
            }
            DetectPlayerStatsInStore();
        });
    }

    private void MoveCursor()
    {
        float verticalInput = Input.GetAxis(variables.VerticalAxis);

        if(verticalInput < 0 &&  rect.localPosition.y == 0)
        {
            rect.DOLocalMoveY(-16, 0.01f);
        }
        if (verticalInput < 0 && rect.localPosition.y == -16)
        {
            rect.DOLocalMoveY(-32, 0.01f);
        }
        if (verticalInput < 0 && rect.localPosition.y == -32)
        {
            rect.DOLocalMoveY(-48, 0.01f);
        }
        if (verticalInput < 0 && rect.localPosition.y == -48)
        {
            rect.DOLocalMoveY(0, 0.01f);
        }
        if (verticalInput > 0 && rect.localPosition.y == 0)
        {
            rect.DOLocalMoveY(-48, 0.01f);
        }
        if (verticalInput > 0 && rect.localPosition.y == -16)
        {
            rect.DOLocalMoveY(-0, 0.01f);
        }
        if (verticalInput > 0 && rect.localPosition.y == -32)
        {
            rect.DOLocalMoveY(-16, 0.01f);
        }
        if (verticalInput > 0 && rect.localPosition.y == -48)
        {
            rect.DOLocalMoveY(-32, 0.01f);
        }
    }

    public void DetectPlayerStatsInStore()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 400, LayerMask.GetMask("Exit"));

        if (hit.collider != null && Input.GetKeyDown(KeyCode.C))
        {
            rect.DOLocalMoveY(0, 0.01f);
            storeBox.SetActive(false);
        }

    }
}
