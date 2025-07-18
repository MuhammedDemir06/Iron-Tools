using System.Collections.Generic;
using UnityEngine;

public class OdinTest : MonoBehaviour
{
    //Test
    [Header("Player Can Attack???")]
    [SerializeField] private bool canAttack;

    [ShowIf("canAttack")]
    public int AttackPower;
    [ShowIf("canAttack")]
    public float AttackRange;
    [ShowIf("canAttack")]
    public string AttackAnimName;
}
