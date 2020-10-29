using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLaserShreddeer : MonoBehaviour
{
    Camera Camera;
    [SerializeField] float AspectRatio = 9f / 16f;


    void Start()
    {
        Camera = Camera.main;
        transform.localScale =  new Vector3(Camera.main.orthographicSize * 2 * AspectRatio, 1,0);
        var middleTopCameraPos = Camera.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));
        //Padding the collider due to its scale.
        transform.position = new Vector3(middleTopCameraPos.x, middleTopCameraPos.y + (transform.localScale.y / 2), 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player Projectile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
