using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInventory : MonoBehaviour
{
    public static MyInventory instance;

    public BaseWeaponControl newWeapon;

    public List<BaseWeaponControl> myInventory = new List<BaseWeaponControl>(4)
    {
        
    };
    
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
            WeaponCon.instance.WeaponSpriteChange();


        }


    }


    public  void AddWeapon(InputAction.CallbackContext context)
    {   
        if(context.started)
        {
            if (newWeapon == null)
            {
                Debug.LogWarning("MI_AddWeapon에서 weapon이 읎다 ");
                return;
            }


            Vector3 localScale = newWeapon.weaponObj.transform.localScale;

            if (myInventory.Count < 4)
            {
                newWeapon.gameObject.transform.SetParent(PlayerCon.instance.GunPosition.transform);

                Debug.Log("줍줍");

                myInventory.Add(newWeapon);
                currentSlot = myInventory.Count - 1;

            }
            else //임시 아이템 교체
            {

                BaseWeaponControl oldWeapon = myInventory[currentSlot];

                GameObject FallItem = Instantiate
                (
                    oldWeapon.gameObject,
                    newWeapon.transform.position,
                    Quaternion.identity
                );

                FallItem.GetComponent<BoxCollider2D>().enabled = true;
                FallItem.GetComponent<WeaponItem>().enabled = true;

                Destroy(myInventory[currentSlot].gameObject);
                myInventory.RemoveAt(currentSlot);

                myInventory.Add(newWeapon);

                newWeapon.transform.parent = PlayerCon.instance.GunPosition.transform;
                newWeapon.myWeaponScript.myWeaponCon.fireWait = false;

                Debug.Log("줍줍");
                currentSlot = myInventory.Count - 1;
            }

            newWeapon.transform.localScale = localScale;
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;

            WeaponCon.instance.thisWeapon = newWeapon.myWeaponScript;
            WeaponCon.instance.WeaponSpriteChange();

            foreach (var item in myInventory)
            {
                item.weaponObj.SetActive(item == myInventory[currentSlot]);
            }
        }

    }



}
