using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

	public float turnDelay = .1f;
	public BoardManager boardScript;

	public int playerFood = 100;

	public bool playerTurn = true;

	public static Manager instance = null;

	private int level = 1;

	private List<EnemyMovement> enemies;
	private bool enemiesMoving;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		enemies = new List<EnemyMovement>();
		InitGame();
	}

	void Update()
	{
		if (playerTurn || enemiesMoving)
			return;
		StartCoroutine(MoveEnemies());
	}

	void InitGame()
	{
		enemies.Clear();
		boardScript.SetupScene(level);
	}

	void OnLevelWasLoaded(int index)
	{
		level++;
		InitGame();
	}

	public void GameOver()
	{
		enabled = false;
	}


	public void AddEnemyToList(EnemyMovement enemy)
	{
		enemies.Add(enemy);
	}

	IEnumerator MoveEnemies()
	{
		enemiesMoving = true;

		yield return new WaitForSeconds(turnDelay);

		if (enemies.Count == 0)
		{
			yield return new WaitForSeconds(turnDelay);
		}

		foreach (EnemyMovement enemy in enemies)
		{
			enemy.MoveEnemy();

			yield return new WaitForSeconds(enemy.moveTime);
		}
		playerTurn = true;

		enemiesMoving = false;
	}
}
