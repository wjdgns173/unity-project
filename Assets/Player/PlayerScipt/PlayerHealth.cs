using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public  float playerMaxHealth;
    private float _playerCurrentHealth;
    public  float playerCurrentHealth
    {
        get => _playerCurrentHealth;
        set
        {
            _playerCurrentHealth = Mathf.Clamp(value, 0, playerMaxHealth);
            //PlayerCon.instance.ShowHealth();
        }
    }

    [SerializeField] Material flashBang;
    [SerializeField] GameObject dieEffect;
    Material original;
    SpriteRenderer sp;
    bool isFlashing = false;

    [SerializeField] TextMeshProUGUI hpUI;

    [SerializeField] SpriteRenderer mySprite;

    private void Start()
    {
        sp = mySprite.GetComponent<SpriteRenderer>();
        playerCurrentHealth = playerMaxHealth;
        original = sp.material;
    }

     void Update()
    {
        HpUI();
    }

    public void HpUI() //임시임 
    {
        hpUI.text = 
            _playerCurrentHealth
            + " / " + 
            playerMaxHealth;
    }


    public void Heal(float amount)
    {
        playerCurrentHealth = Mathf.Min(playerCurrentHealth + amount,playerMaxHealth);
        //Mathf.Min(playerCurrentHealth += amount, playerMaxHealth);
    }

    public void Damage(float damageAmount)
    {
        Debug.Log("맞았다");
        //StartCoroutine(apayo());

        playerCurrentHealth -= damageAmount;
        Debug.Log(playerCurrentHealth);
        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(dieEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    
}
