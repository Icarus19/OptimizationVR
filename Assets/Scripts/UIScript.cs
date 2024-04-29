using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject[] menus; 

    //Main Menu
    public void StartGame(){
    }
    void Awake(){
        UpdateStats();
    }
    public void EnterMenu(GameObject menuID){
        for(int i = 0; i < menus.Length; i++){
            menus[i].SetActive(false);
        }
        menuID.SetActive(true);
    }

    //Wave Info Menu
    [SerializeField] TMP_Text greenCarText, redCarText;

    public void SetWaveSize(int greenAmount, int redAmount){
        greenCarText.text = $"x{greenAmount}";
        redCarText.text = $"x{redAmount}";
        greenCarText.ForceMeshUpdate();
        redCarText.ForceMeshUpdate();
    }

    //Shop Menu
    [SerializeField] int[] cost;
    int purchasePrice = 50, towerID = 0;
    [SerializeField] TMP_Text purchaseText;
    [SerializeField] GrabScript grabScript;

    public void AssaultSelect(){
        purchasePrice = cost[0];
        towerID = 0;
        purchaseText.text = $"{purchasePrice}: Gold";
    }
    public void MortarSelect(){
        purchasePrice = cost[1];
        towerID = 1;
        purchaseText.text = $"{purchasePrice}: Gold";
    }
    public void PurchaseSelected(){
        if((goldValue - purchasePrice) >= 0){
            grabScript.SpawnTower(towerID);
            ChangeGold(-purchasePrice);
        }
    }

    //PlayerStats
    [SerializeField] private int healthValue, goldValue;
    [SerializeField] TMP_Text healthText, goldText;
    [SerializeField] GameObject lightSource;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    bool gameOver;
    void UpdateStats(){
        healthText.text = $"Lives: {healthValue}";
        goldText.text = $"Gold: {goldValue}";
        if(healthValue <= 0 && gameOver == false){
            lightSource.SetActive(false);
            source.PlayOneShot(clip);
            gameOver = true;
        }
    }
    public void ChangeHealth(int value){
        healthValue += value;
        UpdateStats();
    }
    public void ChangeGold(int value){
        goldValue += value;
        UpdateStats();
    }
}
