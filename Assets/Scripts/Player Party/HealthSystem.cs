﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour {
    public bool gameOver = false;
    private float gameOverScreenTime = 3.0f;
    public GameObject gameOverScreen;

    private int maxHeartAmount = 5;
    public int startHearts = 5;
    public int currHealth;
    private int maxHealth;
    private int healthPerHeart = 2;

    public Image[] healthImages;
    public Sprite[] healthSprites;

	// Use this for initialization
	void Start () {
        currHealth = startHearts * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;
        UpdateHearts();
	}

    void UpdateHearts()
    {
        if (!gameOver)
        {
            bool empty = false;
            int i = 0;

            foreach (Image image in healthImages)
            {
                if (empty)
                {
                    image.sprite = healthSprites[0];
                }
                else
                {
                    i++;
                    if (currHealth >= i * healthPerHeart)
                    {
                        image.sprite = healthSprites[healthSprites.Length - 1];
                    }
                    else
                    {
                        int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - currHealth));
                        int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                        int imageIndex = currentHeartHealth / healthPerImage;

                        image.sprite = healthSprites[imageIndex];
                        empty = true;
                    }
                }
            }

            if (currHealth == 0)
            {
                gameOver = true;
                gameOverScreen.SetActive(true);
            }
        }
    }
    
    public void TakeDamage(int amount)
    {
        currHealth += amount;
        currHealth = Mathf.Clamp(currHealth, 0, startHearts*healthPerHeart);
        UpdateHearts();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        if (gameOver)
        {
            gameOverScreenTime -= Time.deltaTime;

            if (gameOverScreenTime < 0)
            {
                Restart();
                gameOver = false;
                currHealth = maxHealth;
            }
        }
    }
}