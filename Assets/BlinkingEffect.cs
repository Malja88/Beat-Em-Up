using UnityEngine;
using UnityEngine.UI;

public class BlinkingEffect : MonoBehaviour
{
    [SerializeField] public Image image;
    void Update()
    {
        GoSignRegulator();
    }

    public void GoSignRegulator()
    {
        if (Time.fixedTime % .5 < .2)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }
}
