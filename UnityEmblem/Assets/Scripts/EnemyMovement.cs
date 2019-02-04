using UnityEngine;
using System.Collections;

public class EnemyMovement : Movement
{
	public int playerDamage;

	private Animator animator;
	private Transform target;
	private bool skipMove;


	protected override void Start()
	{

		Manager.instance.AddEnemyToList(this);


		animator = GetComponent<Animator>();

		target = GameObject.FindGameObjectWithTag("Player").transform;

		base.Start();
	}


	protected override void AttemptMove<T>(int xDir, int yDir)
	{
		if (skipMove)
		{
			skipMove = false;
			return;

		}

		base.AttemptMove<T>(xDir, yDir);

		skipMove = true;
	}


	public void MoveEnemy()
	{
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)

			yDir = target.position.y > transform.position.y ? 1 : -1;

		else
			xDir = target.position.x > transform.position.x ? 1 : -1;

		//Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player
		AttemptMove<PlayerMovement>(xDir, yDir);
	}


	protected override void OnCantMove<T>(T component)
	{
		PlayerMovement hitPlayer = component as PlayerMovement;

		hitPlayer.LoseFood(playerDamage);

		animator.SetTrigger("enemyAttack");

	}
}