using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public  class Shooter : NetworkBehaviour
{
   public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void ShootCMD()
    {

        Debug.Log("girdimm2");
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z + 1f), Quaternion.identity);
        NetworkServer.Spawn(bullet.gameObject);
        bullet.transform.forward = new Vector3(0f, 0f, 10f);
        bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, 50f), ForceMode.Impulse);
        


        StartCoroutine(DestroyBulletAfter3sc(bullet));


    }


    public IEnumerator DestroyBulletAfter3sc(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        Destroy(bullet);
    }

}
