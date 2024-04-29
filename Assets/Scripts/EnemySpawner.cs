using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies, enemyID;
    [Tooltip("Position in array corresponds to enemyID and value is amount of that enemy")] 
    [SerializeField] int[] waveSize;
    [SerializeField] float delay = 1f;
    [SerializeField] UIScript uiScript;
    [SerializeField] GameObject canvasMenu;
    /// <summary>
    /// Make a List of Each enemy and duplicate them from a number instead of spawning each one right away
    /// </summary>
    
    void Start(){
        uiScript = canvasMenu.GetComponentInChildren<UIScript>();
        uiScript.SetWaveSize(waveSize[0], waveSize[1]);
    }

    public void StartWave(){
        StartCoroutine(SpawnEnemy());
    }
    public IEnumerator SpawnEnemy(){
        ObjectPools[] pools = this.GetComponents<ObjectPools>();
        for(int i = 0; i < waveSize.Length; i++){
            for(int j = 0; j < waveSize[i]; j++){
                GameObject enemy = pools[i].GetPooledObject();
                if(enemy != null){
                enemy.transform.position = new Vector3(0, 0, 0);
                enemy.transform.rotation = quaternion.identity;
                enemy.SetActive(true);
            }
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
