using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float m_maxHealth = 100.0f;
	public Slider m_healthBar;
	public Text m_healthText;

	private float m_currentHealth;

	void Start() {
		m_currentHealth = m_maxHealth;
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
		if(m_currentHealth >= m_maxHealth) {
			m_currentHealth = m_maxHealth;
		}
		UpdateHUD();
	}

	void Dead() {
		Debug.Log("you ded");
		Destroy(this.gameObject);
	}

	float CalculateHealth() {
		return m_currentHealth / m_maxHealth;
	}

	void UpdateHUD() {
		if(m_healthBar != null && m_healthText != null) {
			m_healthBar.value = CalculateHealth();
			m_healthText.text = m_currentHealth + "/" + m_maxHealth;
		}
	}
}
