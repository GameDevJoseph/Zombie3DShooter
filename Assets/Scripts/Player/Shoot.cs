using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray origin = Camera.main.ViewportPointToRay(center);
            if (Physics.Raycast(origin, out hit, 50f, 1 << 7))
            {
                if (hit.collider != null)
                {
                    var objectHit = hit.collider.GetComponent<IDamagable>();
                    if (objectHit != null)
                    {
                        objectHit.Damage(35);
                    }
                }
            }
        }
    }
}
