using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button homeBtn;

    [SerializeField] private Image firstTimeImage;

    [SerializeField] private TMPro.TMP_InputField nameInput;
    [SerializeField] private TMPro.TMP_InputField dobInput;
    [SerializeField] private TMPro.TMP_Dropdown genderInput;
    private int genderIndex;
    [SerializeField] private TMPro.TMP_InputField bioInput;

    [SerializeField] private TMPro.TMP_Text errorText;
    [SerializeField] private TMPro.TMP_Text savedText;

    private string dobProcessed;
    private string[] dobArray;

    void OnEnable() {
        StopAllCoroutines();
        errorText.gameObject.SetActive(false);
        savedText.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("Name")) {
            firstTimeImage.gameObject.SetActive(false);
            PopulateProfile();
        }
    }
    
    void Start()
    {   
        saveBtn.onClick.AddListener(Save);

        errorText.gameObject.SetActive(false);
        savedText.gameObject.SetActive(false);
    }

    private void Save() {
        errorText.gameObject.SetActive(false);
        savedText.gameObject.SetActive(false);

        if (ValidateInput() == "Validate OK") {

            if (!PlayerPrefs.HasKey("Name")) homeBtn.gameObject.SetActive(true);

            PlayerPrefs.SetString("Name", nameInput.text);

            // getting selected dropdown item
            genderIndex = genderInput.value;
            PlayerPrefs.SetString("Gender", genderInput.options[genderIndex].text);

            if (!string.IsNullOrEmpty(dobInput.text)) PlayerPrefs.SetString("DOB", dobInput.text.Replace(" ", ""));
            if (!string.IsNullOrEmpty(bioInput.text)) PlayerPrefs.SetString("Bio", bioInput.text);
            StartCoroutine(ShowNotif());    // this CRT is fine because I have nothing else after it; doesn't affect anything

        } else {
            errorText.text = ValidateInput();
            errorText.gameObject.SetActive(true);
        }
    }

    IEnumerator ShowNotif() {
        savedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        savedText.gameObject.SetActive(false);
    }

    private void PopulateProfile() {
        nameInput.text = PlayerPrefs.GetString("Name");
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("DOB"))) dobInput.text = PlayerPrefs.GetString("DOB");
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Bio"))) bioInput.text = PlayerPrefs.GetString("Bio");
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Gender"))) {
            switch (PlayerPrefs.GetString("Gender")) {
                case "Prefer not to say": genderInput.value = 0; break;
                case "Male": genderInput.value = 1; break;
                case "Female": genderInput.value = 2; break;
                default: genderInput.value = 3; break;
            }
        }
    }

    private string ValidateInput() {
        // name required
        if (string.IsNullOrEmpty(nameInput.text)) {
            return "Please enter a name.";
        }

        // validate dob format
        if (!string.IsNullOrEmpty(dobInput.text)) {
            dobProcessed = dobInput.text.Replace(" ", "");
            dobArray = dobProcessed.Split("/");

            try {
                // validating day
                if (Int32.Parse(dobArray[0]) > 31) {
                    return "That day doesn't look right...";
                }
                // validating month
                if (Int32.Parse(dobArray[1]) > 12) {
                    return "That month doesn't look right...";
                }
                // validating year - has to be older than 13
                if (Int32.Parse(dobArray[2]) > (DateTime.Now.Year - 13)) {
                    return "You have to be older than 13 to use FriendMi!";
                }
            } catch (Exception) {
                return "Something went wrong with the date!";
            }
            
        }

        return "Validate OK";
    }
}
