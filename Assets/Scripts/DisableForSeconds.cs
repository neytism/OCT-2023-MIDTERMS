using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableForSeconds : MonoBehaviour
{
    [SerializeField] private float _time = 0f;
    [SerializeField] private bool _isParticle = false;
    private void OnEnable()
    {

        //if particle
        if (_isParticle)
        {
            GetComponent<ParticleSystem>().Play();
        }
        
        StartCoroutine(Life());
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(_time);
        gameObject.SetActive(false);
        
        //if particle
        if (_isParticle)
        {
            GetComponent<ParticleSystem>().Stop();
        } 
    }
}
