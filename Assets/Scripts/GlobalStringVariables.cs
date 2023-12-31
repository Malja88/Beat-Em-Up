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
    public int KnockDownHash = Animator.StringToHash("Player KnockDown");
    public int ItemHitHash = Animator.StringToHash("PlayerWeaponHit");
    public int IdleWithWeapon = Animator.StringToHash("WeaponIdle");
    public int KickHash = Animator.StringToHash("Player Kick");
    public int JumpHash = Animator.StringToHash("Player Jump");
    public int JumpKick = Animator.StringToHash("Player Jump Kick");
    public int PickUpHash = Animator.StringToHash("Player PickUp");
    public int ThrowHash = Animator.StringToHash("Player Throw Item");
    public int JumpWithWeapon = Animator.StringToHash("Player Jump With Weapon");
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
    #endregion

    #region Input Hashes
    public string HorizontalAxis = "Horizontal";
    public string VerticalAxis = "Vertical";
    public string Jump = "Jump";
    public string Punch = "Fire1";
    public string Kick = "Fire2";
    #endregion

    #region Tags
    public string WeaponTag = "Weapon";
    public string KnockdownPunchTag = "KnockDownPunch";
    public string PunchTag = "Punch";
    public string WallTag = "Wall";
    public string ObstacleTag = "Obstacle";
    public string PlayerTag = "Player";
    public string BaseTag = "Base";
    public string EnemyTag = "Enemy";
    public string PipeTag = "Pipe";
    public string DamageTag = "Damage";
    public string PickObjectTag = "PickObject";
    public string CoinTag = "Coin";
    public string StoreTag = "Store";
    public string HeavyAttack = "HeavyAttack";
    #endregion
}
