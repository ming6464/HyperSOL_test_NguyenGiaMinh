using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    private SpriteRenderer _spriteRg;
    void Start()
    {
        _spriteRg = GetComponent<SpriteRenderer>();
        _speed *= 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRg.material.mainTextureOffset += Vector2.right * Time.deltaTime * _speed;
    }
}
