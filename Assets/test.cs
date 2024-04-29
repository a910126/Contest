using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    private Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
