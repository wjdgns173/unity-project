using System.Collections;
using UnityEngine;

public class TestBulletScript : MonoBehaviour
{

    Vector3 startPosition;
    [SerializeField] float delDistance;

    private void Start()
    {
        startPosition = transform.position;
        StartCoroutine(distanceDel());
    }


    public IEnumerator distanceDel()
    {
        while(true)
        {
            float currentDistance = Vector2.Distance
                                            (
                                                startPosition,
                                                transform.position
                                            );

            if (currentDistance >= delDistance) Destroy(gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != 8 || collision.gameObject.layer != 9)
        {
            Destroy(gameObject);
        }
    }
}
