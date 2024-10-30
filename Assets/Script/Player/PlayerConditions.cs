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

    public event Action onTakeDamage;//데미지를 받았을때의 이벤트 액션 등록

    private void LateUpdate()
    {
        //스테미나 자동 충전 값
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
        Debug.Log("플레이어가 죽었다.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }


}
