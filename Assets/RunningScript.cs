using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunningScript : MonoBehaviour
{
    private Controls controls;
    private float currentHorizontalSpeed;
    private bool isRunning;
    private float lastTapTime;
    private float doubleTapTimeThreshold = 0.5f;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Run.performed += OnRunPerformed;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        CharacterRun();
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        //if (Time.time - lastTapTime < doubleTapTimeThreshold)
        //{
        //    // Double-tap detected, start running
        //    currentHorizontalSpeed *= 2f; // Adjust the running speed
        //    isRunning = true;
        //}

        //lastTapTime = Time.time;
        Debug.Log("Rnu");
    }

    private void CharacterRun()
    {
        if (!isRunning)
        {
            // Player is not running, do nothing
            return;
        }

        // Your running logic here...

        // For example:
        // transform.Translate(Vector3.forward * currentHorizontalSpeed * Time.deltaTime);
    }
}
