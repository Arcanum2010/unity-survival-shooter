using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float attackInterval = 0.5f;
	public int attackPower = 10;

	private Animator anim;
	private GameObject player;
	private PlayerHealth playerHealth;
	private bool playerInRange;
	private float timer;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		anim = GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject == player)
			playerInRange = true;
	}

	private void OnTriggerExit(Collider otherInfo) {
		if (otherInfo.gameObject == player)
			playerInRange = false;
	}

	private void Update() {
		timer += Time.deltaTime;

		if(timer >= attackInterval && playerInRange)
			Attack();

		if (playerHealth.currentHealth <= 0)
			anim.SetTrigger("playerDead");
	}

	private void Attack() {
		timer = 0f;

		if (playerHealth.currentHealth > 0)
			playerHealth.TakeDamage(attackPower);
	}
}
