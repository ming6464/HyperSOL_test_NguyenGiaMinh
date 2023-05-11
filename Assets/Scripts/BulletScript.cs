using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool isActive;
    private Vector2 m_posNext;
    private Vector3 m_beginPos;
    private void Update()
    {
        if (!isActive) return;
        DrawLine();
        transform.position = 
            Vector2.MoveTowards(transform.position, m_posNext, Time.deltaTime * _speed);
        if(m_posNext == (Vector2)transform.position) Destroy(this.gameObject);
    }

    private void DrawLine()
    {
        Debug.DrawLine(m_beginPos,m_posNext,Color.magenta);
    }

    public void OnActive(float angle)
    {
        m_beginPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        transform.position = m_beginPos;
        
        transform.Rotate(Vector3.forward,-angle);

        Vector2 _maxPosOnScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float distance = _maxPosOnScreen.y - m_beginPos.y + 1f;
        float y = m_beginPos.y + distance;
        float angle2 = angle * 180 / Mathf.PI;
        float x = m_beginPos.x + distance * Mathf.Tan(angle2);
        m_posNext = new Vector2(x, y);
        isActive = true;
    }
}