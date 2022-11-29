using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartManager : MonoBehaviour
{ 
    public static IDictionary<string, int> cart = new Dictionary<string, int>();

    public static void AddFood(string food) {
        if (cart.ContainsKey(food)) {       // if the cart already has the food in it
            cart[food] = cart[food] + 1;    // find add 1 to the existing value
        } else {
            cart.Add(food, 1);              // cart has a new food
        }

        Debug.Log(food + " " + cart[food]);
    }

    public static void SubtractFood(string food) {
        if (cart[food] > 1) {               // if the cart has 2 or more foods
            cart[food] = cart[food] - 1;    // minimum number is 1
        } else {                            // if the cart has 1 food (should not be able to subtract at 0)
            cart[food] = 0;
        }
        Debug.Log(food + " " + cart[food]);
    }
}
