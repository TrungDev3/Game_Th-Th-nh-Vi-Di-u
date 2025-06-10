using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public Transform target; // Mục tiêu hiện tại của tháp

    [Header("Attributes")]
    public float range = 15f; // Tầm bắn 
    public float fireRate = 1f; // Tốc độ bắn (1 viên/giây) 
    private float fireCountdown = 0f;
    public int damage = 25; // Sát thương mỗi phát bắn 

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy"; // Tag của kẻ địch để tìm kiếm
    public Transform partToRotate; // Phần của tháp sẽ xoay theo địch (ví dụ: nòng súng)
    public GameObject pointSpawnBullet;
    public GameObject Bullet;
    public float speedBullet = 25f;
    public float turnSpeed = 10f;

    

    void Start()
    {
        // Gọi hàm tìm mục tiêu 2 lần mỗi giây thay vì mỗi frame để tối ưu
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Awake()
    {
        //pointSpawnBullet = transform.parent.Find("PointSpawnBullet");
        //// Kiểm tra để chắc chắn rằng đã tìm thấy
        //if (pointSpawnBullet == null)
        //{
        //    Debug.LogError("Không tìm thấy đối tượng con 'pointSpawnObject' trên tháp!", this.gameObject);
        //}
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

   
    void Update()
    {
       

        if (target == null)
        {
            // Tùy chọn: bạn có thể thêm logic để tháp không xoay khi không có mục tiêu
            return;
        }

        // --- Xoay tháp --- (Phần này có thể cần chỉnh sửa cho 2D chuẩn hơn, nhưng tạm để vậy)
        Vector3 dir = target.position - transform.position;
        // Quaternion lookRotation = Quaternion.LookRotation(dir); // Không lý tưởng cho 2D
        // Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        // partToRotate.rotation = Quaternion.Euler(0f, 0f, rotation.z);
        // Thay bằng cách xoay 2D chuẩn:
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward); // -90f để bù trừ hướng của sprite
        partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed);


        // --- Bắn ---
        if (fireCountdown <= 0f)
        {
            Shoot(); // Gọi hàm bắn riêng cho gọn
            fireCountdown = 1f / fireRate; // Đặt lại đồng hồ SAU KHI BẮN
        }

        // Luôn luôn đếm ngược đồng hồ
        fireCountdown -= Time.deltaTime;
    }

    // Tạo hàm Shoot riêng để code sạch sẽ hơn
    void Shoot()
    {
        GameObject bulletGO = Instantiate(Bullet, pointSpawnBullet.transform.position, Quaternion.identity);
        bulletGO.GetComponent<Bullet>().SetDirection(target.position - pointSpawnBullet.transform.position);

        // Gán mục tiêu cho viên đạn (nếu viên đạn có script để tự tìm đường)
        // Hoặc tính toán hướng và vận tốc trực tiếp tại đây
        Rigidbody2D rb = bulletGO.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            
            Vector2 direction = (target.transform.position - pointSpawnBullet.transform.position).normalized;
            rb.velocity = direction * speedBullet;
            bulletGO.transform.right = direction;
            Debug.Log("tỚI DÒNG 96");
            Debug.Log("Speed: " + speedBullet);
        }
    }


    // Vẽ tầm bắn trong Editor để dễ hình dung
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
