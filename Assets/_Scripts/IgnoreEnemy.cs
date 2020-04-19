using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<Collider2D>());
        }
    }
}
