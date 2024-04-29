using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class KeyTransition : MonoBehaviour
{
    private Player player;

    private Vector3 prePos;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void  Update(){
        prePos = player.prePos;
    }
    void OnTriggerEnter(Collider other)
    {
        print("接触");
        if (other.tag == "Player")
        {

            if (player.currentState == Player.State.a)
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 20);
            }
            else
            {
                print("还原");
                player.transform.position = prePos;
            }
        }
    }
}
