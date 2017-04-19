using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Button { A, B, X, Y };

public class ArrowCanvasController : MonoBehaviour
{
    public static ArrowCanvasController Instance;

    public GameObject[] ArrowSprites;
    public GameObject[] ArrowSpawns;
    public GameObject[] WinSpace;
    public Text Score;

    public float LastTime;
    public int score;
    public bool HandlingInput;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        score = 0;
        foreach (GameObject sprite in ArrowSprites)
        {
            sprite.SetActive(false);
        }
        gameObject.SetActive(false);
        SetScore();
    }

    // Update is called once per frame
    void Update()
    {
        SetScore();
        SpawnButtons();
    }

    void SpawnButtons()
    {
        float rand = Random.Range(2.0f, 4.0f);
        if (Time.time - LastTime > rand)
        {
            LastTime = Time.time;
            int button = Random.Range(0, 4);
            GameObject Arrow = Instantiate(ArrowSprites[button] as GameObject);
            Arrow.transform.SetParent(transform);
            Arrow.transform.position = ArrowSpawns[button].transform.position;
            Arrow.GetComponent<ArrowController>().Button = Button.A + button;
            Arrow.SetActive(true);

        }
    }

    void SetScore()
    {
        Score.text = "Score: " + score.ToString();
    }

   
}
