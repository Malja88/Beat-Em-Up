using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public string waveName;
    public List<EnemyAI> regularEnemy = new();
    public List<StrongEnemyAi> strongEnemy = new();
}
