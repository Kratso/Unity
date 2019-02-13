using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public BoardManager boardScript;

	public int playerFood = 100;

	public bool playerTurn = true;

	private Text levelText;                                 //Text to display current level number.
	private GameObject levelImage;
	public static Manager instance = null;

	private int level = 0;

	private List<EnemyMovement> enemies;
	private bool enemiesMoving;

	private bool doingSetup = true;

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
		doingSetup = true;
		levelImage = GameObject.Find("Level Image");
		levelText = GameObject.Find("Level Text").GetComponent<Text>();
		if (level != 0)
		{
			levelText.text = "Day: " + level;

			levelImage.SetActive(true);
			enemies.Clear();
			boardScript.SetupScene(level);
		}
		Invoke("HideLevelImage", levelStartDelay);

	}

	void HideLevelImage()
	{
		levelImage.SetActive(false);
		doingSetup = false;
	}

	void OnLevelWasLoaded(int index)
	{
		level++;

		InitGame();
	}

	public void GameOver()
	{
		levelText.text = "You perished after " + level + "days\nYou won't be remembered.";
		levelImage.SetActive(true);
		playerTurn = false;
		enemiesMoving = false;

		Invoke("goToMenu", 3f);

	}

	void goToMenu()
	{
		Destroy(SoundManager.instance);
		Destroy(this);
		SceneManager.LoadScene(0);

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
