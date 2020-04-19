using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float movespeed;

    public GameObject target;

    public int health = 10;

    public Animator anim;
    public Rigidbody2D rb;
    public AudioSource audio;
    public AudioSource audio1;
    public EscapeMenu em;
    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
            em = GameObject.Find("MenuHandler").GetComponent<EscapeMenu>();
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        movespeed = Random.Range(0.3f, 1);
    }

    private void FixedUpdate()
    {
        if(target.GetComponent<FirePitController>() != null)
            if (target.GetComponent<FirePitController>().fireHP <= 0)
                return;

        if (em != null && em.paused == true)
            return;

        if (health > 0)
        {
           
            rb.MovePosition(Vector3.MoveTowards(this.transform.position, target.transform.position, Time.deltaTime * movespeed));
        }
    }

    public void takeDamage(int damage)
    {
        audio1.Play();
        health -= damage;
        if(health <= 0)
        {
            anim.SetBool("Dead", true);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            audio.Play(0);
        }
            
        StartCoroutine(WaitHalfSec());
    }

    IEnumerator WaitHalfSec()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }


}
