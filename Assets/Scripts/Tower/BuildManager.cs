using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;

        GameManager.instance.UpdateUI();
    }

    [Header("Các loại tháp")]
    public TowerBluePrint archerTowerBlueprint;
    public TowerBluePrint gunTowerBlueprint;


    [Header("Unity Setup")]
    public GameObject buildUIPanel; // Kéo Panel UI vào đây
    public GameObject PointToTurnOn;

    private TowerSlot selectedSlot; // Thay Node bằng TowerSlot

    // Hàm được gọi bởi TowerSlot khi nó được click
    public void SelectSlot(TowerSlot slot) // Thay Node bằng TowerSlot
    {
        if (selectedSlot == slot)
        {
            DeselectSlot();
            return;
        }

        selectedSlot = slot;
        buildUIPanel.SetActive(true);
        buildUIPanel.transform.position = PointToTurnOn.transform.position;
    }
    public void DeselectSlot()
    {
        selectedSlot = null;
        buildUIPanel.SetActive(false);
    }

    // Hàm chung để xử lý việc chọn và xây
    public void BuildSelectedTower(TowerBluePrint blueprint)
    {
        if (selectedSlot == null) return;

        if (GameManager.instance.playerGold < blueprint.cost)
        {
            Debug.Log("Không đủ tiền!");
            DeselectSlot();
            return;
        }

        GameManager.instance.playerGold -= blueprint.cost;
        GameManager.instance.UpdateUI();
        GameObject tower = Instantiate(blueprint.prefab, selectedSlot.transform.position, Quaternion.identity);
        selectedSlot.SetTower(tower);

        Debug.Log("Xây tháp thành công! Vàng còn lại: " + GameManager.instance.playerGold);
       // playerGold1 = GameManager.instance.playerGold;
        
        DeselectSlot();
    }

    // --- Các hàm Public để gọi từ Button trên UI ---
    public void SelectArcherTower()
    {
        BuildSelectedTower(archerTowerBlueprint);
    }

    public void SelectGunTower()
    {
        BuildSelectedTower(gunTowerBlueprint);
    }

    // Giả sử chúng ta có các ô để xây tháp, và mỗi ô có một script Node
    // Đây là một ví dụ đơn giản khi click chuột
   
}
