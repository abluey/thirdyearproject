using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdPage : MonoBehaviour
{   
    [SerializeField] private Image adImage;
    [SerializeField] private TMPro.TMP_Text adText;

    [SerializeField] private Button adBtn;
    [SerializeField] private TMPro.TMP_Text clickedText;
    private string adName;

    void OnEnable() {
        adName = BirthdayManager.clickedName;
        clickedText.gameObject.SetActive(false);
        adBtn.gameObject.SetActive(true);

        switch (adName) {
            case "ad1":
                BirthdayManager.title.text = "Sunglasses B)";
                adText.text = "Are you sick and tired of the humongous fireball in the sky that relentlessly beams down on us all? Well, I'm not. I hardly ever see it. We're in a country that's notorious for having no sun at all. Why sunglasses then? I'm not selling anything. This is all a distraction. Distraction from what, you ask? Well wouldn't you like to know. In the meantime, checkout these sunglasses I bought from another place. They're in perfect condition and - no, I'm not selling them. They're in perfect condition because I've never worn it once. Because I don't need it. Why do I have it? For the flex. And the distraction. Congratulations if you've read up until this point. What are you even doing here?";
                clickedText.text = "Cool button bro! Keep clicking it!";
                break;
            case "ad2":
                BirthdayManager.title.text = "Light Box";
                adText.text = "Light Box is a new and improved box. It is completely opaque and dark from the outside viewing in, but on the inside it is completely bright. This effect dissipates if you open the box to check inside. Please trust in our proudct that it is completely bright inside when the box is closed. Our state-of-the-art technology automatically converts external energies to power the box when it is shut, so no batteries are needed. Invest now and you're sure to never regret it! What does it to, you ask? Why it gives light where you can't, obviously. It makes for a stylish decoration and your family and friends are sure to have already bought one (or more), so why not join in the trend and not be left out? The boxes only come in one color (black).";
                clickedText.text = "Thank you for your sponsorship.";
                break;
            case "ad3":
                BirthdayManager.title.text = "Hot Dogs!";
                adText.text = "Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog! Hot Dogs! Dog Hots! Dogs Hot! Hots Dog!";
                clickedText.text = "Hot Dogs!";
                break;
            case "ad5":
                BirthdayManager.title.text = "The Seancer";
                adText.text = "Need to get in touch with a loved one? Or do you just want a little spooky friend? You've come to the right place! Ghosts, spirits, zombies... you name it, we'll see that it'll happen! Or maybe you want to hire someone to scare the living daylights out of a rival? We can arrange for a poltergeist to haunt their very existence! Simply sign the petition below and you can start funding your own haunted house. Billing happens at the start and end of every month and will fluctuate between two hundred to five hundred pounds depending on services acquired. This is unrefundable and this description is not a promise that a ghost or any other supernatural being will apparate before you.";
                clickedText.text = "Subscribed!";
                break;
            case "quitt":
                BirthdayManager.title.text = "Quitt";
                adText.text = "Are you trying to quit smoking? Maybe a gambling addiction? Or even trying to quit a website? Quitting something can be very discouraging, especially without proper support. Luckily we have our newest app QUITT that gives plenty of support and encouragemenet to quitting things by making you really think about WHY you're quitting the thing you're doing in the first place. Does it not bring you joy? Or is it making a huge dent in your wallet? Or maybe you simply don't have enough time anymore. Regardless, we are here to help, and if you've read up until this point then I hope you are feeling all the encouragement and support you need. Your laptop is completely fine and definitely without viruses; you have a very large wallet and a friendly face and everyone likes you also.";
                adBtn.gameObject.SetActive(false);
                break;
            default: Debug.Log("Something went wrong"); break;
        }

        adImage.sprite = Resources.Load<Sprite>($"Sprites/{adName}");
    }

    void Start()
    {
        adBtn.onClick.AddListener(Click);
    }

    private void Click() {
        PlayerChoices.virusNum += 1;
        StartCoroutine(ClickedCoroutine());
    }

    private IEnumerator ClickedCoroutine() {
        adBtn.gameObject.SetActive(false);
        clickedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        clickedText.gameObject.SetActive(false);
        adBtn.gameObject.SetActive(true);
    }
}
