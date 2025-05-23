using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;
    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTackDamage;

    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        if (health.curValue == 0)
        {
            Die();
        }
    }

    public void AddHealth(float amount)
    {
        health.Add(amount);
    }
    public void AddStamina(float amount)
    {
        stamina.Add(amount);
    }
    public IEnumerator AddHealthForDuration(float amount, float duration)
    {
        yield return RegenerateOverTime(amount, duration, health.Add);
    }
    public IEnumerator AddStaminaForDuration(float amount, float duration)
    {
        yield return RegenerateOverTime(amount, duration, stamina.Add);
    }
    private IEnumerator RegenerateOverTime(float amountPerSecond, float duration, Action<float> applyAmount)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float delta = Time.deltaTime;
            float regenAmount = amountPerSecond * delta;

            applyAmount(regenAmount);

            elapsed += delta;
            yield return null;
        }
    }

    public void Die()
    {
        Debug.Log("Die");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTackDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}
