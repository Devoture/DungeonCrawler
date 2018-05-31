using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float maxHealth = 100.0f;
	public Slider healthBar;
	public Text healthText;

	private float m_currentHealth;

	void Start() {
		m_currentHealth = maxHealth;
	}

	public void TakeDamage(float amount) {
		m_currentHealth -= amount;
		if(m_currentHealth <= 0) {
			m_currentHealth = 0;
			Dead();
		}
		UpdateHUD();
		Debug.Log(m_currentHealth);
	}

	public void Heal(float amount) {
		m_currentHealth += amount;
		if(m_currentHealth >= maxHealth) {
			m_currentHealth = maxHealth;
		}
		UpdateHUD();
	}

	void Dead() {
		Debug.Log("you ded");
		Destroy(this.gameObject);
	}

	float CalculateHealth() {
		return m_currentHealth / maxHealth;
	}

	void UpdateHUD() {
		if(healthBar != null && healthText != null) {
			healthBar.value = CalculateHealth();
			healthText.text = m_currentHealth + "/" + maxHealth;
		}
	}
}
