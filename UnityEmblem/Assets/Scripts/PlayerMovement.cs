using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : Movement
{

	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkSound2;
	public AudioClip gameOverSound;

	public int wallDmg = 1;

	public Text foodText;

	public int pointsPerFood = 10;

	public int pointsPerSoda = 20;

	public float restartDelay = 1f;

	private Animator animator;

	private int food;
	protected override void Start()
	{
		animator = GetComponent<Animator>();
		food = Manager.instance.playerFood;
		foodText.text = "Food: " + food;
		base.Start();

	}

	private void Update()
	{
		if (!Manager.instance.playerTurn) return;

		int horizontal = 0;
		int vertical = 0;


		horizontal = (int)(Input.GetAxisRaw("Horizontal"));

		vertical = (int)(Input.GetAxisRaw("Vertical"));

		if (horizontal != 0)
		{
			vertical = 0;
		}

		if (horizontal != 0 || vertical != 0)
		{
			AttemptMove<Wall>(horizontal, vertical);
		}
	}

	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		food--;
		foodText.text = "Food: " + food;

		base.AttemptMove<T>(xDir, yDir);

		RaycastHit2D hit;
		if (Move(xDir, yDir, out hit))
		{
			SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
		}


		CheckIfGameOver();

		Manager.instance.playerTurn = false;
	}

	protected override void OnCantMove<T>(T component)
	{
		Wall hitWall = component as Wall;

		hitWall.DamageWall(wallDmg);

		animator.SetTrigger("PlayerChop");
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Exit")
		{
			Invoke("Restart", restartDelay);
			Manager.instance.playerFood = food;
			enabled = false;
		}

		else if (other.tag == "Food")
		{
			foodText.text = "Food: " + food + " + " + pointsPerFood;
			food += pointsPerFood;
			SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);

			other.gameObject.SetActive(false);
		}

		else if (other.tag == "Soda")
		{
			foodText.text = "Food: " + food + " + " + pointsPerSoda;
			food += pointsPerSoda;
			SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
			other.gameObject.SetActive(false);
		}
	}

	private void Restart()
	{
		SceneManager.LoadScene("Main");
	}

	public void LoseFood(int loss)
	{
		animator.SetTrigger("PlayerHit");
		foodText.text = "Food: " + food + " - " + loss;
		food -= loss;


		CheckIfGameOver();
	}

	private void onDisable()
	{
		Manager.instance.playerFood = food;

	}

	private void CheckIfGameOver()
	{
		if (food <= 0)
		{
			SoundManager.instance.PlaySingle(gameOverSound);
			SoundManager.instance.musicSource.Stop();
			Manager.instance.GameOver();
		}
	}

}
