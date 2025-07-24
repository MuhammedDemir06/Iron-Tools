using UnityEngine;
using IronTools.Attributes;

public class OdinTest : MonoBehaviour
{
    public bool canAttack;

    [ShowIf("canAttack")]
    public int AttackPower;
    [ShowIf("canAttack")]
    public float AttackRange;
    [ShowIf("canAttack")]
    public string AttackAnimName;


    [Button("","MyIcon",OnlyIcon = true)]
    public void CharacterAction(int test)
    {
        canAttack = !canAttack;
    }
}
