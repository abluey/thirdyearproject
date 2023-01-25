using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDay1 : MonoBehaviour {

    void OnEnable() {
        ChatManager.chatbox.text = PlayerChoices.chatRecord;

        if (PlayerPrefs.GetInt("TimeCount") == 0) {
            switch (PlayerChoices.chatProgress) {
                case 0:
                // checking if player profile is complete (not including Bio)
                    if (string.IsNullOrEmpty(PlayerPrefs.GetString("DOB")) ||
                        (PlayerPrefs.GetString("Gender") == "Prefer not to say")) {
                        T0_StartIncomplete();
                    }
                    else {
                        T0_StartComplete();
                    }
                    break;
                case 1: break;
                case 20: T0_Inc1Choice(); break;
                case 30: T0_Inc2(); break;
                case 69: RFPChoice(); break;
                case 40: RFPReply(); break;
                case 50: T0_ComplPreRPF(); break;
                default: Debug.Log("Something went wrong, D1T0."); break;
            }
        } else if (PlayerPrefs.GetInt("TimeCount") == 1) {
            // final opportunity to ask why they don't have their profs completed
            // otherwise ask what's up with the grocery store

            if (!PlayerChoices.friendPrivacy && PlayerChoices.checkedFriendProfile) {
                ChatManager.choice1.gameObject.SetActive(true);
                ChatManager.choice1text.text = "Why do you not have your profile completed?";

                ChatManager.choice1.onClick.AddListener(T1_Confront);

            } else {
                Debug.Log("Switch" + PlayerChoices.chatProgress);

                switch (PlayerChoices.chatProgress) {
                    case 0: T1_Start(); break;
                    case 1: break;
                    case 11: T1_Choice(); break;
                    case 21: T1_ChoiceThink(); break;
                    case 31: break;
                    case 41: break;
                    case 101: T1_GrocQuestions(); break;
                    case 420: T1_Start(); break;
                    default: Debug.Log("Something went wrong, D1T1."); break;
                }
            }
            
        } else if (PlayerPrefs.GetInt("TimeCount") == 2) {
            // switch
            // ask for plant
        } else {
            Debug.Log("Something went wrong");
        }
        
    }

    /***
    TIME 0: INCOMPLETE PROFILE
    ***/

    private void T0_StartIncomplete() {
        ChatManager.chatbox.text = "Day 1\nReese: Why don't you have your profile completed?\n";
        // cannot update chatRecords here or else it'll duplicate it so many times
        
        ChatManager.choice1text.text = "Why do you ask?";
        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T0_Inc);
    }

    private void T0_Inc() {
        ChatManager.ResetListeners();
        PlayerChoices.chatRecord += "\n\n" + ChatManager.chatbox.text;
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why do you ask?\n");

        StartCoroutine(T0_IncReply());
    }

    private IEnumerator T0_IncReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));

        ChatManager.updateChatRecords("\nReese: Oh, just curious. People usually fill them in!\n");
        PlayerChoices.chatProgress = 20;
        T0_Inc1Choice();
    }

    private void T0_Inc1Choice() {
        ChatManager.choice1text.text = "I don't want to.";
        ChatManager.choice2text.text = "I must've forgot. I'll do it later.";
        ChatManager.ShowChoices(true);

        ChatManager.choice1.onClick.AddListener(T0_Inc1Choice1);
        ChatManager.choice2.onClick.AddListener(T0_Inc1Choice2);
    }

    private void T0_Inc1Choice1() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: I don't want to.\n");
        PlayerChoices.willUpdateProfile = 2;

        StartCoroutine(T0_Inc1Choice1Reply());
    }

    private void T0_Inc1Choice2() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: I must've forgot. I'll do it later.\n");
        PlayerChoices.willUpdateProfile = 1;

        StartCoroutine(T0_Inc1Choice2Reply());
    }

    private IEnumerator T0_Inc1Choice1Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(1.5f));

        ChatManager.updateChatRecords("\nReese: That's fair... To each their own!\n");
        PlayerChoices.chatProgress = 30;

        T0_Inc2();
    }

    private IEnumerator T0_Inc1Choice2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(1.5f));

        ChatManager.updateChatRecords("\nReese: Oh, no pressure! I'm looking forward to it though!\n");
        PlayerChoices.chatProgress = 30;

        T0_Inc2();
    }

    private void T0_Inc2() {

        if (PlayerChoices.checkedFriendProfile) {
            ChatManager.choice1text.text = "Why don't you have yours filled in?";
            ChatManager.choice1.gameObject.SetActive(true);
            ChatManager.choice1.onClick.AddListener(T0_IncConfront);
        } else {
            PlayerChoices.chatProgress = 1;
        }
    }

    private void T0_IncConfront() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why don't you have yours filled in?\n");

        StartCoroutine(ReasonsForPrivacy());
    }

    private IEnumerator ReasonsForPrivacy() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f, 1.5f));

        PlayerChoices.friendPrivacy = true;
        ChatManager.updateChatRecords("\nReese: You never know who's reading your details!\n");

        yield return StartCoroutine(ChatManager.IsTyping(2.5f));
        ChatManager.updateChatRecords("\nTheir intentions can't be trusted.\n");
        PlayerChoices.chatProgress = 69;

        RFPChoice();
    }

    private void RFPChoice() {
        ChatManager.choice1text.text = "I see.";
        ChatManager.choice2text.text = "What do you mean?";

        ChatManager.ShowChoices(true);

        ChatManager.choice1.onClick.AddListener(RFPChoice1);
        ChatManager.choice2.onClick.AddListener(RFPChoice2);
    }

    private void RFPChoice1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();
        
        ChatManager.updateChatRecords("\nYou: I see.\n");

        if (PlayerPrefs.GetInt("TimeCount") == 0) {
            PlayerChoices.chatProgress = 1;
        } else if (PlayerPrefs.GetInt("TimeCount") == 1) {
            StartCoroutine(T1_AfterConfront_Start());
        }
    }

    private void RFPChoice2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: What do you mean?\n");

        StartCoroutine(RFPChoice2Reply());
    }

    private IEnumerator RFPChoice2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Literally that! You don't know who's reading your stuff.\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nIf I say the wrong thing and trigger the wrong psycho, they could come after me!\n");
        yield return StartCoroutine(ChatManager.IsTyping(5.0f));
        ChatManager.updateChatRecords("\nDeath threats and stalkers and stuff, yaknow? Not very nice.\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.5f));
        ChatManager.updateChatRecords("\nAnd also companies could sell your data. Identity theft. Yadda yadda.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nAt the end of the day, everyone has something to hide, even if they don't realize it.\n");

        // if the player said they'll update their profile and still hasn't done so
        if (PlayerChoices.willUpdateProfile == 1 &&
            string.IsNullOrEmpty(PlayerPrefs.GetString("DOB")) &&
            (PlayerPrefs.GetString("Gender") == "Prefer not to say")) {
                yield return StartCoroutine(ChatManager.IsTyping(5.0f));
                ChatManager.updateChatRecords("\nReese: Sorry if that puts you off completing the rest of your profile.\n");
            }

        PlayerChoices.chatProgress = 40;
        PlayerChoices.learnPrivacy = true;

        RFPReply();
    }

    private void RFPReply() {
        ChatManager.choice1text.text = "Oh.";
        ChatManager.choice2text.text = "I don't really care though.";

        ChatManager.ShowChoices(true);
        ChatManager.choice1.onClick.AddListener(RFPReplyChoice1);
        ChatManager.choice2.onClick.AddListener(RFPReplyChoice2);
    }

    private void RFPReplyChoice1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Oh.\n");

        StartCoroutine(RFPEnd());
    }

    private void RFPReplyChoice2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: I don't really care though.\n");

        StartCoroutine(RFPEnd2());
    }

    private IEnumerator RFPEnd() {
        yield return StartCoroutine(ChatManager.IsTyping(1.0f));
        ChatManager.updateChatRecords("\nReese: Yep!\n");

        if (PlayerPrefs.GetInt("TimeCount") == 1) {
            yield return StartCoroutine(ChatManager.IsTyping(1.0f));
            ChatManager.updateChatRecords("\nAnyway...!\n");
            PlayerChoices.chatProgress = 420;
            T1_AfterConfront_Start();
        } else {
            PlayerChoices.chatProgress = 1;
        }
    }

    private IEnumerator RFPEnd2() {
        yield return StartCoroutine(ChatManager.IsTyping(1.5f));
        ChatManager.updateChatRecords("\nReese: Well... you should.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nEven the smallest detail can be used to infer what your life.\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nEvery little bit of privacy counts in our hyper-surveillance society.\n");

        if (PlayerPrefs.GetInt("TimeCount") == 1) {
            yield return StartCoroutine(ChatManager.IsTyping(3.0f));
            ChatManager.updateChatRecords("\nAnyway...!\n");
            PlayerChoices.chatProgress = 420;
            T1_AfterConfront_Start();
        } else {
            PlayerChoices.chatProgress = 1;
        }
    }
   

    /***
    TIME 0: COMPLETE PROFILE
    ***/

    private void T0_StartComplete() {
        ChatManager.chatbox.text = "Day 1\nReese: Oh, I see you've filled in your profile!\n";
        
        ChatManager.choice1text.text = "Yeah, I did.";
        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T0_Compl_Reply);

        if (PlayerChoices.checkedFriendProfile) {
            ChatManager.choice2text.text = "Yeah, I did. Why didn't you?";
            ChatManager.choice2.gameObject.SetActive(true);
            ChatManager.choice2.onClick.AddListener(T0_Compl_RFP);
        }
    }

    private void T0_Compl_Reply() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Yeah, I did.\n");
        StartCoroutine(T0_ComplConvo());
    }

    private void T0_Compl_RFP() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Yeah, I did. Why didn't you?\n");

        StartCoroutine(ReasonsForPrivacy());
    }

    private IEnumerator T0_ComplConvo() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Ah, brave soul!\n");
        PlayerChoices.chatProgress = 50;
        
        T0_ComplPreRPF();
    }

    private void T0_ComplPreRPF() {
        ChatManager.choice1text.text = "Is completing your profile not a normal thing?";
        ChatManager.choice1.onClick.AddListener(T0_ComplPreRPF1);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T0_ComplPreRPF1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Is completing your profile not a normal thing?\n");
        
        StartCoroutine(PreRFP());
    }

    private IEnumerator PreRFP() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Oh, it is! Don't worry.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nAlthough you should be careful with what information you give out.\n");
        
        StartCoroutine(ReasonsForPrivacy()); // ELEPHANT hopefully this works
    }


    /***
    TIME 1
    ***/

    // ask why friend's profile incomplete
    private void T1_Confront() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why do you not have your profile completed?\n");

        StartCoroutine(ReasonsForPrivacy());    // found in time 0 incomplete profile
    }

    private IEnumerator T1_AfterConfront_Start() {
        yield return ChatManager.IsTyping(1.5f);
        T1_Start();
    }

    private void T1_Start() {
        if (PlayerChoices.chatProgress == 420) {
            ChatManager.updateChatRecords("\nGrocery store. Weird, huh?\n");
        } else {
            ChatManager.updateChatRecords("\nReese: Grocery store. Weird, huh?\n");
        }
        
        PlayerChoices.chatProgress = 11;

        T1_Choice();
    }

    private void T1_Choice() {
        ChatManager.choice1text.text = "What's wrong with it?";
        ChatManager.choice2text.text = "I've been meaning to ask about it.";
        ChatManager.choice1.onClick.AddListener(T1_Choice1);
        ChatManager.choice2.onClick.AddListener(T1_Choice2);

        ChatManager.ShowChoices(true);
    }

    private void T1_Choice1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: What's wrong with it?\n");
        StartCoroutine(T1_Choice1Reply());
    }

    private IEnumerator T1_Choice1Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: You never think why we only have one grocery store?\n");

        PlayerChoices.chatProgress = 21;

        T1_ChoiceThink();
    }

    private void T1_ChoiceThink() {
        ChatManager.choice1text.text = "No?";
        ChatManager.choice2text.text = "It is kind of weird now that you say it.";
        ChatManager.choice1.onClick.AddListener(T1_ChoiceThink1);
        ChatManager.choice2.onClick.AddListener(T1_ChoiceThink2);

        ChatManager.ShowChoices(true);
    }

    private void T1_ChoiceThink1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: No?\n");

        StartCoroutine(T1_CT1Reply());
    }

    private IEnumerator T1_CT1Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: \n");   //elephant

        PlayerChoices.chatProgress = 31;
    }

    private void T1_ChoiceThink2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: It is kind of weird now that you say it.\n");

        StartCoroutine(T1_CT2Reply());
    }

    private IEnumerator T1_CT2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: \n");   //elephant

        PlayerChoices.chatProgress = 41;
    }

    private void T1_Choice2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: I've been meaning to ask about it.\n");
        StartCoroutine(T1_Choice2Reply());
    }

    private IEnumerator T1_Choice2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nReese: Oh, go ahead! I've lived here a while, I can probably answer your questions.\n");

        PlayerChoices.chatProgress = 101;

        T1_GrocQuestions();
    }

    private void T1_GrocQuestions() {
        ChatManager.choice1text.text = "Does the city only have one store?";
        ChatManager.choice2text.text = "Who owns the store?";

        ChatManager.choice1.onClick.AddListener(T1_GQ1);
        ChatManager.choice2.onClick.AddListener(T1_GQ2);
    }

    private void T1_GQ1() {
        // elephant
    }

    private void T1_GQ2() {
        
    }

}