using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResMgr.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        PoolMgr.GetInstance().Clear();
    }
}
