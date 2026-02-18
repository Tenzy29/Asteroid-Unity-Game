using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip music;
    public AudioClip clickSound;
    public AudioSource audioSource;

    public GameObject optionsMenu;
    public GameObject mainMenu;
    void Start()
    {
        startMusic();
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void PlayGame()
    {
        click();
        StartCoroutine(waitExit());
    }
    public void OptionsGame()
    {
        click();
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void BackButton()
    {
        click();
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void QuitGmae()
    {
        click();
        Application.Quit();
    }
    private void startMusic()
    {
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void click()
    {
        audioSource.PlayOneShot(clickSound);
    }
    IEnumerator waitExit()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("GameScene");
    }
}
