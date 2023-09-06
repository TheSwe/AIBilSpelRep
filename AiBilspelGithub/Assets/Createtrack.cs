using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createtrack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BanaRak150;
    public GameObject BanaRak300;
    public GameObject BanaTurn90R;
    public GameObject BanaTurn90L;
    void Start()
    {
        
        Instantiate(BanaRak150, new Vector3(transform.position.x, transform.position.y, transform.position.z+150), Quaternion.Normalize(gameObject.transform.rotation));
        Instantiate(BanaRak300, new Vector3(transform.position.x, transform.position.y, transform.position.z + 450), Quaternion.Normalize(gameObject.transform.rotation));
        Instantiate(BanaTurn90R, new Vector3(transform.position.x-140, transform.position.y, transform.position.z + 890), Quaternion.Normalize(gameObject.transform.rotation));
        Instantiate(BanaTurn90L, new Vector3(transform.position.x-300, transform.position.y, transform.position.z + 600), Quaternion.Euler(270,90,0));
    }

}
