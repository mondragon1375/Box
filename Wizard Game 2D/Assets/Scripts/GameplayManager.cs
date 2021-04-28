using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    public GameObject[] enemies;
    public Level[] level;
    public Text scoreCountText;
    public Text scoreCountMaxText;
    public Text currentlevelText;
    public TextMeshProUGUI roundComplete;

    public bool spawn = true;
    public int levelNumber;
    public float spawnTime;

    private int score;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        roundComplete.gameObject.SetActive(false);
        levelNumber = 0;
    }

    private void Update()
    {
        TextHolder();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        spawnTime = level[levelNumber].spawnRate;
        if (score >= level[levelNumber].ScoreMax)
        {
            StartCoroutine(UpgradeThePlayer());
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    IEnumerator UpgradeThePlayer()
    {
        roundComplete.gameObject.SetActive(true);
        roundComplete.text = "Level " + (levelNumber + 1) + " Complete";

        score = 0;
        levelNumber++;
        GameObject.Find("Player").GetComponent<Player>().currentWeapon = level[levelNumber].weapon;
        GameObject.Find("Player").transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = GameObject.Find("Player").GetComponent<Player>().currentWeapon.currentWeaponSpr;
        //transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = currentWeapon.currentWeaponSpr;
        spawn = false;
        DestroyAllEnemies();
        yield return new WaitForSeconds(2);
        roundComplete.gameObject.SetActive(false);
        spawn = true;
    }

    void DestroyAllEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
    }

    void TextHolder()
    {
        scoreCountText.text = score.ToString();
        scoreCountMaxText.text = level[levelNumber].ScoreMax.ToString();
        currentlevelText.text = (levelNumber + 1).ToString();
    }
}
