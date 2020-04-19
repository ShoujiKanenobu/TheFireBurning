using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointSystem : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public TextMeshProUGUI timer;
    public FirePitController firepit;
    public EscapeMenu em;
    public float score;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        em = GameObject.Find("MenuHandler").GetComponent<EscapeMenu>();
        tmpText = this.gameObject.GetComponent<TextMeshProUGUI>();
        timer = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firepit.fireHP <= 0 || em.paused == true)
            return;
        tmpText.text = "Score: " + (int)score;
        timer.text = "Time: " + time.ToString("F2");
        time += Time.deltaTime;
        if (firepit.burnoutModifier > 1)
            score += Time.deltaTime / 0.3f * (firepit.burnoutModifier / 0.2f) * (1 / firepit.fireHP);
        else
        {
            score += 10 * Time.deltaTime * (1 / firepit.fireHP);
        }
    }
}
