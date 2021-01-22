using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();        
    }

    private void Update()
    {
        if (ps.isStopped)
        {
            Destroy(this.gameObject);
        }
    }
}
