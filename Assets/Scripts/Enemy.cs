using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float outerBorderSpawn = 2f; 
    public float innerBorderSpawn = 2f; 
    public float minSpeed = 50;
    public float maxSpeed = 100;
    public int childNums = 2;
    public GameObject enemyChildPrefab = null;    

    [HideInInspector]
    protected float xMin, xMax, yMin, yMax;
    protected float xMinZone, xMaxZone, yMinZone, yMaxZone;
    protected Rect clearZone;
    protected Vector3 tmpPos;
    protected BoundsCtrl bndCtrl;
    protected Rigidbody2D rigid2D;    


    private void Awake()
    {
        bndCtrl = GetComponent<BoundsCtrl>();
        rigid2D = GetComponent<Rigidbody2D>();
    }


    public virtual void Start()
    {
        if (this.transform.position == Vector3.zero)
        {
            tmpPos = Vector3.zero;

            xMin = -bndCtrl.camWidth - outerBorderSpawn;
            xMax = bndCtrl.camWidth + outerBorderSpawn;
            yMin = -bndCtrl.camHeight - outerBorderSpawn;
            yMax = bndCtrl.camHeight + outerBorderSpawn;

            xMinZone = -bndCtrl.camWidth + innerBorderSpawn;
            xMaxZone = bndCtrl.camWidth - innerBorderSpawn;
            yMinZone = -bndCtrl.camHeight + innerBorderSpawn;
            yMaxZone = bndCtrl.camHeight - innerBorderSpawn;

            clearZone = GetZoneWithoutEnemySpawn();

            tmpPos.x = Random.Range(xMin, xMax);
            tmpPos.y = Random.Range(yMin, yMax);

            pos = tmpPos;

            if (clearZone.Contains(pos))
            {
                int axis = Random.Range(0, 4);

                switch (axis)
                {
                    case 0:
                        tmpPos.x = xMin;
                        tmpPos.y = Random.Range(yMin, yMax);
                        pos = tmpPos;
                        break;
                    case 1:
                        tmpPos.x = xMax;
                        tmpPos.y = Random.Range(yMin, yMax);
                        pos = tmpPos;
                        break;
                    case 2:
                        tmpPos.x = Random.Range(xMin, xMax);
                        tmpPos.y = yMin;
                        pos = tmpPos;
                        break;
                    case 3:
                        tmpPos.x = Random.Range(xMin, xMax);
                        tmpPos.y = yMax;
                        pos = tmpPos;
                        break;
                }
            }
        }

        Move();
    }


    public virtual Rect GetZoneWithoutEnemySpawn()
    {
        return new Rect(new Vector2(xMinZone, yMinZone), new Vector2(xMaxZone * 2, yMaxZone * 2));
    }


    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }


    public virtual void Move()
    {  
        this.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
        this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * Random.Range(minSpeed, maxSpeed));
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject go = coll.gameObject;

        switch(go.tag)
        {
            case "Hero":
            case "Enemy_4":
            case "Enemy_5":
            case "ProjectileEnemy":
            case "ProjectileHero":

                if (enemyChildPrefab != null)
                {
                    for (int i = 0; i < childNums; i++)
                    {
                        GameObject enemyChild = Instantiate(enemyChildPrefab);
                        
                        enemyChild.transform.position = this.transform.position;
                        enemyChild.transform.SetParent(this.transform.parent);

                        Main.S.asteroids.Add(enemyChild);
                    }                    
                }

                Main.S.DeleteAsteroidFromList(this.gameObject);
                Main.S.Explosion(pos);

                switch (this.gameObject.tag)
                {
                    case "Enemy_1":
                        Main.S.score += 20;
                        break;
                    case "Enemy_2":
                        Main.S.score += 50;
                        break;
                    case "Enemy_3":
                        Main.S.score += 100;
                        break;
                    
                }

                Main.S.CheckScore();
                SoundsCtrl.S.PlayAsteroidExplosionSound();
                Destroy(this.gameObject);               
                break;
        }        
    }
}
