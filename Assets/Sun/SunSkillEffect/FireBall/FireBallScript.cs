using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    [SerializeField] float downSpeed = 25;

    // Update is called once per frame
    void Update()
    {
        float myY = transform.position.y;
        float downY = myY -= downSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, downY, transform.position.z);
    }
}
