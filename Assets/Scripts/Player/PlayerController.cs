using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Photon.Pun;

[RequireComponent(typeof(CharacterStats))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    public GameObject InventoryUI;
    private CharacterStats myStats;
    private int attackCount = 0;
    private PhotonView view;


    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float dashSpeed;
    [SerializeField] private int maxDashes;
    [SerializeField] private Image[] dashImages;
    [SerializeField] private Sprite[] dashIcon;
    private int currentDashes;
    private bool canDoubleJump;
    private bool canDash = true;

    [Header("Combat")]
    [SerializeField] private Animator armAnim;
    [SerializeField] private Transform armPos;
    [SerializeField] private Transform aimDirection;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    private Vector3 mousePos;

    [Header("Health")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private Image[] healthImages;
    [SerializeField] private Sprite[] healthSprites;

    [Header("Ground Check")]
    [SerializeField] private Vector2 bottomOffset;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask groundLayer;

    private float moveInput;
    private bool jumpInput;
    private bool isGrounded;
    private bool isDashing;
    private bool isFacingRight;

    private InputManager inputManager;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        inputManager = GetComponent<InputManager>();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    void Start()
    {
        view = GetComponent<PhotonView>();
        playerInputs.Movement.Jump.performed += _ => Jump();
        playerInputs.Movement.Shoot.performed += _ => Attack();
        playerInputs.Movement.Dash.started += _ => StartCoroutine(Dash());
        playerInputs.Movement.Interact.performed += _ => UseInventory();

        myStats = GetComponent<CharacterStats>();


        currentDashes = maxDashes;
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (view.IsMine)
        {
            Aim();
            SpriteFlip();
            Move();
            //UpdateDashUI();
            //jumpInput = inputManager.OnJumpPressed;

            isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);

            //if (EventSystem.current.IsPointerOverGameObject())
            //{
            //    return;
            //}

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !jumpInput)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            if (isGrounded)
            {
                currentDashes = maxDashes;
                canDash = true;
            }
            if (currentDashes <= 0)
            {
                canDash = false;
            }
        }
    }

    private void UseInventory()
    {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
    }

    private void Move()
    {
        moveInput = inputManager.MoveInput;

        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            if (moveInput != 0 && isGrounded)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
    }

    private void Jump()
    {
        anim.SetTrigger("Jump");
        if (isGrounded)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            canDoubleJump = false;
        }
    }

    IEnumerator Dash()
    {
        if (canDash)
        {
            rb.AddForce((mousePos - transform.position).normalized * dashSpeed, ForceMode2D.Impulse);
            isDashing = true;

            currentDashes--;

            yield return new WaitForSeconds(0.2f);

            isDashing = false;
            rb.velocity = rb.velocity / 4;
        }

    }

    private void Aim()
    {
        mousePos = playerInputs.Movement.MousePosition.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        Vector3 aimDir = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        aimDirection.eulerAngles = new Vector3(0, 0, angle);

        if (angle > 90 || angle < -90)
        {
            isFacingRight = false;
        }
        else
        {
            isFacingRight = true;
        }
    }

    private void SpriteFlip()
    {
        sr.flipX = !isFacingRight;
    }
    private float CalculateDamage()
    {
        float damageToDeal = (myStats.damage.GetValue());
        damageToDeal = CalculateCrit(damageToDeal);
        Debug.Log(damageToDeal);
        return damageToDeal;
    }

    private float CalculateCrit(float damage)
    {

        if (attackCount == (100 / myStats.CritChance.GetValue()))
        {
            float critDamage = (damage * myStats.CritDamageMultiplier.GetValue());
            Debug.Log("Crited for " + critDamage);
            attackCount = 0;
            return critDamage;
        }
        return damage;
    }
    private void Attack()
    {
        armAnim.SetTrigger("Attack");
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(armPos.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemy)
        {
            if (enemy.gameObject.CompareTag("MeleeEnemy"))
            {
                attackCount++;
                enemy.GetComponent<CharacterStats>().TakeDamage(CalculateDamage());
            }
            //if (enemy.gameObject.CompareTag("RangedEnemy"))
            //{
            //    enemy.GetComponent<CharacterStats>().TakeDamage(CalculateDamage());
            //}
            //if (enemy.gameObject.CompareTag("GhostEnemy"))
            //{
            //    enemy.GetComponent<CharacterStats>().TakeDamage(CalculateDamage());
            //}
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("GetHit");
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        int tempHealth = currentHealth;
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (tempHealth - 3 >= 0)
            {
                healthImages[i].sprite = healthSprites[3];
                tempHealth -= 3;
                continue;
            }
            if (tempHealth - 2 >= 0)
            {
                healthImages[i].sprite = healthSprites[2];
                tempHealth -= 2;
                continue;
            }
            if (tempHealth - 1 >= 0)
            {
                healthImages[i].sprite = healthSprites[1];
                tempHealth -= 1;
                continue;
            }
            if (tempHealth == 0)
            {
                healthImages[i].sprite = healthSprites[0];
                continue;
            }
        }
    }

    private void UpdateDashUI()
    {
        int tempDashcount = currentDashes;
        if (tempDashcount == 0)
        {
            dashImages[0].sprite = dashIcon[0];
        }
        if (tempDashcount == 1)
        {
            dashImages[1].sprite = dashIcon[0];
        }
        for (int i = 0; i < dashImages.Length; i++)
        {
            if (tempDashcount == 2)
            {
                dashImages[i].sprite = dashIcon[1];
            }
        }
    }


    private void Die()
    {
        Debug.Log("die");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var positions = new Vector2[] { bottomOffset };
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere(armPos.position, attackRange);
    }
}
