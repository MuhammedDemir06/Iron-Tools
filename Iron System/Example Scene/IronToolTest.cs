using UnityEngine;
using IronTools.Attributes;

public class IronToolTest : MonoBehaviour
{
    [ShowTitle("PLAYER SPEED", EditorColor.Green)]
    [SerializeField] private float playerSpeed;
    [Space(20)]
    [Section(EditorColor.Red)]
    public bool canAttack;
    [ShowIf("canAttack")]
    public int AttackPower;
    [ShowIf("canAttack")]
    public float AttackRange;
    [ShowIf("canAttack")]
    public string AttackAnimName;

    [ShowDivider(EditorColor.Red, "Attack Settings")]
    public int damage;

    [ShowDivider(EditorColor.Blue)]
    public float cooldown;
    public void CharacterAction(int test)
    {
        canAttack = !canAttack;
    }
}
