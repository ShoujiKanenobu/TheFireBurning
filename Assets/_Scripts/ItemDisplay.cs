using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public SpriteRenderer sr;
    public PlayerController pc;

    private Color blankColor = new Color(0, 0, 0, 0);
    private Color neutralColor = new Color(255, 255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        pc = this.transform.parent.GetComponent<PlayerController>();
        sr = this.GetComponent<SpriteRenderer>();
        if (pc == null)
        {
            Debug.LogError("Couldnt Find player controller for the item display");
        }
        if (sr == null)
        {
            Debug.LogError("Couldnt Find Sprite Renderer for the item display");
        }
    }


    public void UpdateItem()
    {
        if (pc.currentItem == null)
        {
            sr.color = blankColor;
        }
        else
        {
            sr.color = neutralColor;
            sr.sprite = pc.currentItem.itemSprite;
        }
    }
}
