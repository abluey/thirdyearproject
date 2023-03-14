using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text header;
    [SerializeField] private TMPro.TMP_Text content;
    [SerializeField] private Button nextBtn;

    private string[] headerText = {"Even More Time Passes", "The Days Go By", "In Fact", "The Reason", "Who Knows?", "Or",};
    private string[] contentText = {"",
                                    "and you never hear from Reese again.",
                                    "FriendMi is shut down soon after.",
                                    "being an external attack against the system.",
                                    "Maybe Reese is living in a cave somewhere with their friends now.",
                                    "'They' caught up to them. Whoever 'they' are.",};

    void Start()
    {   
        nextBtn.onClick.AddListener(Next);

        content.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);

        StartCoroutine(EndIt());
    }

    private IEnumerator EndIt() {
        if (PlayerChoices.endCount < headerText.Length) {
            header.text = headerText[PlayerChoices.endCount];
            content.text = contentText[PlayerChoices.endCount];
            PlayerChoices.endCount += 1;
        } else {
            header.text = "The End";
            content.text = "Thank you for your time.";

            nextBtn.onClick.RemoveAllListeners();
            nextBtn.onClick.AddListener(Finish);
        }

        yield return new WaitForSeconds(1.5f);
        content.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        nextBtn.gameObject.SetActive(true);
        
    }

    private void Next() {
        StartCoroutine(NextWaitLoad());
    }

    private IEnumerator NextWaitLoad() {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Finish() {
        PlayerPrefs.DeleteAll();
        Save.DeleteAllData();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartMenu", LoadSceneMode.Additive);
        asyncLoad.completed += OnLoadComplete;
    }

    private void OnLoadComplete(AsyncOperation loadOperation) {
        _ = SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
