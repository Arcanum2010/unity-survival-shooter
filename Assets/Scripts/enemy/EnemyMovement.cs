using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	private Transform player;
	private NavMeshAgent navigationAgent;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		navigationAgent = GetComponent<NavMeshAgent>();
	}
	
	private void Update() {
		navigationAgent.SetDestination(player.position);
	}
}
