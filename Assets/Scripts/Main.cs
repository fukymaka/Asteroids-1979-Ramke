using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    static public Main S;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public GameObject heroPrefab;
    public GameObject explosion;
    public float ufoSpawnChance = 20;
    public int howScoreHealthUp = 10000;
    public int startCountAsteroids = 5;

    [Header("Set in Script")]
    public int score = 0;    
    public int health = 5;
    public int round = 0;
    public int healthUpCount = 1;
    public bool isShotDown = false; //for "quiet" destruction of the UFO after the shot
    public List<GameObject> asteroids;   
    static public Transform ENEMYS_ANCHOR;

    [SerializeField]
    private GameObject hero;


    private void Awake()
    {
        S = this;

        round = 0;
        score = 0;
        health = 5;
        healthUpCount = 1;

        if (ENEMYS_ANCHOR == null)
        {
            GameObject go = new GameObject("_EnemysAnchor");
            ENEMYS_ANCHOR = go.transform;
        }
    }
        

    public void StartRound()
    {
        round++;        

        if (health == 0)
        {
            return;
        }

        for (int i = 0; i < (startCountAsteroids + round - 1); i++)
        {
            SpawnEnemy(0);
        }

        if (hero != null)
        {
            Destroy(hero);
            Hero.S = null;
            
        }

        SpawnUfo();
        hero = Instantiate(heroPrefab); 
    }


    private void SpawnUfo()
    {
        if (Hero.S != null)
        {
            SpawnEnemy(Random.Range(1, 3));
        }

        Invoke("SpawnUfo", Random.Range(10, ufoSpawnChance));
    }

   
    private void SpawnEnemy(int ndx)
    {        
        GameObject go = Instantiate(prefabEnemies[ndx]);
        
        if (ndx == 0)
        {
            asteroids.Add(go);
        }       
        
        go.transform.SetParent(ENEMYS_ANCHOR.transform, true);
    }

    
    public void DelayedRevival()
    {
        hero = Instantiate(heroPrefab);
    }


    public void Revival()
    {
        health--;
        UIcontroller.S.DelHealth(health);

        if (health > 0 & asteroids.Count != 0)
        {           
            Invoke("DelayedRevival", 2);
        }    
        else if (health == 0)
        {
            UIcontroller.S.DelayedRestartGame();
        }
    }


    public void DeleteAsteroidFromList(GameObject a)
    {
        asteroids.Remove(a);

        if (asteroids.Count == 0)
        {
            Invoke("StartRound", 2); 
        }               
    }


    public void CheckScore()
    {
        if (score / howScoreHealthUp == healthUpCount)
        {
            UIcontroller.S.AddHealth(health);
            health++;
            healthUpCount++;            
        }
    }


    public void Explosion(Vector3 v)
    {
        GameObject e = Instantiate(explosion);
        e.transform.position = v;
    }
}
