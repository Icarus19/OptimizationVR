using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Healthbar healthbar;
    [SerializeField] UIScript playerHealth;
    [SerializeField] int damageDealth;

    void Awake(){
        playerHealth = FindObjectOfType<UIScript>();
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player") {
            playerHealth.ChangeHealth(-damageDealth);
            healthbar.TakeDamage(99);
        }
    }
}
