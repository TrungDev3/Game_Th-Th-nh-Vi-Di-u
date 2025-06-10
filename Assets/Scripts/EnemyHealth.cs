using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int goldReward = 50;

    private void Start()
    {
        currentHealth = maxHealth;

    }

    public void takeDamage(int damage)
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
       // EnemySpawner.instance.enemiesLeftInWave--;
        GameManager.instance.AddGold(goldReward); //gọi Manager thêm vàng
        EnemySpawner.instance.AnEnemyDied();
        Destroy(gameObject);
    }
}
