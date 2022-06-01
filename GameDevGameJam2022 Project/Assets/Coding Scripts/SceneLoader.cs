using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float buttonSFXloadTime = .2f;
    [SerializeField] AudioClip buttonClickSFX;
    MusicManager musicManager;

    private void Start() {
        musicManager = FindObjectOfType<MusicManager>();
        if (musicManager == null) {return;}
        musicManager.LoadSong();
    }

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

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        
        var playerPieces = FindObjectOfType<TeamManager>().playerTeam;

        foreach (GameObject piece in playerPieces)
        {
            if (piece.gameObject.GetComponent<ItemObject>() != null)
            {
                piece.gameObject.GetComponent<ItemObject>().Pickup();
            }
        }

        yield return new WaitForSeconds(0.2f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex+1 == 5)
        {
            Destroy(FindObjectOfType<InventorySystem>().gameObject);
        }

        SceneManager.LoadScene(currentSceneIndex+1);
    }

}
