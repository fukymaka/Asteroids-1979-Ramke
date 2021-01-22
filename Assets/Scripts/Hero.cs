using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;

    [Header("Set in Inspector")]
    public float posSpeed = 2;
    public float rotSpeed = 2;
    public float stopSpeed = 2;
        
    [Header("Set in script")]
    private float timeLeft = 0;
    private Rigidbody2D rigid;
    private Fire fire;
    private Animator anim;    


    private void Start()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("The singleton S of Hero has already been set!");
        }

        rigid = S.GetComponent<Rigidbody2D>();
        fire = S.GetComponent<Fire>();        

        anim = S.GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            timeLeft += Time.deltaTime * rotSpeed;
            S.transform.rotation = Quaternion.Euler(0, 0, timeLeft);
        }

        if (Input.GetKey(KeyCode.D))
        {
            timeLeft -= Time.deltaTime * rotSpeed;
            S.transform.rotation = Quaternion.Euler(0, 0, timeLeft);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(S.transform.up * posSpeed);
            anim.SetBool("isThrust", true);

            SoundsCtrl.S.PlayThrustSound(true);
        }
        else
        {
            rigid.velocity = Vector3.LerpUnclamped(rigid.velocity, Vector3.zero, Time.deltaTime * stopSpeed);
            anim.SetBool("isThrust", false);

            SoundsCtrl.S.PlayThrustSound(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 dirFire = S.transform.up; //set direction of fire
            fire.TempFire("ProjectileHero", dirFire / 2);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject go = coll.gameObject;

        switch (go.tag)
        {
            case "Enemy_1":
            case "Enemy_2":
            case "Enemy_3":
            case "Enemy_4":
            case "Enemy_5":
                
                Main.S.Explosion(S.transform.position); //effect of explosion
                Main.S.Revival();
                SoundsCtrl.S.PlayThrustSound(false);
                SoundsCtrl.S.PlayDeathHeroSound();
                Destroy(this.gameObject);
                break;
            case "ProjectileEnemy":

                Main.S.isShotDown = true; //for "quiet" destruction of the UFO after the shot
                Main.S.Explosion(S.transform.position);
                Main.S.Revival();
                SoundsCtrl.S.PlayThrustSound(false);
                SoundsCtrl.S.PlayDeathHeroSound();
                Destroy(this.gameObject);
                break;
        }
    }
}
