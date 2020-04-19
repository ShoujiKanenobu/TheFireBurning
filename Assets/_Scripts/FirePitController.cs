using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class FirePitController : MonoBehaviour
{
    public float fireHP = 100;
    public float burnoutModifier;
    public float burnoutRampUp;
    public float burnoutMax;
    public Slider hpBar;
    public VisualEffect vfx;
    public AudioSource audio;
    public float maxVolume;
    public GameObject deathText;
    public EscapeMenu em;

    private void Start()
    {
        em = GameObject.Find("MenuHandler").GetComponent<EscapeMenu>();
    }
    // Update is called once per frame
    void Update()
    {
        if (em.paused == true)
            return;
        if (fireHP > 100)
        {
            fireHP = 100;
        }

        if (fireHP > 0)
        {
            fireHP -= Time.deltaTime * burnoutModifier;
            if(burnoutModifier <= burnoutMax)
                burnoutModifier += burnoutRampUp;
        }
        else
        {
            deathText.SetActive(true);
        }
        vfx.SetFloat("Intensity", (fireHP / 100) * 18f);
        hpBar.value = fireHP / 100;
        audio.volume = (fireHP / 100) * maxVolume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item>() && collision.gameObject.GetComponent<Item>().type == Item.ItemType.LOG)
        {
            fireHP += 10;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponent<Item>() && collision.gameObject.GetComponent<Item>().type == Item.ItemType.COAL)
        {
            fireHP += 20;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponent<ProjectileController>())
        {
            fireHP += 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            fireHP -= 5;
            collision.gameObject.GetComponent<EnemyController>().takeDamage(999);
        }
    }

}
