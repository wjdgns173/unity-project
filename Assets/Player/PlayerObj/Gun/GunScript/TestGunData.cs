using UnityEngine;

[CreateAssetMenu(fileName = "TestGunData", menuName = "WeaponData")]
public class TestGunData : ScriptableObject
{
    

    [SerializeField] private float GunAttackSpeed;
    public float gunAttackSpeed
    {
        get { return GunAttackSpeed; }
        set { gunAttackSpeed = value; }
    }

    [SerializeField] private float AttackDamage;
    public float attackDamage
    {
        get { return AttackDamage; }
        set { attackDamage = value; }
    }

    [SerializeField] private float BulletCount;
    public float bulletCount
    {
        get { return BulletCount; }
        set { bulletCount = value; }
    }

    [SerializeField] private float BulletBounce;
    public float bulletBounce
    {
        get { return BulletBounce; }
        set { bulletBounce = value; }
    }

    [SerializeField] private float BulletForce;
    public float bulletForce
    {
        get { return BulletForce; }
        set { bulletForce = value; }
    }

    [SerializeField] private float BulletDistance;
    public float bulletDistance
    {
        get { return BulletDistance; }
        set { bulletDistance = value; }
    }

    [Tooltip("총의 최대 보관 총알")]
    [SerializeField] private int MaxBullet;
    public int maxBullet
    {
        get { return MaxBullet; }
        set { maxBullet = value; }
    }

    [Tooltip("한 탄창당 최대 총알")]
    [SerializeField] private int MaxAmmo;
    public int maxAmmo
    {
        get { return MaxAmmo; }
        set { maxAmmo = value; }
    }

    [SerializeField] private float ReloadTime;
    public float reloadTIme
    {
        get { return ReloadTime; }
        set { reloadTIme = value; }
    }

    [SerializeField] private float GlobalShakeForce;
    public float globalShakeForce
    {
        get { return GlobalShakeForce; }
        set { globalShakeForce = value; }
    }

    public string shootAnimeName;
    public string reloadAnimeName;


    //ammo bullet 안정성 낮음 수정 요망
    public int currentBullet;
    public int currentAmmo;

    public bool canShoot = true;
    public bool cnaReload = true;
    public bool isReloading;
    public bool isShooting;



    public bool IamShootGun;

}
