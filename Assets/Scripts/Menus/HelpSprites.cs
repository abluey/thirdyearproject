using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpSprites : MonoBehaviour
{   
    private TMPro.TMP_Text text;

    void Start() {
        text = gameObject.GetComponentInChildren<TMPro.TMP_Text>(true);
    }

    public void OnMouseOver()
    {
        text.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        text.gameObject.SetActive(false);
    }
}
