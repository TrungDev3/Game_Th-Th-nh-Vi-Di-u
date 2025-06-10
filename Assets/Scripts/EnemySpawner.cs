using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;


    public GameObject enemyPrefab; // Kéo Prefab của Enemy vào đây
    public Transform spawnPoint;

    [Header("Cài đặt Wave")]
    [SerializeField] private int enemiesInFirstWave = 3; // Số enemy ở wave đầu tiên
    [SerializeField] private float timeBetweenSpawns = 3f;

    // Biến nội bộ để quản lý
    private int currentEnemiesToSpawn;
    public int enemiesLeftInWave;
    public bool isWaveActive = false;

    private void Awake()
    {
        instance = this;

        // Khởi tạo số lượng enemy cho wave đầu tiên
        currentEnemiesToSpawn = enemiesInFirstWave;
      //  enemiesLeftInWave = currentEnemiesToSpawn;
    }


    private void Update()
    {

        // Ví dụ: Nhấn phím 'N' để bắt đầu wave mới nếu wave cũ đã kết thúc
        if (Input.GetKeyDown(KeyCode.Space) && !isWaveActive)
        {
            Debug.Log("Chạy dòng 37 rồi");
            StartCoroutine(SpawnWave());
        }
        // Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    // Coroutine để spawn một wave, có độ trễ giữa mỗi lần spawn
    IEnumerator SpawnWave()
    {
        isWaveActive = true;
       
            enemiesLeftInWave = currentEnemiesToSpawn; // Đặt bộ đếm số enemy còn lại
            Debug.Log("Bắt đầu Wave mới với " + currentEnemiesToSpawn + " enemies.");

            for (int i = 0; i < currentEnemiesToSpawn; i++)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                // Đợi 1 giây trước khi spawn con tiếp theo
                yield return new WaitForSeconds(timeBetweenSpawns);
            

            }
        
    }

    // Hàm này sẽ được gọi bởi script của Enemy khi nó chết
    public void AnEnemyDied()
    {

        enemiesLeftInWave--;
        Debug.Log("Một enemy đã chết! Còn lại: " + enemiesLeftInWave);

            // Kiểm tra nếu tất cả enemy trong wave đã chết
            if (enemiesLeftInWave <= 0)
            {
                isWaveActive = false;
                currentEnemiesToSpawn++; // Tăng số lượng cho wave tiếp theo
                Debug.LogWarning("Đã dọn sạch wave! Wave tiếp theo sẽ có " + currentEnemiesToSpawn + " enemies. Nhấn 'N' để bắt đầu.");
            }
        
    }
}
