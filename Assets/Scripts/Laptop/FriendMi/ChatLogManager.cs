using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatLogManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text logbox;

    void OnEnable()
    {
        logbox.text = PlayerChoices.chatRecord;
    }
}
