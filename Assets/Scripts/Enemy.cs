using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public GameObject itemPrefab;  // ����� ������

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("���� ����! ���� ü��: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("���� ����!");
        DropItem();
        Destroy(gameObject);
    }

    void DropItem()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
            Debug.Log("������ ��ӵ�!");
        }
    }
}
