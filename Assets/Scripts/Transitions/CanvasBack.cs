using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBack : MonoBehaviour
{
    [SerializeField] private Button backBtn;

    void Start() {
        backBtn?.onClick.AddListener(Back);
    }

    public void Back() {
        gameObject.SetActive(false);
    }
}
