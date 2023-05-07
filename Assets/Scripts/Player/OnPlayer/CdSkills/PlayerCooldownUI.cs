using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerCooldownUI : MonoBehaviour
{
    [SerializeField] private Sprite _skillSprite;

    private Image _skill;
    public Sprite Init(Image skill)
    {
        _skill = skill;
        return _skillSprite;
    }

    public void StartCooldownUI(float cooldown)
    {
        StartCoroutine(CooldownRoutine(cooldown));
    }

    private IEnumerator CooldownRoutine(float cooldown)
    {
        float currentTimeFill = 0;

        while(currentTimeFill < cooldown)
        {
        currentTimeFill += Time.deltaTime;
        _skill.fillAmount = currentTimeFill / cooldown;
        Mathf.Clamp(_skill.fillAmount, 0, 1);
            yield return null;
        }
    }
}
