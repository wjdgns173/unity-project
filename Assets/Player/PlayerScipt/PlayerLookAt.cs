using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookAt : MonoBehaviour
{
    private Camera mainCam;
    Vector3 mousePos;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        LookAtSprite();

    }
    
    public void LookAtSprite()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        mousePos = mainCam.ScreenToWorldPoint
                                (
                                    new Vector3
                                    (
                                        screenPos.x,
                                        screenPos.y,
                                        mainCam.nearClipPlane
                                    )
                                );
                                
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler( 0, 0, rotZ - 90);
    }
}
