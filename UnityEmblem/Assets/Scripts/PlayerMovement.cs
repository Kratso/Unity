using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Movement
{
	public int wallDmg = 1;
	public int pointsPerFood = 10;

	public int pointsPerSoda = 20;

	public float restartDelay = 1f;

	private Animator animator;

	private int food;
	protected override void Start()
	{
		animator = GetComponent<Animator>();
		food = Manager.instance.playerFood;

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

		base.AttemptMove<T>(xDir, yDir);

		RaycastHit2D hit;

		if (Move(xDir, yDir, out hit))
		{
		}

		CheckIfGameOver();

		Manager.instance.playerTurn = false;
	}

	protected override void OnCantMove<T>(T component)
	{
		Wall hitWall = component as Wall;

		hitWall.DamageWall(wallDmg);

		animator.SetTrigger("playerChop");
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Exit")
		{
			Invoke("Restart", restartDelay);

			enabled = false;
		}

		else if (other.tag == "Food")
		{
			food += pointsPerFood;

			other.gameObject.SetActive(false);
		}

		else if (other.tag == "Soda")
		{
			food += pointsPerSoda;

			other.gameObject.SetActive(false);
		}
	}

	private void Restart()
	{
		SceneManager.LoadScene(0);
	}

	public void LoseFood(int loss)
	{
		animator.SetTrigger("playerHit");

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

			Manager.instance.GameOver();
		}
	}

}
