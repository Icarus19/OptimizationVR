using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TowerScripts : MonoBehaviour
{
    [SerializeField] float range, firerate;
    [SerializeField] GameObject[] projectileTypes;
    //I tried to use abstract classes here, but I ran out of time before I could find a way to give them unique fields such as damage and firerate
    [Tooltip("Index corresponds to type of tower 0 = Mortar, 1 = Assault")]
    [Range(0, 1)]
    [SerializeField] int index = 0;
    [SerializeField] int damage;
    [SerializeField] Material[] materials;
    [SerializeField] Renderer[] meshRenderer;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clip;
    Healthbar target;
    bool active = false;
    const int enemyLayerMask = 1 << 9;
    float cooldown;
    ParticlesSystem particleSystem;


    void Update(){
        if(active){
            if(AcquireTarget() && cooldown <= 0){
                FireAtTarget();
            } else {
                cooldown -= Time.deltaTime; 
            }
        }
    }

    public void ActivateTower(){
        //Remove xr component and turn into a static tower
        particleSystem = FindObjectOfType<ParticlesSystem>();
        source = FindObjectOfType<AudioSource>();
        active = true;
        meshRenderer[0].materials = materials;
        meshRenderer[1].materials = materials;
        var tmp = transform.GetComponentInParent<XRGrabInteractable>();
        if(tmp != null){
            tmp.enabled = false;
        }
    }
    /*void OnDrawGizmosSelected () {
		Gizmos.color = Color.yellow;
		Vector3 position = transform.position;
		position.y += 0.01f;
		Gizmos.DrawWireSphere(position, range);
	}*/
    //We start by casting a sphere around the tower to identify all nearby enemies
    bool AcquireTarget() {
		Collider[] targets = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
		if (targets.Length > 0) {
			target = targets[0].GetComponent<Healthbar>();
			Debug.Assert(target != null, "Targeted non-enemy!", targets[0]);
			return true;
		}
		target = null;
		return false;
	}
    //We trigger a shot and call on special effects before restarting the cooldown timer on shooting
    void FireAtTarget(){
        if(index == 0){
            //meaning Mortar Tower
            //We cast another sphere at our targets position and blow up everything nearby
            particleSystem.Explosion(target.transform.position);
            Collider[] targets = Physics.OverlapSphere(target.transform.position, 3f, enemyLayerMask);
            if(targets.Length > 0){
                for(int i = 0; i < targets.Length; i++){
                    targets[i].transform.GetComponent<Healthbar>().TakeDamage(damage);
                    AudioSource.PlayClipAtPoint(clip[0], target.transform.position, 0.7f);
                    Debug.Assert(target != null, "Targeted non-enemy!", targets[0]);
                }
            }
        } else if(index == 1){
            //Assault Tower
            target.TakeDamage(damage);
            source.PlayOneShot(clip[1], 0.06f);
        }
        cooldown = firerate;
    }
}

