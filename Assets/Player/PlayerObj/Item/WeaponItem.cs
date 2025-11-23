using UnityEngine;

public class WeaponItem : MonoBehaviour
{

    public BaseWeaponControl mainWeapon;
    bool canPickUp;

    void Awake()
    {
        mainWeapon.isReloading = false;
        mainWeapon.myWeaponScript.TestFire.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            MyInventory.instance.newWeapon = mainWeapon;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            MyInventory.instance.newWeapon = mainWeapon;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            MyInventory.instance.newWeapon = null;
        }
    }
}
