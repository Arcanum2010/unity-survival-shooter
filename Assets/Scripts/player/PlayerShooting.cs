using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {


	public int attackPower = 20;
	public float attackInterval = 0.15f;
	public float range = 100f;


	private float timer;
	private Ray shootRay;
	private RaycastHit shootHit;
	private int shootableMask;
	private ParticleSystem gunParticle;
	private LineRenderer gunLine;
	private AudioSource gunAudio;
	private Light gunLight;
	float effectDisplayTime = 0.2f;


	private void Awake() {
		shootableMask = LayerMask.GetMask("Shootable");
		gunParticle = GetComponent<ParticleSystem>();
		gunLine = GetComponent<LineRenderer>();
		gunAudio = GetComponent<AudioSource>();
		gunLight = GetComponent<Light>();
	}

	private void Update() {
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timer >= attackInterval)
			Shoot();

		if (timer >= attackInterval * effectDisplayTime)
			DisableEffects();
	}

	public void DisableEffects() {
		gunLine.enabled = false;
		gunLight.enabled = false;
	}

	private void Shoot() {
		timer = 0f;

		gunAudio.Play();
		gunLight.enabled = true;

		gunParticle.Stop();
		gunParticle.Play();

		gunLine.enabled = true;
		gunLine.SetPosition(0, transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) {
			EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
			if (enemyHealth != null)
				enemyHealth.TakeDamage(attackPower, shootHit.point);

			gunLine.SetPosition(1, shootHit.point);
		} else {
			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
		}
	}

}
