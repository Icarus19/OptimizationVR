using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleDecay : MonoBehaviour
{
    void Awake()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        this.transform.gameObject.SetActive(false);
    }
}