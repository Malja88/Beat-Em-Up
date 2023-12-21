using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineTransitionController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] private CinemachineVirtualCamera enemyWaveCamera;
    [SerializeField] private float transitionDuration = 2f;

    private void Start()
    {
        //StartCoroutine(TransitionToPlayerFollow());
        playerFollowCamera.Priority = 0;

        // Enable the enemy-wave camera
        enemyWaveCamera.Priority = 10;
    }

    private IEnumerator TransitionToPlayerFollow()
    {
        // Disable the player-follow camera initially
        playerFollowCamera.Priority = 0;

        // Enable the enemy-wave camera
        enemyWaveCamera.Priority = 10;

        // Wait for the transition duration
        yield return new WaitForSeconds(transitionDuration);

        // Enable the player-follow camera and disable the enemy-wave camera
        playerFollowCamera.Priority = 10;
        enemyWaveCamera.Priority = 0;
    }
}
