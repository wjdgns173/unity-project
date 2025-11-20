using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInventory : MonoBehaviour
{   


    public List<BaseWeaponControl> myInventory = new List<BaseWeaponControl>(4)
    {
        
    };

    public static MyInventory instance;

    public int currentSlot = 0;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuikSlot(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            foreach (var item in myInventory) 
            {
                item.weaponObj.SetActive(false);
            }

            currentSlot = Mathf.Clamp((int)context.ReadValue<float>(), 0, myInventory.Count - 1); 

            myInventory[currentSlot].weaponObj.SetActive(true);
            
            WeaponCon.instance.thisWeapon = myInventory[currentSlot].myWeaponScript;

            

        }


    }

    public void AddWeapon(BaseWeaponControl weapon)
    {
        
        if(weapon == null)
        {
            Debug.LogWarning("MI_AddWeapon에서 weapon이 읎다 ");
            return;
        }

        Vector3 localScale = weapon.weaponObj.transform.localScale;

        if (myInventory.Count < 4)
        {
            weapon.gameObject.transform.SetParent(PlayerCon.instance.GunPosition.transform);

            Debug.Log("줍줍");

            myInventory.Add(weapon);
            currentSlot = myInventory.Count - 1;

        }
        else //임시 아이템 교체
        {

            GameObject FallItem = Instantiate
            (
                myInventory[currentSlot].gameObject,
                weapon.transform.position,
                Quaternion.identity
            );

            FallItem.gameObject.GetComponentInChildren<GunWeapon>().TestFire.SetActive(false);
            FallItem.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            FallItem.gameObject.GetComponent<WeaponItem>().enabled = true;
            


            Destroy(myInventory[currentSlot].gameObject);
            myInventory.RemoveAt(currentSlot);


            myInventory.Add(weapon); 

            weapon.transform.parent = PlayerCon.instance.GunPosition.transform;
            weapon.myWeaponScript.myWeaponCon.fireWait = false;

            Debug.Log("줍줍");
            currentSlot = myInventory.Count - 1;
        }

        WeaponCon.instance.thisWeapon = weapon.myWeaponScript;

        weapon.transform.localScale    = localScale;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        foreach (var item in myInventory)
        {
            item.weaponObj.SetActive(item == myInventory[currentSlot]);
        }

    }



}
