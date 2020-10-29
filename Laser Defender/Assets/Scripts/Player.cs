using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float MinX;
    private float MaxX;
    private float MinY;
    private float MaxY;
    private float PlayerWidth;
    private float PlayerHeight;
    [SerializeField] float PlayerSpeed = 10f;
    [SerializeField] GameObject LaserShoot;
    [SerializeField] float TimeBetweenBulletsOnContinuousShooting = 0.2f;
    [SerializeField] float SpeedOfLaserShoot = 20f;



    void Start()
    {
        SetUpPlayerSizes();
        SetUpMoveBounderies();
    }

    void Update()
    {
        Move();
        ShootWhileSpaceIsPressed();
    }


    private void ShootWhileSpaceIsPressed()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine("FireContinuously");
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine("FireContinuously");
        }
    }
    private IEnumerator FireContinuously()
    {
        while (true)
        {
            CreateLaserShoot();
            yield return new WaitForSeconds(TimeBetweenBulletsOnContinuousShooting);
        }
    }
    private void CreateLaserShoot()
    {
        float newLaserYPos = transform.position.y + (PlayerHeight / 2);
        GameObject laser = Instantiate(LaserShoot, new Vector3(transform.position.x, newLaserYPos, transform.position.z), Quaternion.identity);
        laser.AddComponent<Rigidbody2D>().velocity = new Vector2(0, SpeedOfLaserShoot);
    }
    private void SetUpPlayerSizes()
    {
        PlayerWidth = transform.localScale.x;
        PlayerHeight = transform.localScale.y;
    }
    private void SetUpMoveBounderies()
    {
        var camera = Camera.main;
        MinX = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + (PlayerWidth / 2);
        MaxX = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - (PlayerWidth / 2);
        MinY = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + (PlayerHeight / 2);
        MaxY = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - (PlayerHeight / 2);
    }
    private void Move()
    {
        transform.position = new Vector2(GetNewXPos(), GetNewYPos());
    }
    private float GetNewXPos()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * PlayerSpeed;
        var newXPos = transform.position.x + deltaX;
        return Mathf.Clamp(newXPos, MinX, MaxX);
    }
    private float GetNewYPos()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * PlayerSpeed;
        var newYPos = transform.position.y + deltaY;
        return Mathf.Clamp(newYPos, MinY, MaxY);
    }
}
