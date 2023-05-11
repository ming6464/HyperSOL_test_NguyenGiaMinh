using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private BulletScript _bullet;
    [SerializeField] private float _delayTimeSpawnBullet;
    [SerializeField] private float _speed;

    private float m_x, m_y,m_maxX,m_minX,m_maxY,m_minY;
    private Vector2 addPos;


    private void Start()
    {
        StartCoroutine(DelaySpawnBullet());
        Vector2 sizeScreenMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 sizeScreenMin = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        m_minX = sizeScreenMin.x;
        m_minY = sizeScreenMin.y;
        m_maxX = sizeScreenMax.x;
        m_maxY = sizeScreenMax.y;
    }

    private void Update()
    {
        OnMove();
    }

    private void OnMove()
    {
        m_x = Input.GetAxisRaw("Horizontal");
        m_y = Input.GetAxisRaw("Vertical");

        float ratio = Time.deltaTime * _speed;

        addPos = new Vector2(ratio * m_x, ratio * m_y);
        transform.position += (Vector3)addPos;

        m_x = Mathf.Clamp(transform.position.x, m_minX, m_maxX);
        m_y = Mathf.Clamp(transform.position.y, m_minY, m_maxY);

        transform.position = new Vector3(m_x, m_y, transform.position.z);
    }

    IEnumerator DelaySpawnBullet()
    {
        yield return new WaitForSeconds(_delayTimeSpawnBullet);
        BulletScript newBullet1 = Instantiate(_bullet);
        BulletScript newBullet2 = Instantiate(_bullet);
        BulletScript newBullet3 = Instantiate(_bullet);
        
        newBullet1.OnActive(-30f);
        newBullet2.OnActive(0f);
        newBullet3.OnActive(30f);

        StartCoroutine(DelaySpawnBullet());
    }
}