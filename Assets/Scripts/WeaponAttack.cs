using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public bool canAttack = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 공격 버튼
        {
            canAttack = true;
            Invoke("ResetAttack", 0.2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canAttack && other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void ResetAttack()
    {
        canAttack = false;
    }
}
