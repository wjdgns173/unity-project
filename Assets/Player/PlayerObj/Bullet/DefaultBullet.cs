using UnityEngine;

public class DefaultBullet : BaseBulletScript
{

    void OnEnable()
    {
        Shoooooowng();
        StartCoroutine(DelBullet());
    }

}
