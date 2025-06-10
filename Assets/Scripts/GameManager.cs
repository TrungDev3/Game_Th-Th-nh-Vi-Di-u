using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    // Dùng Singleton pattern để dễ dàng truy cập từ các script khác
    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene!");
            return;
        }
        instance = this;
    }


    [Header("Game Stats")]
    public int startGold = 100; // Vàng khởi đầu 
    public int playerGold;

    public int startBaseHealth = 20; // Máu căn cứ 
    public int baseHealth;

    [Header("UI")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI healthText;

    void Start()
    {
        playerGold = startGold;
        baseHealth = startBaseHealth;
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        UpdateUI();
    }

    public void SpendGold(int amount)
    {
        playerGold -= amount;
        UpdateUI();
    }

    public void EnemyReachedBase()
    {
        baseHealth--;
        UpdateUI();

        if (baseHealth <= 0)
        {
            LoseGame();
        }
    }

    public void UpdateUI()
    {
        goldText.text =  playerGold.ToString();
        healthText.text =  baseHealth.ToString();
    }

    void LoseGame()
    {
        // Hiện màn hình thua, tạm thời chỉ log ra console
        Debug.Log("GAME OVER!"); // Điều kiện thua khi máu căn cứ về 0 
        // Tạm dừng game
        Time.timeScale = 0f;
    }
}
