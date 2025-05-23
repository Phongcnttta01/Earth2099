using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject preHeal ;
    [SerializeField] private float healTime = 0.15f;
    [SerializeField] private GameManager gm;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gm.PauseGame();
        }
    }

    void MovePlayer()
    {
        // Nếu nhấn sang trái là -1 và nếu bấm phải là 1 còn ko bấm là 0 
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        rb.linearVelocity = playerInput.normalized * moveSpeed;
        if(playerInput.x <0) sr.flipX = true;
        else if (playerInput.x > 0) sr.flipX = false;
        
        if(playerInput.x != 0 || playerInput.y !=0) anim.SetBool("IsRun",true);
        else anim.SetBool("IsRun", false);
    }

    public void TakeDamage(float damage)
    { 
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        UpdateHpBar();
        
        if(currentHp <= 0) Die();
    }
    private void Die()
    {
        gm.GameOver();
    }
    
    private void UpdateHpBar()
    {
        if(hpBar != null) 
            hpBar.fillAmount = currentHp / maxHp;
    }

    public void Heal(float heal)
    {
        if (currentHp < maxHp)
        {
            currentHp += heal;
            currentHp = Mathf.Min(currentHp, maxHp);
            UpdateHpBar();
            
        }
        HealEffect();
    }

    public void HealEffect()
    {
        GameObject effect = Instantiate(preHeal, transform.position, Quaternion.identity);
        Destroy(effect,healTime);
    }
    
    
}
