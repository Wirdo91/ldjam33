using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Highscore : MonoBehaviour {

    List<int> highscore;
    void Start()
    {
        highscore = new List<int>();

        ReadScore();
        if (highscore.Count <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                highscore.Add(0);
            }
        }
    }


    public void AddScore(int score)
    {
        Debug.Log("Meh");
        if (score > highscore[highscore.Count - 1])
        {
            highscore.Add(score);
            highscore = highscore.OrderByDescending(i => i).ToList();
            highscore.RemoveRange(10, 1);
        }
        WriteScore();
    }
    void ReadScore()
    {
        string s = PlayerPrefs.GetString("HighScore");

        string[] ss = s.Split(new char[]{','}, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string score in ss)
        {
            highscore.Add(int.Parse(score));
        }
        highscore = highscore.OrderByDescending(i => i).ToList();
    }
    void WriteScore()
    {
        PlayerPrefs.SetString("HighScore", List2String(highscore));
        PlayerPrefs.Save();
    }

    string List2String(List<int> list)
    {
        string s = list[0].ToString();

        for (int i = 1; i < list.Count; i++)
        {
            s += "," + list[i].ToString();
        }

        return s;
    }
}
