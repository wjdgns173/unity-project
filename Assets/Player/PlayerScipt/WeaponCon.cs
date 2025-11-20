
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class WeaponCon : MonoBehaviour
{
    static public WeaponCon instance;

    public GunWeapon thisWeapon;

    private Camera  mainCam;
    public  Vector3 mousePos;

    public  float rotZ = 0f;

    public TextMeshProUGUI CurrentAmmoText;

    public bool wantShoot;

    public int curSlot
    {
        get {return MyInventory.instance.currentSlot;}
    }
    public int lastSlot = 0;

    public Image gunImage;

    public Slider reloadingSlier;
    /* public TextMeshProUGUI GunNameText;
     public TextMeshProUGUI CurrentAmmoText;
    */

    void Awake()
    {
        instance = this;

        mainCam  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lastSlot = curSlot;
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();

        if(curSlot != lastSlot)
        {
            WeaponChange();
            lastSlot = curSlot;

            Sprite weaponSprite = thisWeapon.mySprite.sprite;
            gunImage.sprite     = weaponSprite;

            thisWeapon.myWeaponCon.ChangeWait();
        }

        if (thisWeapon != null)
        {
            if(wantShoot)
            {
                if (CanIShoot()) //CanIShoot = 나 쏴도 되냐고
                {
                    thisWeapon.Using(rotZ);
                    thisWeapon.myWeaponCon.FireWait();
                    thisWeapon.myWeaponCon.FireTwinkle();
                    thisWeapon.myWeaponCon.AnimePlay(thisWeapon.gunData.shootAnimeName);
                }
                

            }

            
        }

        if(thisWeapon != null)
        {
            CurrentAmmoText.text =
            $"{thisWeapon.gunData.currentAmmo}"
            + " / " +
            $"{thisWeapon.gunData.currentBullet}";
        }

        

    }

    public void ReladWeapon(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            thisWeapon.myWeaponCon.Reload();
        }
    } 
    
    public bool CanIShoot()
    {
        return (thisWeapon.myWeaponCon.canShoot());
    }

    public void LookAt()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        mousePos = 
        (
            mainCam.ScreenToWorldPoint
            (
                new Vector3
                (
                    screenPos.x,
                    screenPos.y,
                    mainCam.nearClipPlane
                )
            )
        );

        Vector3 rotation = mousePos - transform.position;

        rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
    }

    public void ShootInput(InputAction.CallbackContext context) 
    {

        if (context.started || context.performed)
        {
            wantShoot = true;
        }
        
        else if(context.canceled)
        {
            wantShoot = false;
        }
        
    }

    void WeaponChange()
    {
        GunWeapon newWeapon = MyInventory.instance.myInventory[curSlot].myWeaponScript;
        thisWeapon = newWeapon;
        
    }


}
