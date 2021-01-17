﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeAttack : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<VirusClass>() != null && !collision.GetComponent<VirusClass>().Invincible)
        {
            collision.GetComponent<VirusClass>().GetDamaged(damage);
            Destroy(gameObject);
        }        
    }
}
