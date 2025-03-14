using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public RectTransform inventoryPanel; // Inventory Panel (UI에서 연결)
    public GameObject prefabSlot; // 슬롯 프리팹 (UI에서 연결)
    public List<GameObject> slots = new List<GameObject>();
    public List<Item> items = new List<Item>(); // 인벤토리 아이템 리스트
    public int maxSlotCount = 4; // 슬롯 개수 (4칸 고정)
    public Sprite emptySlotSprite;
    void Start()
    {
        SetupInventory();
    }


    void SetupInventory()
    {
        // 4개의 슬롯 생성
        for (int i = 0; i < maxSlotCount; i++)
        {
            GameObject slot = Instantiate(prefabSlot, inventoryPanel);
            slots.Add(slot);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            DropItem(); // X 키를 누르면 아이템 버리기
        }
    }

    public void DropItem()
    {
        if (items.Count > 0)
        {
            Debug.Log($"{items[items.Count - 1].HPP}을(를) 버렸습니다!");
            items.RemoveAt(items.Count - 1);
            UpdateInventoryUI(); // UI 업데이트 추가
        }
    }
    public void AddItem(Item newItem)
    {
        if (items.Count < maxSlotCount)
        {
            items.Add(newItem);
            UpdateInventoryUI(); // UI 업데이트 추가
            Debug.Log($"{newItem.HPP}을(를) 획득!");
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
        }
    }

    // UI를 업데이트하는 함수 추가
    void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image slotImage = slots[i].GetComponent<Image>();

            if (i < items.Count)
            {
                slotImage.sprite = items[i].HPSprite; // 아이템 이미지로 변경
            }
            else
            {
                slotImage.sprite = emptySlotSprite; // 빈 슬롯 이미지로 변경
            }
        }
    }
    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Player player = FindObjectOfType<Player>(); // 플레이어 찾기
            Item item = items[index];

            if (item.HPP == "체력 물약")
            {
                player.RestoreHP(20); // 체력 회복
            }
            else if (item.HPP == "이동속도 물약")
            {
                player.speed += 2f; // 이동 속도 증가
                StartCoroutine(ResetSpeed(player)); // 5초 후 원래 속도로 복구
            }

            Debug.Log($"{item.HPP} 사용!");
            items.RemoveAt(index); // 아이템 삭제
            UpdateInventoryUI(); //  UI 업데이트 추가
        }
    }

    // 이동 속도를 원래대로 복구하는 코루틴 추가
    private IEnumerator ResetSpeed(Player player)
    {
        yield return new WaitForSeconds(5f);
        player.speed -= 2f;
        Debug.Log("이동 속도가 원래대로 돌아왔습니다.");
    }
}