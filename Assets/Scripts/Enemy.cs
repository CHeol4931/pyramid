using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public GameObject itemPrefab;  // 드롭할 아이템

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("적이 맞음! 현재 체력: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("적이 죽음!");
        DropItem();
        Destroy(gameObject);
    }

    void DropItem()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
            Debug.Log("아이템 드롭됨!");
        }
    }
}
