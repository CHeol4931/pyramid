using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public RectTransform inventoryPanel; // Inventory Panel (UI���� ����)
    public GameObject prefabSlot; // ���� ������ (UI���� ����)
    public List<GameObject> slots = new List<GameObject>();
    public List<Item> items = new List<Item>(); // �κ��丮 ������ ����Ʈ
    public int maxSlotCount = 4; // ���� ���� (4ĭ ����)
    public Sprite emptySlotSprite;
    void Start()
    {
        SetupInventory();
    }


    void SetupInventory()
    {
        // 4���� ���� ����
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
            DropItem(); // X Ű�� ������ ������ ������
        }
    }

    public void DropItem()
    {
        if (items.Count > 0)
        {
            Debug.Log($"{items[items.Count - 1].HPP}��(��) ���Ƚ��ϴ�!");
            items.RemoveAt(items.Count - 1);
            UpdateInventoryUI(); // UI ������Ʈ �߰�
        }
    }
    public void AddItem(Item newItem)
    {
        if (items.Count < maxSlotCount)
        {
            items.Add(newItem);
            UpdateInventoryUI(); // UI ������Ʈ �߰�
            Debug.Log($"{newItem.HPP}��(��) ȹ��!");
        }
        else
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�!");
        }
    }

    // UI�� ������Ʈ�ϴ� �Լ� �߰�
    void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image slotImage = slots[i].GetComponent<Image>();

            if (i < items.Count)
            {
                slotImage.sprite = items[i].HPSprite; // ������ �̹����� ����
            }
            else
            {
                slotImage.sprite = emptySlotSprite; // �� ���� �̹����� ����
            }
        }
    }
    public void UseItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Player player = FindObjectOfType<Player>(); // �÷��̾� ã��
            Item item = items[index];

            if (item.HPP == "ü�� ����")
            {
                player.RestoreHP(20); // ü�� ȸ��
            }
            else if (item.HPP == "�̵��ӵ� ����")
            {
                player.speed += 2f; // �̵� �ӵ� ����
                StartCoroutine(ResetSpeed(player)); // 5�� �� ���� �ӵ��� ����
            }

            Debug.Log($"{item.HPP} ���!");
            items.RemoveAt(index); // ������ ����
            UpdateInventoryUI(); //  UI ������Ʈ �߰�
        }
    }

    // �̵� �ӵ��� ������� �����ϴ� �ڷ�ƾ �߰�
    private IEnumerator ResetSpeed(Player player)
    {
        yield return new WaitForSeconds(5f);
        player.speed -= 2f;
        Debug.Log("�̵� �ӵ��� ������� ���ƿԽ��ϴ�.");
    }
}