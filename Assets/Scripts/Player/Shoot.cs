using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] GameObject _bloodSplatPrefab;
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
                        var blood = Instantiate(_bloodSplatPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                        objectHit.Damage(35);
                        Destroy(blood, 0.25f);
                    }
                }
            }
        }
    }
}
