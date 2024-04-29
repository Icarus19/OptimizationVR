using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPools : MonoBehaviour
{
    [SerializeField] List<GameObject> pooledObjects;
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;
    
    void Start(){
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++){
            tmp = Instantiate(objectToPool);
            tmp.SetActive(true);
            pooledObjects.Add(tmp);
            tmp.SetActive(false);
        }
    }

    public GameObject GetPooledObject(){
        for(int i = 0; i < amountToPool; i++){
            if(!pooledObjects[i].activeInHierarchy){
                return pooledObjects[i];
            }
        }
        amountToPool++;
        GameObject tmp;
        tmp = tmp = Instantiate(objectToPool);
        tmp.SetActive(true);
        pooledObjects.Add(tmp);
        tmp.SetActive(false);
        return pooledObjects[amountToPool-1];
    }
}