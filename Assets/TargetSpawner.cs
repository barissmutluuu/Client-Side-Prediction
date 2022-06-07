using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TargetSpawner : NetworkBehaviour
{

    public GameObject targetPrefab;
    [SerializeField] NetworkIdentity _identity = null;
    public GameObject startPos;
    // Start is called before the first frame update
    void Start()
    {

        if (isServer == true)
        {

           GameObject target = Instantiate(targetPrefab,startPos.transform.position, Quaternion.identity);
            target.name = "Target";
           NetworkServer.Spawn(target.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
