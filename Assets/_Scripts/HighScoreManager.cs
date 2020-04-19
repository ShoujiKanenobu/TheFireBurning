using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreManager : MonoBehaviour
{
    public GameObject newHighScoreText;
    public TextMeshProUGUI text;
    public FirePitController fp;
    public PointSystem ps;
    public void Update()
    {
        if(fp.fireHP <= 0 && ps.score >= PlayerPrefs.GetInt("HighScoreScore"))
        {
            newHighScore(ps.score, ps.time);
            newHighScoreText.SetActive(true);
        }
        else
        {
            newHighScoreText.SetActive(false);
        }
        if (fp.fireHP <= 0 && ps.time >= PlayerPrefs.GetInt("HighTimeTime"))
        {
            newHighTime(ps.score, ps.time);
            newHighScoreText.SetActive(true);
        }
        else
        {
            newHighScoreText.SetActive(false);
        }


        if(this.gameObject.activeInHierarchy == true)
        {
            float[] highscore = DisplayHighScore();
            text.text = "Highest Score" +
                "\nScore: " + highscore[0].ToString("F2") + 
                "\nTime: " + highscore[1].ToString("F2");
            float[] highTime = DisplayHighTime();
            text.text += "\n\nBest Time " +
                "\nScore: " + highTime[0].ToString("F2") +
                "\nTime: " + highTime[1].ToString("F2");
        }
    }

    public float[] DisplayHighScore()
    {
        float[] highscore = new float[2];
        highscore[0] = PlayerPrefs.GetFloat("HighScoreScore");
        highscore[1] = PlayerPrefs.GetFloat("HighScoreTime");
        return highscore;
    }

    public void newHighScore(float score, float time)
    {
        PlayerPrefs.SetFloat("HighScoreScore", score);
        PlayerPrefs.SetFloat("HighScoreTime", time);

    }

    public float[] DisplayHighTime()
    {
        float[] hightime = new float[2];
        hightime[0] = PlayerPrefs.GetFloat("HighTimeScore");
        hightime[1] = PlayerPrefs.GetFloat("HighTimeTime");
        return hightime;
    }

    public void newHighTime(float score, float time)
    {
        PlayerPrefs.SetFloat("HighTimeScore", score);
        PlayerPrefs.SetFloat("HighTimeTime", time);

    }
}
