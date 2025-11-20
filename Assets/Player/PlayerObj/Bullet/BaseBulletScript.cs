using System.Collections;
using UnityEngine;

public class BaseBulletScript : MonoBehaviour
{
    public Rigidbody2D rb;

    Vector2 startPosition;

    public float bulletForce = 0;
    public float delDistance = 10;
    public float bulletDamage = 0;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        

    }

    public void bulletAllSetting(float force, float distance, float damage)
    {
        bulletForce  = force;
        delDistance  = distance;
        bulletDamage = damage;
    }

    public void BulletForce(float force)
    {
        bulletForce = force;
    }

    public void BulletDistance(float distance)
    {
        delDistance = distance;
    }

    public void BulletDamage(float damage)
    {
        bulletDamage = damage;
    }

    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    protected virtual void Shoooooowng() //슈우우우우우웅
    {
        rb.AddForce
           (
                transform.right * bulletForce,
                ForceMode2D.Impulse
           );
    }

    protected virtual IEnumerator DelBullet()
    {
        while (true)
        {
            float currentDistance = Vector2.Distance(startPosition, transform.position);
            if (currentDistance >= delDistance)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject != null)
        {
            if(collision.gameObject.CompareTag("enemy"))
            {
                MonsterBase monster = collision.gameObject.GetComponent<MonsterBase>();
                monster.Damage(bulletDamage);
            }
        }
        Destroy(gameObject);
    }



}
