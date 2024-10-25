using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerConditions : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action onTakeDamage;//�������� �޾������� �̺�Ʈ �׼� ���
    public event Action onUseStamina;//�޸��� ��� ���¹̳�

    private void Update()
    {
        //���׹̳� �ڵ� ���� ��
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (health.curValue < 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }
}