using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float buttonSFXloadTime = .2f;
    [SerializeField] AudioClip buttonClickSFX;
    // [SerializeField] GameObject menuButtons;
    // [SerializeField] GameObject startButtons;
    // [SerializeField] GameObject difficultyButtons;

    // private void Start() {
    //     if (startButtons)
    //     {
    //         startButtons.SetActive(false);
    //         difficultyButtons.SetActive(false);
    //         menuButtons.SetActive(true);
    //     }
    // }
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        yield return new WaitForSeconds(buttonSFXloadTime);
        SceneManager.LoadScene("Battle Scene 1");
    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuCoroutine());
    }

    private IEnumerator MainMenuCoroutine()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        yield return new WaitForSeconds(buttonSFXloadTime);

        SceneManager.LoadScene("Main Menu");

    }

    public void CreditScene()
    {
        StartCoroutine(CreditSceneCoroutine());
    }

    private IEnumerator CreditSceneCoroutine()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        yield return new WaitForSeconds(buttonSFXloadTime);

        SceneManager.LoadScene("Credits");
    }

    public void OptionsScreen()
    {
        StartCoroutine(OptionsScreenCoroutine());
    }

    private IEnumerator OptionsScreenCoroutine()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        yield return new WaitForSeconds(buttonSFXloadTime);

        SceneManager.LoadScene("Options");
    }

    public void TutorialScreen()
    {
        StartCoroutine(TutorialCoroutine());
    }

    private IEnumerator TutorialCoroutine()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        yield return new WaitForSeconds(buttonSFXloadTime);

        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    private IEnumerator QuitGameCoroutine()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        yield return new WaitForSeconds(buttonSFXloadTime);

        Application.Quit();
    }

    // public void StartSessionOptions()
    // {
    //     GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        
    //     //load 1 or 2 player buttons and disable other buttons
    //     menuButtons.SetActive(false);
    //     startButtons.SetActive(true);
    // }
    // public void DifficltyOptions()
    // {
    //     GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        
    //     //load 1 or 2 player buttons and disable other buttons
    //     startButtons.SetActive(false);
    //     difficultyButtons.SetActive(true);
    // }

    // public void BackToMenuFromSession()
    // {
    //     GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        
    //     menuButtons.SetActive(true);
    //     startButtons.SetActive(false);
    // }

    // public void BackToSessionFromDifficulty()
    // {
    //     GetComponent<AudioSource>().PlayOneShot(buttonClickSFX);
        
    //     startButtons.SetActive(true);
    //     difficultyButtons.SetActive(false);
    // }
}
