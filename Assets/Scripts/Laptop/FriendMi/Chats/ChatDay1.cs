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
                    case 31: T1_CT1PreEnd(); break;
                    case 41: T1_CT2PreEnd(); break;
                    case 51: T1_CT2End12(); break;
                    case 101: T1_GrocQuestions(); break;
                    case 102: T1_GQPt2(); break;
                    case 103: T1_GQPt3(); break;
                    case 104: T1_GQPt4(); break;
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
                ChatManager.updateChatRecords("\nSorry if that puts you off completing the rest of your profile.\n");
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

        // if player profile complete
        // NEEDS TESTING
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("DOB")) && (PlayerPrefs.GetString("Gender") != "Prefer not to say")) {
            yield return StartCoroutine(RFPExtra());
        }

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
        ChatManager.updateChatRecords("\nReese: Well... you should!\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nEven the smallest detail can be used to infer what your life.\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nEvery little bit of privacy counts in our hyper-surveillance society.\n");

        // if player profile complete
        // NEEDS TESTING
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("DOB")) && (PlayerPrefs.GetString("Gender") != "Prefer not to say")) {
            yield return StartCoroutine(RFPExtra());
        }

        if (PlayerPrefs.GetInt("TimeCount") == 1) {
            yield return StartCoroutine(ChatManager.IsTyping(3.0f));
            ChatManager.updateChatRecords("\nAnyway...\n");
            PlayerChoices.chatProgress = 420;
            T1_AfterConfront_Start();
        } else {
            PlayerChoices.chatProgress = 1;
        }
    }

    private IEnumerator RFPExtra() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nOf course, if the stuff in your profile is fake, then... hehe!\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nDon't tell me if it is or not, though! That ruins the surprise.\n");
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
        ChatManager.choice1text.text = "Not really. I think it's fine.";
        ChatManager.choice2text.text = "It is kind of weird now that you say it.";
        ChatManager.choice1.onClick.AddListener(T1_ChoiceThink1);
        ChatManager.choice2.onClick.AddListener(T1_ChoiceThink2);

        ChatManager.ShowChoices(true);
    }

    private void T1_ChoiceThink1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Not really. I think it's fine.\n");

        StartCoroutine(T1_CT1Reply());
    }

    private IEnumerator T1_CT1Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: Really? Is it like this in other places too?\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.5f));
        ChatManager.updateChatRecords("\nI always thought some terrible disaster ate the other choices.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nBut maybe it's just always been like this.\n");

        PlayerChoices.chatProgress = 31;
        T1_CT1PreEnd();
    }

    private void T1_CT1PreEnd() {
        ChatManager.choice1text.text = "I suppose so.";
        ChatManager.choice1.onClick.AddListener(T1_CT1End);

        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T1_CT1End() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: I suppose so.\n");
        PlayerChoices.D1T1Ending = 1;
        PlayerChoices.chatProgress = 1;
    }

    private void T1_ChoiceThink2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: It is kind of weird now that you say it.\n");
        StartCoroutine(T1_CT2Reply());
    }

    private IEnumerator T1_CT2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: I know, right?? I'm glad I'm not the only one who sees this!\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.5f));
        ChatManager.updateChatRecords("\nI swear other shops existed before. You remember too, right?\n");

        PlayerChoices.chatProgress = 41;
        T1_CT2PreEnd();
    }

    private void T1_CT2PreEnd() {
        ChatManager.choice1text.text = "Yes.";
        ChatManager.choice1.onClick.AddListener(T1_CT2PreEnd1);
        ChatManager.choice2text.text = "No.";
        ChatManager.choice2.onClick.AddListener(T1_CT2PreEnd2);

        ChatManager.ShowChoices(true);
    }

    private void T1_CT2PreEnd1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Yes.\n");
        StartCoroutine(T1_CT2End1());
    }

    private IEnumerator T1_CT2End1() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Yes! I'm not the only one!\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nMy other friends only mention The Grocery Store. Nothing else.\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nIt's like they're programmed to say one thing only.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nBut now at least I know you're a real person :)\n");

        PlayerChoices.chatProgress = 51;

        T1_CT2End12();
    }

    private void T1_CT2End12() {
        ChatManager.choice1text.text = "Of course I am.";
        ChatManager.choice1.onClick.AddListener(T1_CT2End3);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T1_CT2End3() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Of course I am.\n");

        StartCoroutine(T1_CT2EndEnd());
    }

    private IEnumerator T1_CT2EndEnd() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Of course, of course!\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.5f));
        ChatManager.updateChatRecords("\nAbsolutely no reason for you not to be.\n");

        PlayerChoices.D1T1Ending = 2;
        PlayerChoices.chatProgress = 1;
    }

    private void T1_CT2PreEnd2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: No.\n");
        StartCoroutine(T1_CT2End2());
    }

    private IEnumerator T1_CT2End2() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Oh, haha, very funny.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nOther chains existed. I know it.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nYou know it too.\n");

        PlayerChoices.D1T1Ending = 3;
        PlayerChoices.chatProgress = 1;
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

        ChatManager.ShowChoices(true);
    }

    private void T1_GQ1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Does the city only have one store?\n");

        StartCoroutine(T1_GQ1Ans());
    }

    private void T1_GQ2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Who owns the store?\n");

        StartCoroutine(T1_GQ2Ans());
    }

    private IEnumerator T1_GQ1Ans() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: Yep. The government runs it.\n");

        PlayerChoices.chatProgress = 102;
        T1_GQPt2();
    }

    private IEnumerator T1_GQ2Ans() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: The government runs it. It's the only store in town.\n");

        PlayerChoices.chatProgress = 102;
        T1_GQPt2();
    }

    private void T1_GQPt2() {
        ChatManager.choice1text.text = "What happened to the other stores?";
        ChatManager.choice2text.text = "That's all, thanks.";

        ChatManager.ShowChoices(true);
        ChatManager.choice1.onClick.AddListener(T1_GQPt2_1);
        ChatManager.choice2.onClick.AddListener(T1_GQEnd);
    }

    private void T1_GQEnd() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: That's all, thanks.\n");
        StartCoroutine(T1_GQEndReply());
    }

    private IEnumerator T1_GQEndReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: No worries!");

        PlayerChoices.D1T1Ending = 4;
        PlayerChoices.chatProgress = 1;
    }

    private void T1_GQPt2_1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: What happened to the other stores?\n");
        StartCoroutine(T1_GQPt2Reply());
    }

    private IEnumerator T1_GQPt2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: I have no idea.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nBest I can tell, they all got bought out or something.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nBut, no one remembers.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f,2.0f));
        ChatManager.updateChatRecords("\nDo you have other questions?\n");

        PlayerChoices.chatProgress = 103;
        T1_GQPt3();
    }

    private void T1_GQPt3() {
        ChatManager.choice1text.text = "Is the store evil?";
        ChatManager.choice2text.text = "That's all, thanks.";

        ChatManager.ShowChoices(true);
        ChatManager.choice1.onClick.AddListener(T1_GQPt3_1);
        ChatManager.choice2.onClick.AddListener(T1_GQEnd);
    }

    private void T1_GQPt3_1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Is the store evil?\n");
        StartCoroutine(T1_GQPt3Reply());
    }

    private IEnumerator T1_GQPt3Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f, 1.5f));
        ChatManager.updateChatRecords("\nReese: HAHA! That's the funniest thing I've read in a while.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        yield return StartCoroutine(ChatManager.IsTyping(1.0f, 2.0f));
        ChatManager.updateChatRecords("\nNo, it's not.\n");

        yield return StartCoroutine(ChatManager.IsTyping(2.0f,2.0f));
        ChatManager.updateChatRecords("\nDo you have other questions?\n");

        T1_GQPt4();   
    }

    private void T1_GQPt4() {
        ChatManager.choice1text.text = "That's all, thanks.";

        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T1_GQEnd);
    }

}