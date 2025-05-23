using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        PlayerCondition condition = CharacterManager.Instance.player.condition;
        PlayerController controller = CharacterManager.Instance.player.controller;
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        if (data.type == ItemType.Consumable)
        {
            for (int i = 0; i < data.itemEffects.Length; i++)
            {
                ApplyEffect(data.itemEffects[i], condition, controller);
            }

            Destroy(gameObject);
        }
    }
    void ApplyEffect(ItemEffect effect, PlayerCondition condition, PlayerController controller)
    {
        bool isInstant = effect.applicationType == EffectApplicationType.Instant;

        switch (effect.statType)
        {
            case StatType.Health:
                if (isInstant)
                    condition.AddHealth(effect.value);
                else
                    condition.StartCoroutine(condition.AddHealthForDuration(effect.value, effect.duration));
                break;

            case StatType.Stamina:
                if (isInstant)
                    condition.AddStamina(effect.value);
                else
                    condition.StartCoroutine(condition.AddStaminaForDuration(effect.value, effect.duration));
                break;

            case StatType.Speed:
                if (isInstant)
                    controller.AddMoveSpeed(effect.value);
                else
                    controller.StartCoroutine(controller.AddMoveSpeedForDuration(effect.value, effect.duration));
                break;

            case StatType.Jump:
                if (isInstant)
                    controller.AddJumpPower(effect.value);
                else
                    controller.StartCoroutine(controller.AddJumpPowerForDuration(effect.value, effect.duration));
                break;
        }
    }

}
