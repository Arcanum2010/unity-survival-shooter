using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float flashSpeed = 5f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.3f);
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;

	private Animator anim;
	private AudioSource playerAudio;
	private PlayerMovement playerMovement;
	private bool isDead;
	private bool damaged;

	private void Awake() {
		anim = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<PlayerMovement>();

		currentHealth = startingHealth;
	}

	private void Update() {
		if (damaged)
			damageImage.color = flashColor;
		else
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

		damaged = false;
	}

	public void TakeDamage(int amount) {
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		playerAudio.Play();
		if (currentHealth <= 0 && !isDead)
			Death();
	}

	private void Death() {
		isDead = true;
		anim.SetTrigger("die");
		playerAudio.clip = deathClip;
		playerAudio.Play();

		playerMovement.enabled = false;
	}
}
