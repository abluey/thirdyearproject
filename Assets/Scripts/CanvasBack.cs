using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBack : MonoBehaviour
{
    [SerializeField] private Button backBtn;

    void Start() {
        backBtn.onClick.AddListener(Back);
    }

    private void Back() {
        gameObject.SetActive(false);
    }
}
