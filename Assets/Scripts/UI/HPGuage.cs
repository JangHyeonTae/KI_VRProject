using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGuage : MonoBehaviour
{
    [SerializeField] private Image playerHpImage;
    [SerializeField] private Image EnemyHpImage;

    public void SetPlayerHp(float amount)
    {
        playerHpImage.fillAmount = amount;
    }

    public void SetEnemyHp(float amount)
    {
        EnemyHpImage.fillAmount = amount;
    }
}
