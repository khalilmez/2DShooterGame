using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.3f;
    Renderer renderer;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        offset = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        renderer.material.mainTextureOffset += offset * Time.deltaTime;
    }
}
