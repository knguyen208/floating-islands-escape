using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    public GameObject gun;
    public TMP_Text ammoText;

    private int gunAmmo = 0;

    private string currentWeapon = "None";

    public string GetCurrentWeapon() => currentWeapon;
    public bool HasAmmo() => currentWeapon == "Gun" && gunAmmo > 0;

    public void UseAmmo()
    {
        if (currentWeapon == "Gun" && gunAmmo > 0)
        {
            gunAmmo--;
            UpdateAmmoUI();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            gunAmmo += 6;
            SwitchToWeapon("Gun");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("AmmoBox"))
        {
            gunAmmo += 6;
            Destroy(other.gameObject);
            UpdateAmmoUI();
        }
    }


    void SwitchToWeapon(string weaponName)
    {
        currentWeapon = weaponName;
        gun.SetActive(weaponName == "Gun");
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (currentWeapon == "Gun")
        {
            ammoText.text = "x" + gunAmmo;
        }
    }
}