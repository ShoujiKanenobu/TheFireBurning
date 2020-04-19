using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 8f;
    public float dropDistance;
    

    public Rigidbody2D rb;
    public Animator anim;
    private ItemDisplay itemDisplay;
    public FirePitController fp;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public GameObject projectile;
    public int pierceBonus = 0;
    public EscapeMenu em;
    private Vector2 movement;

    public GameObject[] items;

    public enum currentDir { UP, DOWN, LEFT, RIGHT };
    public currentDir dir;

    public Item currentItem;
    private void Start()
    {
        em = GameObject.Find("MenuHandler").GetComponent<EscapeMenu>();
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        itemDisplay = this.transform.GetChild(0).gameObject.GetComponent<ItemDisplay>();
        if (anim == null)
        {
            Debug.LogError("Couldn't find the animator on gameobject: " + this.gameObject.name);
        }
        if (rb == null)
        {
            Debug.LogError("Couldn't find the rigidbody2D on gameobject: " + this.gameObject.name);
        }
        if (itemDisplay == null)
        {
            Debug.LogError("Couldn't find the itemdisplay on gameobject: " + this.gameObject.name);
        }
        itemDisplay.StartCoroutine("UpdateItem");
    }
    private void Update()
    {
        if (fp.fireHP <= 0 || em.paused == true)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            DropItem();

        if(Input.GetButtonDown("Fire1") && currentItem != null && currentItem.type == Item.ItemType.LOG)
        {
            Vector3 fireAt = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -0.01f));
            GameObject newProj = Instantiate(projectile);
            newProj.transform.position = this.transform.position;
            newProj.GetComponent<ProjectileController>().setDestination(fireAt);
            newProj.GetComponent<ProjectileController>().addPierce(pierceBonus);
            currentItem = null;
            itemDisplay.StartCoroutine("UpdateItem");
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.y > 0)
            dir = currentDir.UP;
        else if (movement.y < 0)
            dir = currentDir.DOWN;
        else if (movement.x > 0)
            dir = currentDir.RIGHT;
        else if (movement.x < 0)
            dir = currentDir.LEFT;

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.magnitude);

        if(movement.magnitude > 0.01 || movement.magnitude < -0.01)
        {
            audio1.UnPause();
        }
        else
        {
            audio1.Pause();
        }

        switch(dir)
        {
            case currentDir.DOWN:
                anim.SetInteger("DIR", 2);
                break;
            case currentDir.UP:
                anim.SetInteger("DIR", 0);
                break;
            case currentDir.RIGHT:
                anim.SetInteger("DIR", 1);
                break;
            case currentDir.LEFT:
                anim.SetInteger("DIR", 3);
                break;
        }
        
    }
    private void FixedUpdate()
    {
        if (fp.fireHP <= 0)
            return;
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IPickup>() != null && currentItem == null && collision.gameObject.GetComponent<Item>().type == Item.ItemType.LOG)
        {
            Item hold = collision.gameObject.GetComponent<IPickup>().Pickup();
            PickupItem(hold);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Item>() != null && collision.gameObject.GetComponent<Item>().type == Item.ItemType.CHOCOLATE)
        {
            audio2.Play();
            movespeed += movespeed * 0.1f;
            Destroy(collision.gameObject);
            currentItem = null;
        }
        else if (collision.gameObject.GetComponent<Item>() != null && collision.gameObject.GetComponent<Item>().type == Item.ItemType.COAL)
        {
            Item hold = collision.gameObject.GetComponent<IPickup>().Pickup();
            PickupItem(hold);
            collision.gameObject.SetActive(false);
        }
    }

    public void PickupItem(Item i)
    {
        audio2.Play();
        if (currentItem != null)
        {
            DropItem();
        }
        if (i == null)
        {
            currentItem = null;
        }
        currentItem = i;
        itemDisplay.StartCoroutine("UpdateItem");
    }

    public void DropItem()
    {
        if (currentItem == null)
            return;

        audio3.Play();

        GameObject itemToCreate;

        switch (currentItem.type)
        {
            case Item.ItemType.LOG:
                itemToCreate = items[0];
                break;
            case Item.ItemType.COAL:
                itemToCreate = items[1];
                break;
            default:
                itemToCreate = new GameObject();
                break;
        }
        GameObject createdItem = null;
        switch (dir)
        {
            case currentDir.UP:
                createdItem = Instantiate(itemToCreate);
                createdItem.transform.position = this.gameObject.transform.position + new Vector3(0, dropDistance, 0);
                break;
            case currentDir.DOWN:
                createdItem = Instantiate(itemToCreate);
                createdItem.transform.position = this.gameObject.transform.position + new Vector3(0, -dropDistance, 0);
                break;
            case currentDir.LEFT:
                createdItem = Instantiate(itemToCreate);
                createdItem.transform.position = this.gameObject.transform.position + new Vector3(-dropDistance, 0, 0);
                break;
            case currentDir.RIGHT:
                createdItem = Instantiate(itemToCreate);
                createdItem.transform.position = this.gameObject.transform.position + new Vector3(dropDistance, 0, 0);
                break;
        }
        Destroy(currentItem.gameObject);

        currentItem = null;
        itemDisplay.StartCoroutine("UpdateItem");
    }
}
