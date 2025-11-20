using UnityEngine;

public class WeaponItem : MonoBehaviour
{

    public BaseWeaponControl mainWeapon;
    bool canPickUp;

    void Start()
    {
        
    }

    void Update()
    {
        if(canPickUp && PlayerInput.instance.getPickUp)
        {
            if(mainWeapon == null)
            {
                mainWeapon = gameObject.GetComponent<BaseWeaponControl>();
            } 
            MyInventory.instance.AddWeapon(mainWeapon);
            this.enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            canPickUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            canPickUp = false;
        }
    }
}
