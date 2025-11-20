using System;
using System.Collections;
using UnityEngine;

public abstract class BaseWeaponControl : MonoBehaviour
{
    public GameObject weaponObj;
    public GunWeapon  myWeaponScript;

    public Animator anime;

    public bool fireWait;     //총 발사속도 제어
    public bool changeWait;   //무기 교체시 대기시간
    public bool isReloading;  //나는 장전중이다 


    public bool magazineFull => myWeaponScript.gunData.currentAmmo >= myWeaponScript.gunData.maxAmmo;
    public bool bulletEmpty  => myWeaponScript.gunData.currentBullet < 0;


    //읽기 전용 
    private (Func<bool> condition, string whyNotMassage)[] reloadCheck;

    void Awake()
    {
        anime = weaponObj.GetComponent<Animator>();

        reloadCheck = new (Func<bool>, string)[]
        {
            (() => magazineFull, "탄창이 이미 꽉 찼습니다"),
            (() => bulletEmpty, "총알이 부족합니다"),
            (() => isReloading, "이미 장전 중 입니다")
        };
    }

     



    public bool canShoot()
    {
        return (
                    myWeaponScript.gunData.currentAmmo > 0 &&
                    !isReloading && 
                    !fireWait &&
                    !changeWait
                );
    }

    
 

    #region "재장전"
    public virtual void Reload()
    {
        /*if(canReload())
        {
            StartCoroutine(Reloading());
        }*/

        foreach(var _reloadCheck in reloadCheck)
        {
            if(_reloadCheck.condition())
            {
                Debug.LogWarning(_reloadCheck.whyNotMassage);
                return;
                
            }
        }
        
        StartCoroutine(Reloading());

    }

    public virtual IEnumerator Reloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(myWeaponScript.gunData.reloadTIme);

        int needAmmo   = myWeaponScript.gunData.maxAmmo - myWeaponScript.gunData.currentAmmo; //현재 탄창 크기 - 현재 총알 = 필요한 총알
        int reLoadAmmo = Mathf.Min(needAmmo, myWeaponScript.gunData.currentBullet); 

        myWeaponScript.gunData.currentBullet -= reLoadAmmo;
        myWeaponScript.gunData.currentAmmo   += reLoadAmmo;
        
        isReloading = false;
    }
    #endregion

    #region "총구 불꽃 제어"
    public virtual void FireTwinkle() //if로 제어 x
    {
        StartCoroutine(FireTwinkling());
    }

    IEnumerator FireTwinkling() 
    {
        myWeaponScript.TestFire.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        myWeaponScript.TestFire.SetActive(false);
    }
    #endregion

    #region "발사속도 제어"
    public virtual void FireWait() 
    {
        if(!fireWait)
        {
            StartCoroutine(FireWaiting());
        }
    }

    IEnumerator FireWaiting()
    {
        fireWait = true;
        yield return new WaitForSeconds(myWeaponScript.gunData.gunAttackSpeed);
        fireWait = false;
    }
    #endregion


    public void ChangeWait()
    {
        if(!changeWait)
        {
            StartCoroutine(ChangeWaiting());
        }
        
    }

    public IEnumerator ChangeWaiting()
    {
        changeWait = true;
        yield return new WaitForSeconds(0.1f);
        changeWait = false;
    }

    public void AnimePlay(string animeName)
    {
        anime.Play(animeName);
    }



}
