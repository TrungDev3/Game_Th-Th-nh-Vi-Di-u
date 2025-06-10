using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [Header("Thông số")]
    public int buildCost = 50; // Giá tiền để xây tháp trên ô này

    [Header("Feedback Trực Quan (Tùy chọn)")]
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;

    // Biến để lưu lại tháp đã được xây trên ô này
    private GameObject towerOnNode;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    // Hàm được gọi khi chuột click LÊN collider của GameObject này
    void OnMouseDown()
    {
        // 1. Kiểm tra xem ô này đã có tháp chưa
        if (towerOnNode != null)
        {
            Debug.Log("Vị trí này đã có tháp rồi!");
            return;
        }

        // 2. Gọi BuildManager để thử xây tháp và nhận lại kết quả
        BuildManager.instance.SelectSlot(this);

    }

    // Thêm hàm này để BuildManager có thể cập nhật lại Slot sau khi xây
    public void SetTower(GameObject tower)
    {
        this.towerOnNode = tower;
    }
    // Các hàm để thay đổi màu sắc khi di chuột vào/ra
    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }


    
}
