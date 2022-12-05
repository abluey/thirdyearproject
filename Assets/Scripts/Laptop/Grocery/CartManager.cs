using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CartManager : MonoBehaviour
{ 

    [SerializeField] private Button checkoutBtn;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    [SerializeField] private Canvas purchasedPage;

    public static Dictionary<string, int> cart = new Dictionary<string, int>();

    void OnEnable() {
        Deny();
    }

    void Start() {
        checkoutBtn.onClick.AddListener(Checkout);
        yesBtn.onClick.AddListener(Confirm);
        noBtn.onClick.AddListener(Deny);

        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
    }

    public static void AddFood(string food) {
        if (cart.ContainsKey(food)) {       // if the cart already has the food in it
            cart[food] = cart[food] + 1;    // find add 1 to the existing value
        } else {
            cart.Add(food, 1);              // cart has a new food
        }
    }

    public static void SubtractFood(string food) {
        if (cart[food] > 1) {               // if the cart has 2 or more foods
            cart[food] = cart[food] - 1;    // minimum number is 1
        } else {                            // if the cart has 1 food (should not be able to subtract at 0)
            cart.Remove(food);              // remove the entry from the cart if it's at 0
        }
    }

    private void Checkout() {
        checkoutBtn.gameObject.SetActive(false);
        yesBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);
    }

    private void Confirm() {
        purchasedPage.gameObject.SetActive(true);

        string[] cartKeys = cart.Keys.ToArray();
        int[] cartValues = cart.Values.ToArray();
        int cartIndex = 0;

        PlayerChoices.shoppedItems = new string[cart.Count * 2];

        for (int i = 0; i < PlayerChoices.shoppedItems.Length; i++) {
            if (i % 2 == 0) {   // even numbers, i.e. the keys
                PlayerChoices.shoppedItems[i] = cartKeys[cartIndex];
            } else {    // odd numbers, i.e. the values of the previous keys
                PlayerChoices.shoppedItems[i] = (cartValues[cartIndex]).ToString();
            }

            if (i > 0 && i % 2 == 1) cartIndex++;   // on every odd number, the cartIndex moves
        }


        PlayerChoices.groceryDone = true;

        gameObject.SetActive(false);
    }

    private void Deny() {
        checkoutBtn.gameObject.SetActive(true);
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
    }
}
