using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatDay2 : MonoBehaviour {

    private bool showName;

    [SerializeField] private ScrollRect scrollRect;
    
    void OnEnable() {
        showName = true;
        ChatManager.chatbox.text = PlayerChoices.chatRecord;
        scrollRect.normalizedPosition = new Vector2(0, 0);

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
                    ChatManager.chatbox.text = "No new messages.";
                    ChatManager.choice1.gameObject.SetActive(true);
                    ChatManager.choice1text.text = "Hey, you there?";

                    ChatManager.choice1.onClick.AddListener(T1_Start);
                } else {
                    switch (PlayerChoices.chatProgress) {
                        case 1: break;
                        case 21: T1_AskForShop(); break;
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
            // this has to be time 2
            switch (PlayerChoices.chatProgress) {
                case 0: 
                    ChatManager.chatbox.text = "No new messages.";
                    ChatManager.choice1.gameObject.SetActive(true);
                    ChatManager.choice1text.text = "Hey, how are you doing?";
                    ChatManager.choice1.onClick.AddListener(T2_Start);
                    break;
                case 1: break;
                case 22: T2_Choice1(); break;
                case 32: T2_C2(); break;
                case 42: T2_EndChoice(); break;
                case 102: T2_BeBackResponse(); break;
                case 202: T2_ExplainReact(); break;
                case 302: T2_EndChoiceBot(); break;
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

        ChatManager.chatbox.text = "You: Hey, you there?\n";
        PlayerChoices.chatRecord += "\nYou: Hey, you there?\n";

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
            ChatManager.updateChatRecords("\nI'm sure you've seen these sorts of pages before.\n");
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

        ChatManager.updateChatRecords("\nYou: What? How do you know?\n");
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
        ChatManager.ResetListeners();

        PlayerChoices.introducedYourself = true;
        ChatManager.chatbox.text = "You: Hey, how are you doing?\n";
        PlayerChoices.chatRecord = "\nYou: Hey, how are you doing?\n";
        
        ChatManager.ShowChoices(false);

        StartCoroutine(T2_StartReply());
    }

    private IEnumerator T2_StartReply() {
        yield return StartCoroutine(ChatManager.IsTyping(1.0f, 1.0f));
        ChatManager.updateChatRecords("\nReese: Hey there!\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nDoing well. Packing up. Be leaving in a few hours.\n");

        PlayerChoices.chatProgress = 22;
        T2_Choice1();
    }

    private void T2_Choice1() {
        ChatManager.choice1text.text = "You still won't tell me where you're going, are you?";
        ChatManager.choice2text.text = "Will you be back online?";
        ChatManager.choice1.onClick.AddListener(T2_C11);
        ChatManager.choice2.onClick.AddListener(T2_C12);
        ChatManager.ShowChoices(true);
    }

    private void T2_C11() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);
        ChatManager.updateChatRecords("\nYou: You still won't tell me where you're going, are you?\n");

        StartCoroutine(T2_C11Reply());
    }

    private IEnumerator T2_C11Reply() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: No, sorry.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.5f,1.0f));
        ChatManager.updateChatRecords("\n...I'll tell you this. I'm going to try and find my other friends.\n");
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nMy in-person friends, who've all stopped using online services one by one.\n");

        PlayerChoices.chatProgress = 32;
        T2_C2();
    }

    private void T2_C2() {
        ChatManager.choice1text.text = "I see. Stay safe.";
        ChatManager.choice1.onClick.AddListener(T2_C21);
        ChatManager.choice2text.text = "Will you be back online?";
        ChatManager.choice2.onClick.AddListener(T2_C12);
        ChatManager.ShowChoices(true);
    }

    private void T2_C21() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);
        ChatManager.updateChatRecords("\nYou: I see. Stay safe.\n");

        StartCoroutine(T2_BringChange());
    }

    private IEnumerator T2_BringChange() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        
        // ELEPHANT needs testing
        // redirecting from "Stay safe."
        if (PlayerChoices.chatProgress == 32) {
            ChatManager.updateChatRecords("\nReese: Thank you. You too.\n");
        }
        // redirecting from robot
        else if (PlayerChoices.chatProgress == 202) {
            ChatManager.updateChatRecords("\nAnyway.\n");
        }
        
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nKeep your eyes peeled for more tricks on the Internet.\n");
        yield return StartCoroutine(ChatManager.IsTyping(1.5f,1.0f));
        ChatManager.updateChatRecords("\nBring awareness and change.\n");

        PlayerChoices.chatProgress = 42;
        T2_EndChoice();
    }

    private void T2_C12() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);
        ChatManager.updateChatRecords("\nYou: Will you be back online?\n");

        StartCoroutine(T2_BeBack());
    }

    private IEnumerator T2_BeBack() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Depends.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nIf they find someone to replace me, then 'I'll' be back.\n");

        PlayerChoices.chatProgress = 102;
        T2_BeBackResponse();
    }

    private void T2_BeBackResponse() {
        ChatManager.choice1text.text = "That's not ominous at all. What do you mean?";
        ChatManager.choice1.onClick.AddListener(T2_BBR);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T2_BBR() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);
        ChatManager.updateChatRecords("\nYou: That's not ominous at all. What do you mean?\n");

        StartCoroutine(T2_Explain());
    }

    private IEnumerator T2_Explain() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f,1.5f));
        ChatManager.updateChatRecords("\nReese: Well. Every account I used to talk to here has been replaced by bots.\n");
        yield return StartCoroutine(ChatManager.IsTyping(2.0f,2.0f));
        ChatManager.updateChatRecords("\nI expect I will be, too. Replaced, I mean.\n");

        PlayerChoices.chatProgress = 202;
        T2_ExplainReact();
    }

    private void T2_ExplainReact() {
        ChatManager.choice1text.text = "Is that why you said not many people use FriendMi now?";
        ChatManager.choice1.onClick.AddListener(T2_ERDisplay);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T2_ERDisplay() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);
        ChatManager.updateChatRecords("\nYou: Is that why you said not many people use FriendMi now?\n");

        StartCoroutine(T2_ExplainEnd());
    }

    private IEnumerator T2_ExplainEnd() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: Yeah.\n");

        if (PlayerChoices.D1T1Ending == 2 || PlayerChoices.D1T1Ending == 3) {
            PlayerChoices.chatProgress = 302;
            T2_EndChoiceBot();
        } else {
            T2_BringChange();
        }
    }

    private void T2_EndChoiceBot() {
        ChatManager.choice1text.text = "How do you know I'm not just a really advanced bot?";
        ChatManager.choice1.onClick.AddListener(T2_ECBDisplay);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T2_ECBDisplay() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);
        ChatManager.updateChatRecords("\nYou: How do you know I'm not just a really advanced bot?\n");

        StartCoroutine(T2_BotReply());
    }

    private IEnumerator T2_BotReply() {
        yield return StartCoroutine(ChatManager.IsTyping(3.0f));
        ChatManager.updateChatRecords("\nReese: Then I hope you're not like the rest of them and can influence the future with your awareness.\n");

        PlayerChoices.chatProgress = 42;
        T2_EndChoice();
    }

    private void T2_EndChoice() {
        ChatManager.choice1text.text = "This is sounding increasingly dystopian.";
        ChatManager.choice1.onClick.AddListener(T2_EndFinal);
        ChatManager.choice1.gameObject.SetActive(true);
    }

    private void T2_EndFinal() {
        ChatManager.ResetListeners();
        ChatManager.ShowChoices(false);
        ChatManager.updateChatRecords("\nYou: This is sounding increasingly dystopian.\n");

        StartCoroutine(T2_EndFinal1());
    }

    private IEnumerator T2_EndFinal1() {
        yield return StartCoroutine(ChatManager.IsTyping(2.0f));
        ChatManager.updateChatRecords("\nReese: No one ever said it wasn't.\n");

        PlayerChoices.chatProgress = 1;
    }
}