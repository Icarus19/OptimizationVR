using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour
{
    [SerializeField] GameObject[] towers;
    bool full;

    public void SpawnTower(int index){
        ObjectPools[] pools = this.GetComponents<ObjectPools>();
        if(index == 0){
            GameObject tower = pools[0].GetPooledObject();
            if(tower != null){
                tower.transform.position = transform.localPosition;
                tower.transform.rotation = transform.localRotation;
                tower.SetActive(true);
            }
        } else if(index == 1){
            GameObject tower = pools[1].GetPooledObject();
            if(tower != null){
                tower.transform.position = transform.localPosition;
                tower.transform.rotation = transform.localRotation;
                tower.SetActive(true);
            }
        }
    }
}
