using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float widthOfShootingRange = 1.2f;
    [SerializeField] float ProjectilesSpeed = 5f;
    [SerializeField] GameObject Projectile;
    [SerializeField] float TimeBetweenShoots = 0.01f;
    private Player Player;
    private Transform PlayerTransform;



    void Start()
    {
        Player = FindObjectOfType<Player>();
        PlayerTransform = Player.GetComponent<Transform>();
        StartCoroutine(CheckIfInShootingRangeAndShoot());
    }

    private IEnumerator CheckIfInShootingRangeAndShoot()
    {
        while (true)
        {
            if (IsInShootingRange())
            {
                Shoot();
            }
            yield return new WaitForSeconds(TimeBetweenShoots);
        }
    }

    private bool IsInShootingRange()
    {
        return Mathf.Abs(PlayerTransform.position.x - transform.position.x) < widthOfShootingRange;
    }

    private void Shoot()
    {
        float newXPosOfProjectile = transform.position.x;
        float newYPosOfProjectile = transform.position.y - (transform.localScale.y / 2);
        var shoot = Instantiate(Projectile, new Vector3(newXPosOfProjectile, newYPosOfProjectile, 0), Quaternion.identity);
        shoot.AddComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * ProjectilesSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttacingDamageDealer damageDealer = collision.gameObject.GetComponent<AttacingDamageDealer>();
        if (collision.gameObject.tag.Equals("Player Projectile"))
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(AttacingDamageDealer damageDealer)
    {
        if (damageDealer != null)
        {
            health -= damageDealer.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
