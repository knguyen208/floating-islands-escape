using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public int bulletDamage = 1;

    private WeaponPickup weaponPickup;

    void Start()
    {
        weaponPickup = GetComponent<WeaponPickup>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && weaponPickup.HasAmmo() && weaponPickup.GetCurrentWeapon() == "Gun")
        {
            ShootBullet();
            weaponPickup.UseAmmo();
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

        AudioManager.Instance.PlaySound(AudioManager.Instance.shootSound);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(bulletDamage);
        }
    }
}