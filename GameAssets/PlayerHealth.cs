using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    public TMP_Text livesText;
    public TMP_Text loseText;

    void Start()
    {
        UpdateLivesUI();
        loseText.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        UpdateLivesUI();

        if (lives <= 0)
        {
            ShowLoseScreen();
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.playerHitSound);
    }

    public void Heal(int amount)
    {
        if (lives < 3)
        {
            lives += amount;
            if (lives > 3) lives = 3;
            UpdateLivesUI();
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "x" + lives;
        }
    }

    void ShowLoseScreen()
    {
        Debug.Log("Player has died!");
        loseText.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthKit"))
        {
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.lives < 3)
            {
                playerHealth.Heal(1);
                Destroy(other.gameObject);
            }
        }
    }
}