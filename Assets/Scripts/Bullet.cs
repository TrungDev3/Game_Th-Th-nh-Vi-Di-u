using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    public Tower tower1;
    protected int damage;
    private Vector2 moveDirection;
    private float speed = 10f;


    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;
    }
    private void Awake()
    {

        rb2D = GetComponent<Rigidbody2D>();
        tower1 = GetComponent<Tower>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("đã tới đây");
        // Kiểm tra xem có va chạm với đối tượng có tag là "Enemy" không
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Đạn đã trúng kẻ địch!");

            // Lấy script EnemyHealth từ kẻ địch đã va chạm
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.takeDamage(damage); // Giả sử có hàm takeDamage
            }

            // Phá hủy viên đạn sau khi va chạm
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }
}
