using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject logPrefab;
    public float rotationSpeed;
    public float throwSpeed;
    public float throwTime;
    public Vector3 destination;
    public Rigidbody2D rb;
    private float aliveTime = 0;
    private Vector3 direction;
    public int pierce = 1;
    public PointSystem ps;
    ProjectileController(Vector3 dest)
    {
        destination = dest;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GameObject.Find("Score").GetComponent<PointSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0,rotationSpeed));
        
        rb.MovePosition(this.transform.position + direction * throwSpeed * Time.deltaTime);
        aliveTime += Time.deltaTime;
        if(aliveTime >= throwTime)
        {
            GameObject dropItem = Instantiate(logPrefab);
            dropItem.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyController>())
        {
            Debug.Log("Entered");
            collision.GetComponent<EnemyController>().takeDamage(10);
            ps.addScore();
            pierce--;
            if (pierce <= 0)
                Destroy(this.gameObject);
        }
    }

    public void setDestination(Vector3 dis)
    {
        destination = dis;
        Vector3 heading = destination - this.transform.position;
        float distance = heading.magnitude;
        direction = heading / distance;
    }

    public void addPierce(int amount)
    {
        pierce += amount;
    }
}
