using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoices : MonoBehaviour
{
    // so PlayerPrefs has:
    // TimeCount, DayCount
    // Name, DOB, Gender, Bio

    // All of PlayerChoices resets after Save&Menu in GameMenu
    // ELEPHANT: is there some things that don't need resetting?

    // plant
    public static bool buyPremiumPlant {get; set;} = true;  // default is to get it unless it's unchecked
    public static bool receivePlantPromo {get; set;} = true;  // default option is to receive checked promo
    public static string plantRibbonColor {get; set;} = "";
    public static string plantType {get; set;} = "";

    // grocery
    public static string[] shoppedItems {get; set;} = {}; // default empty? does this work?
    public static bool groceryDone {get; set;} = false;

    // friendmi
    public static bool acceptRequest {get; set;} = false;   // default no friend request accepted
    public static bool introducedYourself {get; set;} = false;
    public static string chatRecord {get; set;} = "";
    public static int chatProgress {get; set;} = 0;     // format for this is step/day/time; resets on TimeTransition

    // friendmi day 1
    public static bool checkedFriendProfile {get; set;} = false;
    public static int willUpdateProfile {get; set;} = 0;    // 0 is default; 1 is will update, 2 is will not
    public static bool friendPrivacy {get; set;} = false;   // if friend has said why they don't have their pf filled in
    public static bool learnPrivacy {get; set;} = false;
    public static bool learnDPDef {get; set;} = false;
    public static bool learnDPPlant {get; set;} = false;    // misdirection and trick questions
    public static int readTCs {get; set;} = 0; // 0 is default; 1 is read; 2 is didn't read

    public static int D1T1Ending {get; set;} = 0;
    // 1: having 1 groc store is fine
    // 2: Reese mentions their friends seem "programmed"; "no reason for player to be not human"
    // 3: player said 1 groc store is weird, but then backtracks
    // 4: player asked questions about the store

    // choices are currently reset once you go back to MainMenu
    // choices will persist in JSON if saved
    public static void ResetChoices() {
        buyPremiumPlant = true;
        receivePlantPromo = true;
        plantRibbonColor = "";
        plantType = "";

        shoppedItems = Array.Empty<string>();   // creates an empty array
        groceryDone = false;

        acceptRequest = false;
        introducedYourself = false;
        chatRecord = "";
        chatProgress = 0;

        checkedFriendProfile = false;
        willUpdateProfile = 0;
        friendPrivacy = false;
        learnPrivacy = false;
        learnDPDef = false;
        learnDPPlant = false;
        readTCs = 0;

        D1T1Ending = 0;
    }
}