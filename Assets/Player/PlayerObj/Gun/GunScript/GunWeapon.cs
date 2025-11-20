using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;




public abstract class GunWeapon : MonoBehaviour
{
    public BaseWeaponControl myWeaponCon;
    
    public SpriteRenderer mySprite;

    public bool changedShootgun; // "많으면 장떙" 아이템 획득시 true로 교체;

    public TestGunData gunData;
    public Transform   Tip;
    public Animator    anime;

    public GameObject TestFire;
    public GameObject bullet;
    public BaseBulletScript bulletStat;
    

    public int plusBullet = 0; //아이템 or 효과로 보너스 총알

    public CinemachineImpulseSource impulseSource;

    void Awake()
    {
        gunData  = Instantiate(gunData);
        anime    = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();

        impulseSource = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CinemachineImpulseSource>();
        Debug.Log(plusBullet);
    }

    void Start()
    {
        bulletStat.bulletAllSetting(gunData.bulletForce,gunData.bulletDistance,gunData.attackDamage);
    }

    //virtual
    public virtual void Using(float rotZ)
    {
        Shoot(rotZ);
        Debug.Log("Using 사용됨");
    }

    public virtual void Shoot(float rotZ)
    {
        if(changedShootgun)
        {
            MultiShoot(rotZ);
        }

        else
        {
            DefaultShoot(rotZ);
        }
        
        CameraShakeManager.instance.CameraShake(impulseSource, gunData.globalShakeForce);
        gunData.currentAmmo--;

    }

    /*public virtual void DefaultShoot(float rotZ)
    {
        float randomRot = Random.Range(-gunData.bulletBounce, gunData.bulletBounce);

        Quaternion bulletRotation = Quaternion.Euler(0, 0, (rotZ) + randomRot);  //탄튐 );

        var clone = Instantiate
                    (
                        bullet,          //총알
                        Tip.position,    //발사위치
                        bulletRotation   //각도
                    );



    }*/

    public virtual void DefaultShoot(float rotZ)
    {

        

        for (int i = 1; i <= gunData.bulletCount + plusBullet; i++)
        {
            bulletStat.BulletForce(Mathf.Round(Random.Range(gunData.bulletForce / 0.75f, gunData.bulletForce)));


            float randomRot = Random.Range(-gunData.bulletBounce, gunData.bulletBounce);

            Quaternion bulletRotation = Quaternion.Euler(0, 0, (rotZ) + randomRot);  //탄튐 );

            var clone = Instantiate
                        (
                            bullet,          //총알
                            Tip.position,    //발사위치
                            bulletRotation   //각도
                        );


        }
    }

    public virtual void MultiShoot(float rotZ)
    {


        for (int i = 1; i <= (gunData.bulletCount + plusBullet) + 5; i++)
        {

            bulletStat.BulletForce(Mathf.Round(Random.Range(gunData.bulletForce / 0.75f, gunData.bulletForce)));


            float randomRot = Random.Range(-gunData.bulletBounce, gunData.bulletBounce);

            Quaternion bulletRotation = Quaternion.Euler(0, 0, (rotZ) + randomRot);  //탄튐 );

            var clone = Instantiate
                        (
                            bullet,          //총알
                            Tip.position,    //발사위치
                            bulletRotation   //각도
                        );

        }
    }








}
