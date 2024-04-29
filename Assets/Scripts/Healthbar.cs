using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] int health = 6;
    [SerializeField] ParticlesSystem particleSystem;
    [SerializeField] int droppedGold;
    UIScript uiScript;

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            BlowUp();
        }
    }
    //I know I shouldn't use the find functions, but because I can't make the particlesystem into an asset. And I can't reference it in the inspector.
    void BlowUp(){
        uiScript = FindObjectOfType<UIScript>();
        particleSystem = FindObjectOfType<ParticlesSystem>();
        uiScript.ChangeGold(droppedGold);
        particleSystem = FindObjectOfType<ParticlesSystem>();
        particleSystem.Explosion(transform.position);
        gameObject.SetActive(false);
    }
}
