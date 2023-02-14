using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDay1 : MonoBehaviour {

    private bool showName;

    void OnEnable() {
        ChatManager.chatbox.text = PlayerChoices.chatRecord;
        showName = false;

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
            
            // NOTHING can happen if you haven't shopped yet
            if (!PlayerChoices.groceryDone) {
                // do nothing
            }

            // final opportunity to ask why they don't have their profs completed
            // otherwise ask what's up with the grocery store
            else if (!PlayerChoices.friendPrivacy && PlayerChoices.checkedFriendProfile) {
                ChatManager.choice1.gameObject.SetActive(true);
                ChatManager.choice1text.text = "Why do you not have your profile completed?";

                ChatManager.choice1.onClick.AddListener(T1_Confront);

            } else {

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
            
            // nothing should happen before buying a plant
            if (PlayerChoices.plantType == "") {
                // do nothing
            } else {
                switch (PlayerChoices.chatProgress) {
                    case 0: T2_Start(); break;
                    case 1: break;
                    case 12: DPP1(); break;
                    case 13: WhatIsDP(); break;
                    case 201: T2_C2What(); break;
                    case 202: T2_C2PrePermission(); break;
                    case 203: T2_PostPermission(); break;
                    default: Debug.Log("Something went wrong, D1T2."); break;
                }
            }
            
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
        // elephant: NEEDS TESTING
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
        ChatManager.updateChatRecords("\nEven the smallest detail can be used to infer what your life is like.\n");
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
            // elephant - hopefully works
            ChatManager.chatbox.text = "Day 1\nReese: Grocery store. Weird, huh?\n";
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
        ChatManager.updateChatRecords("\nReese: Yep. The city runs it.\n");

        PlayerChoices.chatProgress = 102;
        T1_GQPt2();
    }

    private IEnumerator T1_GQ2Ans() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: The city owns and runs it. It's the only store in town.\n");

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
        ChatManager.updateChatRecords("\nReese: No worries.");

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
        ChatManager.updateChatRecords("\nBut no one remembers.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f,2.0f));
        ChatManager.updateChatRecords("\nDo you have other questions?\n");

        PlayerChoices.chatProgress = 103;
        T1_GQPt3();
    }

    private void T1_GQPt3() {
        ChatManager.choice1text.text = "Why does the store not have product prices?";
        ChatManager.choice2text.text = "That's all, thanks.";

        ChatManager.ShowChoices(true);
        ChatManager.choice1.onClick.AddListener(T1_GQPt3_1);
        ChatManager.choice2.onClick.AddListener(T1_GQEnd);
    }

    private void T1_GQPt3_1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Why does the store not have product prices?\n");
        StartCoroutine(T1_GQPt3Reply());
    }

    private IEnumerator T1_GQPt3Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: Well, originally it was to prevent price comparison between stores.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nThat's an example of a 'dark pattern', by the way. Techniques that try to trick you into doing things you didn't mean to.\n");
        yield return StartCoroutine(ChatManager.IsTyping(5.0f));
        ChatManager.updateChatRecords("\nBut the price comparison doesn't matter now, since it's the only store in town anyway.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nNow, the prices are gone in the hopes that you overspend.\n");

        yield return StartCoroutine(ChatManager.IsTyping(2.0f,2.0f));
        ChatManager.updateChatRecords("\nDo you have other questions?\n");

        PlayerChoices.chatProgress = 104;
        PlayerChoices.learnDPDef = true;

        T1_GQPt4();   
    }

    private void T1_GQPt4() {
        ChatManager.choice1text.text = "That's all, thanks.";

        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T1_GQEnd);
    }

    /***
    TIME 2
    ***/

    private void T2_Start() {
        // elephant - hopefully works
        ChatManager.chatbox.text = "\nReese: You got a plant??\n";

        ChatManager.choice1text.text = "Yeah? Is something wrong?";
        ChatManager.choice2text.text = "How did you know?";

        ChatManager.choice1.onClick.AddListener(T2_C1PreDP);
        ChatManager.choice2.onClick.AddListener(T2_C2);
        ChatManager.ShowChoices(true);
    }

    private void T2_C1PreDP() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Yeah? Is something wrong?\n");

        StartCoroutine(DarkPatternPlant());
    }

    private IEnumerator DarkPatternPlant() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: You know that website is... quite interesting?\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.5f));
        if (PlayerChoices.buyPremiumPlant) {
            ChatManager.updateChatRecords("\nYou got a premium plant... Was that on purpose?\n");
        } else {
            ChatManager.updateChatRecords("\nI see you didn't get a premium plant, so you must've caught on to their lies.\n");
        }

        PlayerChoices.chatProgress = 12;
        DPP1();
    }

    private void DPP1() {
        if (PlayerChoices.buyPremiumPlant) {
            ChatManager.choice1text.text = "Yes.";
            ChatManager.choice2text.text = "No.";

            ChatManager.choice1.onClick.AddListener(DPPPremY);
            ChatManager.choice2.onClick.AddListener(DPPPremN);
        } else {
            ChatManager.choice1text.text = "Yeah. I noticed the price increase so I double-checked.";
            ChatManager.choice2text.text = "Yeah. I saw the option to continue without an upgrade.";

            ChatManager.choice1.onClick.AddListener(DPPNoPrem1);
            ChatManager.choice2.onClick.AddListener(DPPNoPrem2);
        }

        ChatManager.ShowChoices(true);
    }

    private void DPPPremY() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Yes.\n");
        StartCoroutine(DPPPremYReply());
    }

    private IEnumerator DPPPremYReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: ...Oh.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nWell, it really wasn't worth it in my opinion, but you do you.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nAt least you're well-versed in dark pattern misdirection, then!\n");

        PlayerChoices.chatProgress = 13;
        WhatIsDP();
    }

    private void WhatIsDP() {
        ChatManager.choice1text.text = "What do you mean by misdirection?";
        ChatManager.choice1.onClick.AddListener(PreLearnDPPlant);

        ChatManager.choice2text.text = "Could you remind me what dark patterns are again?";
        ChatManager.choice2.onClick.AddListener(PreLearnDP);
        
        ChatManager.ShowChoices(true);
    }

    private void PreLearnDPPlant() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: What do you mean by misdirection?\n");
        StartCoroutine(LearnDPPlant());
    }

    private IEnumerator LearnDPPlant() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        if (showName) {
            ChatManager.updateChatRecords("\nReese: Misdirection is a type of dark pattern, and hides what's actually going on.\n");
            showName = false;
        } else {
            ChatManager.updateChatRecords("\nMisdirection is a type of dark pattern, and hides what's actually going on.\n");
        }
        
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nIn the case of the plant store, if you don't pay attention, you're automatically charged extra.\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nThey used to do something even sneakier, which is sneaking things into your checkout cart, but that's illegal now.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f, 1.0f));
        ChatManager.updateChatRecords("\nThere's another thing they do, though, which are trick questions: questions designed to be confusing, so you give an answer they want.\n");
        yield return StartCoroutine(ChatManager.IsTyping(4.0f));
        ChatManager.updateChatRecords("\nYou can see it at the checkout page when they ask if you want to receive ads from them.\n");

        PlayerChoices.learnDPPlant = true;
        PlayerChoices.chatProgress = 14;
        PostDPPlant();
    }

    private void PostDPPlant() {
        if (PlayerChoices.receivePlantPromo) {
            ChatManager.choice1text.text = "Ah... I didn't pick up on that.";
            ChatManager.choice2text.text = "I see. I saw it but still chose to receive the ads.";

            ChatManager.choice1.onClick.AddListener(T2_PreEnd1);
            ChatManager.choice2.onClick.AddListener(T2_PreEnd2);
            ChatManager.ShowChoices(true);

        } else {
            ChatManager.choice1text.text = "I see. I did pick up on it and unticked the box.";

            ChatManager.choice1.onClick.AddListener(T2_PreEnd3);
            ChatManager.choice1.gameObject.SetActive(true);
        }
    }

    private void T2_PreEnd1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Ah... I didn't pick up on that.\n");
        StartCoroutine(T2_End1());
    }

    private void T2_PreEnd2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: I see. I saw it but still chose to receive the ads.\n");
        StartCoroutine(T2_End1());
    }

    private void T2_PreEnd3() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: I see. I did pick up on it and unticked the box.\n");
        StartCoroutine(T2_End2());
    }

    private IEnumerator T2_End1() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Ahh. Well, regardless!\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nNow you know for next time, too!\n");

        // elephant: ending code?
        PlayerChoices.chatProgress = 1;
    }

    private IEnumerator T2_End2() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: Good! Attention to detail is a crucial skill to combat these patterns.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f,1.0f));
        ChatManager.updateChatRecords("\nIt's a good skill to have in general, actually.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nKeep it up!\n");

        // elephant: ending code?
        PlayerChoices.chatProgress = 1;
    }

    private void PreLearnDP() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Could you remind me what dark patterns are again?\n");
        StartCoroutine(LearnDP());
    }

    private IEnumerator LearnDP() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: Dark patterns are tricks to get you to do things you don't mean to.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nThere's many types, and misdirection is one of them.\n");
        showName = true;
        PlayerChoices.learnDPDef = true;

        // elephant HOPE THIS WORKS
        StartCoroutine(LearnDPPlant());
    }

    private void DPPPremN() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: No.\n");

        StartCoroutine(DPPPremNReply());
    }

    private IEnumerator DPPPremNReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: ...Oh.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nWell, that's unlucky.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nThey use a dark pattern technique called 'misdirection.'\n");

        PlayerChoices.chatProgress = 13;
        WhatIsDP();
    }

    private void DPPNoPrem1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Yeah. I noticed the price increase so I double-checked.\n");

        StartCoroutine(DPPNoPremReply());
    }

    private void DPPNoPrem2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Yeah. I saw the option to continue without an upgrade.\n");

        StartCoroutine(DPPNoPremReply());
    }

    private IEnumerator DPPNoPremReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Good, you got sharp eyes!\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nThat's a form of dark pattern called misdirection!\n");

        PlayerChoices.chatProgress = 13;

        WhatIsDP();
    }

    private void T2_C2() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: How did you know?\n");

        StartCoroutine(T2_C2Reply());
    }

    private IEnumerator T2_C2Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: They announced it on their social media.\n");
        PlayerChoices.chatProgress = 201;
        T2_C2What();
    }

    private void T2_C2What() {
        ChatManager.choice1text.text = "What?";
        ChatManager.choice1.onClick.AddListener(T2_C2What1);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T2_C2What1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();
        ChatManager.updateChatRecords("\nYou: What?\n");
        StartCoroutine(T2_C2WhatReply());
    }

    private IEnumerator T2_C2WhatReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Yeah, they announce every purchase.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nThey didn't tag you, but I recognized your name.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.5f));
        ChatManager.updateChatRecords("\nThere can't be that many " + PlayerPrefs.GetString("Name") + "s after all!\n");

        PlayerChoices.chatProgress = 202;
        T2_C2PrePermission();
    }

    private void T2_C2PrePermission() {
        ChatManager.choice1text.text = "Is that allowed?";
        ChatManager.choice1.onClick.AddListener(T2_C2PrePer1);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T2_C2PrePer1() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Is that allowed?\n");
        StartCoroutine(Permission());
    }

    private IEnumerator Permission() {
        yield return StartCoroutine(ChatManager.IsTyping(1.0f));
        ChatManager.updateChatRecords("\nReese: Yeah, why not?\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nIt's in their terms and conditions.\n");

        PlayerChoices.chatProgress = 203;
        T2_PostPermission();
    }

    private void T2_PostPermission() {
        ChatManager.choice1text.text = "Who reads that anymore?";
        ChatManager.choice2text.text = "Oh right, I remember reading that.";
        ChatManager.choice1.onClick.AddListener(T2_PermNoRead);
        ChatManager.choice2.onClick.AddListener(T2_PermRead);
        ChatManager.ShowChoices(true);
    }

    private void T2_PermNoRead() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Who reads that anymore?\n");
        StartCoroutine(T2_PermNoReadReply());
    }

    private IEnumerator T2_PermNoReadReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: You do have a point, but you gotta be careful.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nThough I suppose they fill it up with so much legal jargon it's deliberately difficult to understand anyway.\n");
        yield return StartCoroutine(ChatManager.IsTyping(1.0f));
        
        PlayerChoices.readTCs = 2;
        StartCoroutine(DPPBridge());
    }

    private void T2_PermRead() {
        ChatManager.ShowChoices(false);
        ChatManager.ResetListeners();

        ChatManager.updateChatRecords("\nYou: Oh right, I remember reading that.\n");
        StartCoroutine(T2_PermReadReply());
    }

    private IEnumerator T2_PermReadReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Oh, you actually read that??");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nYou have to be the second person I know who actually read it.\n");
        yield return StartCoroutine(ChatManager.IsTyping(1.5f));
        ChatManager.updateChatRecords("\nThe first person being me, of course :)");

        PlayerChoices.readTCs = 1;
        StartCoroutine(DPPBridge());
    }

    private IEnumerator DPPBridge() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nMan, dark patterns are all over the place these days.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nWhat you really want to look out for is the store's misdirection though.\n");

        PlayerChoices.chatProgress = 13;
        WhatIsDP();
    }

}