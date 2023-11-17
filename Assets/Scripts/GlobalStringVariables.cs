using UnityEngine;

public class GlobalStringVariables
{
    #region Animation Hashes
    public int PunchHash = Animator.StringToHash("Punch");
    public int PunchFinisherHash = Animator.StringToHash("ComboEndBlow");
    public int RunHash = Animator.StringToHash("Run");
    public int HurtHash = Animator.StringToHash("PlayerHurt");
    #endregion

    #region Input Hashes
    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";
    public string Jump = "Jump";
    public string Punch = "Fire1";
    #endregion
}