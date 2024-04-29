using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSystem : MonoBehaviour
{
    [SerializeField] int intensity, amountToPool;
    [SerializeField] GameObject explosion;
    [SerializeField] List<GameObject> particles;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    void Awake(){
        particles = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++){
            tmp = Instantiate(explosion);
            tmp.SetActive(true);
            particles.Add(tmp);
            tmp.SetActive(false);
        }
    }
    
    public void Explosion(Vector3 position){
        GameObject particle;
        for(int i = 0; i < amountToPool; i++){
            if(!particles[i].activeInHierarchy){
                particle = particles[i];
                particle.transform.position = position;
                particle.SetActive(true);
                source = particle.GetComponent<AudioSource>();
                source.PlayOneShot(clip, 0.4f);
                particle.GetComponent<ParticleSystem>().Play();
                return;
            }
        }
        
        //This doens't work and I cannot figure out why. There are no parameters given and the same Objectpool script is working on at least 4 other gameobjects.
        //The error says it's an out of range index, but the index is set to 0 and because the parameters are exposed we can see the objects in the inspector.
        //I thought I found the answer when another bug gave the same error message but on all my other objectpools, but I manage to fix those.
        //The other bug was that any gameObject with the Objectpool script would give errors if it got turned into a prefab, but this one just doesn't work.
        //My only idea now is that it might be related to the particle system specifically but who knows.

        /*ObjectPools pool = this.GetComponent<ObjectPools>();
        Debug.Log("Boom");
        GameObject particle = pool.GetPooledObject();
        Debug.Log("Object Fetched");
        particle.transform.position = position;
        particle.SetActive(true);
        particle.transform.GetComponent<ParticleSystem>().Emit(intensity);
        Debug.Log("After Boom");*/
    }
}
