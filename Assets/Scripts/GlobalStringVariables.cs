using UnityEngine;

public class GlobalStringVariables
{
    #region Animation Hashes
    public int PunchHash = Animator.StringToHash("Player Punch");
    public int PunchFinisherHash = Animator.StringToHash("Punch End Blow");
    public int RunHash = Animator.StringToHash("PlayerRun");
    public int HurtHash = Animator.StringToHash("Player Hurt");
    public int ItemHitHash = Animator.StringToHash("ItemHit");
    public int IdleWithWeapon = Animator.StringToHash("PlayerIdleWithWeapon");
    public int KickHash = Animator.StringToHash("Player Kick");
    public int JumpHash = Animator.StringToHash("Player Jump");
    public int KickFinisherHash = Animator.StringToHash("Kick End Blow");
    #endregion

    #region Input Hashes
    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";
    public string Jump = "Jump";
    public string Punch = "Fire1";
    public string Kick = "Fire2";
    #endregion
}
