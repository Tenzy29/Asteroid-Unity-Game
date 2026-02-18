using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float spawnRate;
    public GameObject[] enemy;
    bool isDead = false;


    public AudioSource audioSource;
    public AudioClip music;

    public GameObject pauseMenu;
    public GameObject restartMenu;
    public GameObject shopMenu;
    public static bool isPaused;
    public static bool isShop;
    public GameObject stats2;

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI playerSpeedText;
    public TextMeshProUGUI shootDelayText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI coinsText2;
    public TextMeshProUGUI playerSpeedText2;
    public TextMeshProUGUI shootDelayText2;
    public TextMeshProUGUI damageText2;
    public TextMeshProUGUI healthText2;

    public TextMeshProUGUI playerSpeedTextPrice;
    public TextMeshProUGUI shootDelayTextPrice;
    public TextMeshProUGUI damageTextPrice;
    public TextMeshProUGUI healthTextPrice;

    public GameObject player;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        stats2.SetActive(true);
        pauseMenu.SetActive(false);
        shopMenu.SetActive(false);
        restartMenu.SetActive(false);

        StartCoroutine(spawning());
        startMusic();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins: " + playerController.coins.ToString();
        playerSpeedText.text = "Speed: " + playerController.speed.ToString();
        shootDelayText.text = "Shoot Delay: " + playerController.shootDelay.ToString();
        damageText.text = "Damage: " + playerController.damage.ToString();
        healthText.text = "Health: " + playerController.health.ToString();

        coinsText2.text = "Coins: " + playerController.coins.ToString();
        playerSpeedText2.text = "Speed: " + playerController.speed.ToString();
        shootDelayText2.text = "Shoot Delay: " + playerController.shootDelay.ToString();
        damageText2.text = "Damage: " + playerController.damage.ToString();
        healthText2.text = "Health: " + playerController.health.ToString();

        playerSpeedTextPrice.text = "Price: " + costSpeed.ToString();
        shootDelayTextPrice.text = "Price: " + costDelay.ToString();
        damageTextPrice.text = "Price: " + costDamage.ToString();
        healthTextPrice.text = "Price: " + costHealth.ToString();




        if (playerController.health<=0 && isDead==false)
        {
            isDead = true;
            restartMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(isShop==false)
            {
                ShopMenu();
            }
            else
            {
                ShopMenuExit();
            }

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
               PauseGame();
        }
    }

    private void spawnEnemy()
    {
        Vector3 rand = new Vector3(Random.Range(-7, 8), 6, 0);
        int enemyIndex = Random.Range(0, 4);

        GameObject spawnedEnemy = Instantiate(enemy[enemyIndex], rand, Quaternion.identity); //Random.Range(0,4)                     //          dddddddddddddddddddd

        if (enemyIndex<3)
        {
            float randomScale = Random.Range(0.8f, 1.5f);
            spawnedEnemy.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }

    }
    IEnumerator spawning()
    {
        while (true)
        {
            spawnEnemy();
            yield return new WaitForSeconds(spawnRate);
            
        }
        
    }
    private void startMusic()
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    private void ShopMenu()
    {
        stats2.SetActive(false);
        shopMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        isShop = true;
    }
    private void ShopMenuExit()
    {
        stats2.SetActive(true);
        shopMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        isShop = false;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void restartGame()
    {
        restartMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("GameScene");
        
    }
    int costSpeed = 1;
    public void UpgradeSpeed()
    {
        if (playerController.coins > costSpeed)
        {
            playerController.speed += 1f;  // Upgrade speed by 1 unit
            playerController.coins -= costSpeed;
            costSpeed *= 2;
        }
    }

    int costDelay = 1;
    public void UpgradeShootDelay()
    {
          // Reduce shoot delay by 0.1 second
        if (playerController.coins > costDelay)
        {
            playerController.shootDelay -= 0.1f;  // Upgrade speed by 1 unit
            playerController.coins -= costDelay;
            costDelay *= 2;
        }
    }

    int costDamage = 1;
    public void UpgradeDamage()
    {
        
        if (playerController.coins > costDamage)
        {
            playerController.damage +=1;  // Upgrade speed by 1 unit
            playerController.coins -= costDamage;
            costDamage *= 2;
        }
    }
    int costHealth = 1;
    public void UpgradeHealth()
    {
        
        if (playerController.coins > costHealth)
        {
            playerController.health += 1;  // Upgrade speed by 1 unit
            playerController.coins -= costHealth;
            costHealth *= 2;
        }
    }

}
