using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject projectilePrefab;
    public float projSpeed = 2;
    public float timeShot = 1;

    [Header("Set in script")]
    static public Transform PROJECTILE_ANCHOR;
    private float lastShot = 0;


    private void Awake()
    {
        lastShot = Time.time;
        print(lastShot);
    }

    private void Start()
    {
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }
        
        
    }


    public void TempFire(string tag, Vector3 dirFire)
    {        
        if (Time.time - lastShot > timeShot)
        {            
            GameObject projGO = Instantiate(projectilePrefab);
            projGO.tag = tag;
            projGO.transform.position = this.transform.position + dirFire;
            projGO.transform.SetParent(PROJECTILE_ANCHOR, true);
            Rigidbody2D rigidB = projGO.GetComponent<Rigidbody2D>();

            rigidB.velocity = dirFire.normalized * projSpeed;

            lastShot = Time.time;

            switch(this.gameObject.tag)
            {
                case "Enemy_4":
                case "Enemy_5":
                    SoundsCtrl.S.PlayUfoShotSound();
                    break;
                case "Hero":
                    SoundsCtrl.S.PlayHeroShotSound();
                    break;
            }
        }
    }
}
