using UnityEngine;

public class GlobalStringVariables
{
    #region Animation Hashes
    public int PunchHash = Animator.StringToHash("Player Punch");
    public int PunchFinisherHash = Animator.StringToHash("Punch End Blow");
    public int RunHash = Animator.StringToHash("PlayerRun");
    public int HurtHash = Animator.StringToHash("Player Hurt");
    public int ItemHitHash = Animator.StringToHash("PlayerWeaponHit");
    public int IdleWithWeapon = Animator.StringToHash("WeaponIdle");
    public int KickHash = Animator.StringToHash("Player Kick");
    public int JumpHash = Animator.StringToHash("Player Jump");
    public int KickFinisherHash = Animator.StringToHash("Kick End Blow");
    public int RunWithWeapon = Animator.StringToHash("PlayerRunWithWeapon");
    public int EnemyWalk = Animator.StringToHash("EnemyWalk");
    public int EnemyAttack = Animator.StringToHash("EnemyAttack");
    public int EnemyHurt = Animator.StringToHash("Enemy Hurt");
    public int EnemyKnockDown = Animator.StringToHash("Enemy KnockDown");
    public int EnemyGetUp = Animator.StringToHash("Enemy GetUp");
    public int EnemyDeath = Animator.StringToHash("Enemy Death");
    #endregion

    #region Input Hashes
    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";
    public string Jump = "Jump";
    public string Punch = "Fire1";
    public string Kick = "Fire2";
    #endregion
}
