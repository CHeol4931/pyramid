using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSpeed = 5f;
    private float gravity;
    private CharacterController cc;
    private Animator animator;
    private Vector3 mov;
    private float mouseX;
    public bool isAttackCheck = false;

    // ���� ����
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayer;

    // �κ��丮 ����
    private int selectedItem = -1;
    public Image[] slotImages; // ���� UI �̹��� �迭
    public Sprite emptySlotSprite; // �� ���� �̹���
    public Dictionary<string, string> itemMessages = new Dictionary<string, string>
    {
        { "ü�� ����", "ü���� ȸ���մϴ�!" },
        { "�̵��ӵ� ����", "�̵� �ӵ��� �����մϴ�!" }
    };

    // HP �� ��� ����
    public float maxHP = 100f;
    public float currentHP;
    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenDepletionRate = 5f; // �ʴ� ���ҷ�
    public float oxygenDamage = 10f; // ��Ұ� 0�� �� HP ���ҷ�
    public Slider hpBar;
    public Slider oxygenBar;

    void Awake()
    {
        currentHP = maxHP;
        currentOxygen = maxOxygen;

        if (hpBar != null) hpBar.value = 1f;
        if (oxygenBar != null) oxygenBar.value = 1f;
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mov = Vector3.zero;
        gravity = 10f;

    }

    void Update()
    {
        MoveAndCameraRot();
        Attack();
        HandleOxygen();
    }

    void HandleOxygen()
    {
        if (currentOxygen > 0)
        {
            currentOxygen -= oxygenDepletionRate * Time.deltaTime;
        }
        else
        {
            TakeDamage(oxygenDamage * Time.deltaTime);
        }

        if (oxygenBar != null) oxygenBar.value = currentOxygen / maxOxygen;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (hpBar != null) hpBar.value = currentHP / maxHP;

        if (currentHP <= 0)
        {
            Debug.Log("�÷��̾� ���!");
        }
    }

    public void RestoreOxygen(float amount)
    {
        currentOxygen = Mathf.Min(currentOxygen + amount, maxOxygen);
        if (oxygenBar != null) oxygenBar.value = currentOxygen / maxOxygen;
    }

    public void RestoreHP(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        if (hpBar != null) hpBar.value = currentHP / maxHP;
    }

    public void MoveAndCameraRot()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        this.transform.localEulerAngles = new Vector3(0, mouseX, 0);

        if (cc.isGrounded)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            if (moveX != 0 || moveZ != 0)
            {
                mov = new Vector3(moveX, 0, moveZ);
                mov = cc.transform.TransformDirection(mov);
            }
            else
            {
                mov = Vector3.zero;
            }
        }
        else
        {
            mov.y -= gravity * Time.deltaTime;
        }
        cc.Move(mov * Time.deltaTime * speed);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("isAttack");
            isAttackCheck = true;

            if (attackPoint == null)
            {
                Debug.LogError("attackPoint�� �������� �ʾҽ��ϴ�!");
                return;
            }

            Collider[] hitEnemies = Physics.OverlapBox(attackPoint.position, new Vector3(attackRange, attackRange, attackRange), Quaternion.identity, enemyLayer);
            foreach (Collider enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            InventorySystem inventory = FindObjectOfType<InventorySystem>();

            if (inventory != null)
            {
                inventory.AddItem(item);
                Destroy(other.gameObject);
            }
        }
    }
}
