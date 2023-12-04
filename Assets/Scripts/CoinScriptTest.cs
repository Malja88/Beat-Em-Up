using System.Collections;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CoinScriptTest : MonoBehaviour
{
    [Header("Coin Distance Travel Variables")]
    [SerializeField] private int randomXStart;
    [SerializeField] private int randomXEnd;
    [SerializeField] private int randomYStart;
    [SerializeField] private int randomYEnd;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float wanderTime;

    [Header("Coin Bounceness Settings")]
    [SerializeField] private Rigidbody2D coinRb;
    [SerializeField] private float bouncePower;
    [SerializeField] private int randomStart;
    [SerializeField] private int randomEnd;

    [SerializeField] private BoxCollider2D shadowCollider;
    [SerializeField] private CircleCollider2D coinCollider;
    [SerializeField] private Rigidbody2D shadowRb;
    private Vector3 targetPosition;
    private float timer;

    void Start()
    {
        shadowCollider.OnCollisionEnter2DAsObservable().Where(x => x.gameObject.CompareTag("Wall") || x.gameObject.CompareTag("Obstacle")).Subscribe(x => 
        { 
            smoothSpeed = 0;      
        });
        coinCollider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag("Player")).Subscribe(_ => { Destroy(gameObject); } );
        StartCoroutine(Wander());
        TurnCoinColliderOn();
    }

    IEnumerator Wander()
    {
        float randomX = Random.Range(randomXStart, randomXEnd);
        float randomY = Random.Range(randomYStart, randomYEnd);
        targetPosition = new Vector2(randomX, randomY);
        coinRb.AddForce(new Vector2(0, Random.Range(randomStart, randomEnd) * bouncePower), ForceMode2D.Impulse);
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
                transform.position = newPosition;
                timer += Time.deltaTime;

                if (timer > wanderTime || Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    smoothSpeed = 0;
                }

                yield return null;
            }       
    }

    private async void TurnCoinColliderOn()
    {
        if (this != null)
        {
            await Task.Delay(1500);
            if (coinCollider != null)
            {
                coinCollider.enabled = true;
            }
        }
    }
}
