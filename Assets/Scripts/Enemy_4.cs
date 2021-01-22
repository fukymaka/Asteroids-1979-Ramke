using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : Enemy
{
    [Header("Set in Inspector: Enemy_4")]    
    public float minDuration = 1;
    public float maxDuration = 2;

    private float speed;
    private Vector3 pos1;
    private float timeStart;    
    private Fire fire;

    public override void Start()
    {
        base.Start();

        timeStart = Time.time;
        pos1 = Vector3.zero;
        speed = Random.Range(minSpeed / 100, maxSpeed / 100);

        fire = this.GetComponent<Fire>();

        SoundsCtrl.S.PlayUfoComingSound();
    }


    public override void Move()
    {    
        if (Time.time - timeStart > Random.Range(minDuration, maxDuration))
        {
            pos1.x = Random.Range(xMin - 10, xMax + 10);
            pos1.y = Random.Range(yMin - 10, yMax + 10);

            timeStart = Time.time;
            speed = Random.Range(minSpeed / 100, maxSpeed / 100);

            bndCtrl.keepOnScreen = false;
        }

        pos = Vector3.LerpUnclamped(pos, pos1, Time.deltaTime * speed);
    }


    private void Update()
    {
        Move();

        if (Hero.S != null)
        {
            Vector3 dirFire = Hero.S.transform.position - this.transform.position;  //расчет направления для выстрела
            fire.TempFire("ProjectileEnemy", dirFire.normalized);
        }

        if (Main.S.asteroids.Count == 0 || Main.S.isShotDown || Hero.S == null)
        {
            Main.S.isShotDown = false;
            Destroy(this.gameObject);
        }        
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (this.gameObject.tag)
        {
            case "Enemy_4":
                Main.S.score += 200;
                break;
            case "Enemy_5":
                Main.S.score += 1000;
                break;
        }

        Main.S.Explosion(pos);
        SoundsCtrl.S.PlayUfoDeathSound();
        Destroy(this.gameObject);
    }
}
