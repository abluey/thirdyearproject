using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDay2 : MonoBehaviour {

    private bool showName;
    
    void OnEnable() {
        showName = true;
        ChatManager.chatbox.text = PlayerChoices.chatRecord;

        if (PlayerPrefs.GetInt("TimeCount") == 0) {
            switch (PlayerChoices.chatProgress) {
                case 0: T0_Start(); break;
                case 1: break;
                case 2: T0_WhereToWhy(); break;
                case 3: T0_Who(); break;
                default: Debug.Log("Something went wrong D2T0"); break;
            }
        } else if (PlayerPrefs.GetInt("TimeCount") == 1) {

            // bought a present
            if (PlayerChoices.buyPresent) {

                // no chat yet
                if (PlayerChoices.chatProgress == 0) {
                    ChatManager.chatbox.text = "No new messages";
                    ChatManager.choice1.gameObject.SetActive(true);
                    ChatManager.choice1text.text = "Hey, you there?";

                    ChatManager.choice1.onClick.AddListener(T1_Start);
                } else {
                    switch (PlayerChoices.chatProgress) {
                        case 1: break;
                        case 21: T1_Shop(); break;
                        case 31: T1_ShopChoice(); break;
                        case 41: PostDisguisedAds(); break;
                        case 51: End(); break;
                        default: Debug.Log("Something went wrong D2T1"); break;
                    }
                }
            } else {
                // didn't buy a present, shouldn't display anything
            }
            
        } else {
            switch (PlayerChoices.chatProgress) {
                case 0: T2_Start(); break;
                case 1: break;
                default: Debug.Log("Something went wrong D2T2"); break;
            }
        }
    }

    /***
    TIME 0
    ***/

    private void T0_Start() {
        ChatManager.chatbox.text = "Day 2\nReese: It's been a pleasure talking with you. You won't see me after tomorrow.\n";

        ChatManager.choice1text.text = "Why not?";
        ChatManager.choice2text.text = "Where are you going?";

        ChatManager.choice1.onClick.AddListener(T0_Why);
        ChatManager.choice2.onClick.AddListener(T0_Where);
        ChatManager.ShowChoices(true);
    }

    private void T0_Why() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why not?\n");
        StartCoroutine(T0_Leave());
    }
    
    private void T0_Where() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Where are you going?\n");
        StartCoroutine(T0_WhereReply());
    }

    private IEnumerator T0_WhereReply() {
        yield return StartCoroutine(ChatManager.IsTyping(1.0f));
        ChatManager.updateChatRecords("\nReese: Away.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nCan't say much.\n");

        PlayerChoices.chatProgress = 2;
        T0_WhereToWhy();
    }

    private void T0_WhereToWhy() {
        ChatManager.choice1text.text = "Why?";
        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T0_WhereToWhyBridge);
    }

    private void T0_WhereToWhyBridge() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Why?\n");
        StartCoroutine(T0_Leave());
    }

    private IEnumerator T0_Leave() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Because they found me.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nI'm sorry I can't tell you more.\n");

        PlayerChoices.chatProgress = 3;
        T0_Who();
    }

    private void T0_Who() {
        ChatManager.choice1text.text = "Who found you?";
        ChatManager.choice1.gameObject.SetActive(true);
        ChatManager.choice1.onClick.AddListener(T0_WhoReply);
    }

    private void T0_WhoReply() {
        ChatManager.updateChatRecords("\nYou: Who found you?\n");
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        PlayerChoices.chatProgress = 1;
    }

    /***
    TIME 1
    ***/
    
    private void T1_Start() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Hey, you there?\n");

        StartCoroutine(T1_Reply());
    }

    private IEnumerator T1_Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Yeah, for now. What's up?\n");
        
        PlayerChoices.chatProgress = 21;
        T1_AskForShop();
    }

    private void T1_AskForShop() {
        ChatManager.choice1text.text = "Is there anything you can share about the birthday shop?";
        ChatManager.choice1.onClick.AddListener(T1_AskForShop1);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T1_AskForShop1() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);
        ChatManager.updateChatRecords("\nYou: Is there anything you can share about the birthday shop?\n");  

        StartCoroutine(T1_AskForShopReply());
    }

    private IEnumerator T1_AskForShopReply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Heh, might as well.\n");
        yield return StartCoroutine(ChatManager.IsTyping(1.0f, 1.0f));
        ChatManager.updateChatRecords("\nWhat are your first impressions of it?\n");

        PlayerChoices.chatProgress = 31;
        T1_ShopChoice();
    }

    private void T1_ShopChoice() {
        ChatManager.choice1text.text = "There's way too many ads!";
        ChatManager.choice2text.text = "Hmm. Not sure; how about you tell me?";
        ChatManager.choice1.onClick.AddListener(T1_ShopChoice1);
        ChatManager.choice2.onClick.AddListener(T1_ShopChoice2);

        ChatManager.ShowChoices(true);
    }

    private void T1_ShopChoice1() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: There's way too many ads!\n");
        StartCoroutine(DisguisedAds());
    }

    private void T1_ShopChoice2() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Hmm. Not sure; how about you tell me?\n");
        StartCoroutine(PreDisguisedAds());
    }

    private IEnumerator PreDisguisedAds() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f,1.0f));
        ChatManager.updateChatRecords("\nReese: You're right. I don't have much time.\n");
        showName = false;
        StartCoroutine(DisguisedAds());
    }

    private IEnumerator DisguisedAds() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        if (showName) {
            ChatManager.updateChatRecords("\nReese: I'm sure you've seen these sorts of pages before.\n");
        } else {
            ChatManager.updateChatRecords("\nI'm sure you've seen these sorts of pages before.\n")
            showName = true;
        }

        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nThey're called 'disguised ads'. Ads that are disguised as something else to get you to click on them.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nSometimes they're involved in click fraud - where the owner of the ads are paid based on the number of clicks.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.5f));
        ChatManager.updateChatRecords("\nAnd if you're really unlucky, you could get viruses from them.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f,1.0f));

        // how many false ads the user clicked
        if (PlayerChoices.virusNum == 0) {
            ChatManager.updateChatRecords("\nYou don't seem to have any, so that's good.\n");
        } else if (PlayerChoices.virusNum == 1) {
            ChatManager.updateChatRecords("\nOh, seems like you have a virus.\n");
        } else {
            ChatManager.updateChatRecords("\nOh, seems like you have " + PlayerChoices.virusNum + " viruses.\n");
        }

        PlayerChoices.chatProgress = 41;
        PostDisguisedAds();
    }

    private void PostDisguisedAds() {
        ChatManager.choice1text.text = "What? How do you know?";
        
        ChatManager.choice1.onClick.AddListener(PostDisguisedAds1);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void PostDisguisedAds1() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: What? How do you know?");
        StartCoroutine(RidVirus());
    }

    private IEnumerator RidVirus() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: It doesn't matter.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));

        if (PlayerChoices.virusNum != 0) {
            ChatManager.updateChatRecords("\nYou should scan your laptop to get rid of the viruses, though.\n");
            yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        }
        
        ChatManager.updateChatRecords("\nMake sure you have some antivirus, and scan your computer regularly.\n");

        PlayerChoices.chatProgress = 51;    
        End();
    }

    private void End() {
        ChatManager.choice1text.text = "Thanks, I'll do that. Talk to you later?";
        ChatManager.choice1.onClick.AddListener(End2);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void End2() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);

        ChatManager.updateChatRecords("\nYou: Thanks, I'll do that. Talk to you later?");

        PlayerChoices.chatProgress = 1;
    }


    /***
    TIME 2
    ***/
    
    private void T2_Start() {

    }
}