using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	private Transform player;
	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;
	private NavMeshAgent navigationAgent;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
		navigationAgent = GetComponent<NavMeshAgent>();
	}
	
	private void Update() {
		if (enemyHealth.currentHealth > 0 &&  playerHealth.currentHealth > 0)
			navigationAgent.SetDestination(player.position);
		else
			navigationAgent.enabled = false;
	}
}
