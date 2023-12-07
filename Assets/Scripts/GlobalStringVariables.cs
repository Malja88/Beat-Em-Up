using System.Diagnostics.Contracts;
using UnityEngine;

public class GlobalStringVariables
{
    #region Animation Hashes
    public int Walk = Animator.StringToHash("PlayerWalk");
    public int WalkingWithWeapon = Animator.StringToHash("PlayerWeaponWalk");
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
    public int EnemyHeavyAttack = Animator.StringToHash("Enemy Heavy Attack");
    public int EnemyHurt = Animator.StringToHash("Enemy Hurt");
    public int EnemyKnockDown = Animator.StringToHash("Enemy KnockDown");
    public int EnemyGetUp = Animator.StringToHash("Enemy GetUp");
    public int EnemyDeath = Animator.StringToHash("Enemy Death");
    public int EnemyRun = Animator.StringToHash("EnemyRun");
    //public int StrongEnemyWalk = Animator.StringToHash("StrongEnemyWalk");
    //public int StrongEnemyAttack = Animator.StringToHash("StrongEnemyAttack"); 
    //public int strongEnemyHurt = Animator.StringToHash("Strong Enemy Hurt");
    //public int strongEnemyStandUp = Animator.StringToHash("Strong Enemy Stand Up");
    //public int strongEnemyKnockDown = Animator.StringToHash("Strong Enemy KnockDown");
    #endregion

    #region Input Hashes
    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";
    public string Jump = "Jump";
    public string Punch = "Fire1";
    public string Kick = "Fire2";
    #endregion
}
