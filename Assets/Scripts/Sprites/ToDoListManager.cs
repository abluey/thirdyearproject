using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToDoListManager : MonoBehaviour
{
    public TMPro.TMP_Text listContent;

    private int day;
    private int time;
    private string todo;

    // Start is called before the first frame update
    void Start()
    {
        day = PlayerPrefs.GetInt("DayCount");
        time = PlayerPrefs.GetInt("TimeCount");
        switch (day) {
            case 0: todo = "- Set up FriendMi profile"; break;
            case 1: case 2: todo = "- Any new friends?"; break;
            default: todo = "Nothing for now."; break;
        }

        if (day == 0) {
            if (time >= 1) {
                todo += "\n\n- Check fridge (buy groceries?)";
            }
            if (time == 2) {
                todo += "\n\n- Chill";
            }
        } else if (day == 1) {
            if (time >= 1) {
                todo += "\n\n- Buy a plant!";
            }
            // if (time == 2) {
            //     todo += "\n\n- Buy snacks";
            // }
        } else if (day == 2) {
            if (time >= 1) {
                todo += "\n\n- Buy a present for mom";
            }
            if (time == 2) {
                todo += "\n\n- Get milk";
            }
        }

        listContent.text = todo;
    }
}
