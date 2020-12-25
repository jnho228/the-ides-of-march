using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyObject;
    public GameObject daggerObject;

    private bool isActive = true;
    private float spawnTimer = 1f;
    private float spawnDelay = 5f;

    private float difficultyIncreaseTimer = 15f;
    private float difficultyIncreaseDelay = 15f;
    private int difficultyLevel = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //remove this later
            Application.Quit(-10);

        if (!isActive)
            return;

        // Timer stuff
        if (spawnTimer < Time.deltaTime)
        {
            // Spawn stuff
            // Define a playing area and have mobs spawn on the edge and move randomly in a direction

            //-20.5, 13.5 / -11.5, 15.5
            EnemyController newEnemy = Instantiate(enemyObject, new Vector2(0, 0), Quaternion.identity).GetComponent<EnemyController>();
            int randomSide = Random.Range(0, 4);

            if (randomSide == 0) //left
            {
                newEnemy.SetPosition(new Vector2(-20.5f, Random.Range(-10.5f, 14.5f)));
                newEnemy.SetMoveAngle(90 + Random.Range(-15f, 15f));
                newEnemy.SetMoveSpeed(Random.Range(1f, 4f));
            }
            if (randomSide == 1) //top
            {
                newEnemy.SetPosition(new Vector2(Random.Range(-19.5f, 12.5f), 15.5f));
                newEnemy.SetMoveAngle(Random.Range(-15f, 15f));
                newEnemy.SetMoveSpeed(Random.Range(1f, 4f));
            }
            if (randomSide == 2) //right
            {
                newEnemy.SetPosition(new Vector2(13.5f, Random.Range(-10.5f, 14.5f)));
                newEnemy.SetMoveAngle(-90 + Random.Range(-15f, 15f));
                newEnemy.SetMoveSpeed(Random.Range(1f, 4f));
            }
            if (randomSide == 3) //bottom
            {
                newEnemy.SetPosition(new Vector2(Random.Range(-19.5f, 12.5f), -11.5f));
                newEnemy.SetMoveAngle(180 + Random.Range(-15f, 15f));
                newEnemy.SetMoveSpeed(Random.Range(1f, 4f));
            }

            spawnTimer = spawnDelay - (difficultyLevel * 0.5f);

            if (spawnTimer < 0.5f) // Hard limit so it's not... too hard, ya'know?
                spawnTimer = 0.5f;
        }
        else
            spawnTimer -= Time.deltaTime;

        // Difficulty Increase
        if (difficultyIncreaseTimer < Time.deltaTime)
        {
            // I think with each difficulty bump I wanna spawn some sort of super enemy lol

            difficultyLevel++;
            difficultyIncreaseTimer = difficultyIncreaseDelay;
        }
        else
            difficultyIncreaseTimer -= Time.deltaTime;
    }

    public void EndGame()
    {
        isActive = false;

        // Pause everything
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject go in enemies)
        {
            EnemyController ec = go.GetComponent<EnemyController>();
            ec.enabled = false;
        }

        GameObject[] daggers = GameObject.FindGameObjectsWithTag("Dagger");

        foreach(GameObject go in daggers)
        {
            DaggerController dc = go.GetComponent<DaggerController>();
            dc.enabled = false;
        }
    }
}
