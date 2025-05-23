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
        health.Add(amount);
        yield return new WaitForSeconds(duration);
        health.Subtract(amount);
    }

    public IEnumerator AddStaminaForDuration(float amount, float duration)
    {
        stamina.Add(amount);
        yield return new WaitForSeconds(duration);
        stamina.Subtract(amount);
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
