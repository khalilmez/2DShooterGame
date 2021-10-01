using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDuration : MonoBehaviour
{
    [SerializeField]
    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, duration); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
