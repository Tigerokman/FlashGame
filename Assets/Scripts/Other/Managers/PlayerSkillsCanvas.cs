using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillsCanvas : MonoBehaviour
{
    [SerializeField] private Image _skill;

    public void SetSkill(PlayerCooldownUI playerCooldownUI)
    {
        _skill.sprite =  playerCooldownUI.Init(_skill);
    }
}
