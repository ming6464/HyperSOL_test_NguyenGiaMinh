using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public bool isRunning,isActive,canPlayNextStep,canMove,canMoveDown,canMoveUp;
    public int order;
    public float row, col,delayMoveDown;

    [SerializeField] private float _speed = 4;

    private Vector2 m_p1, m_p2, m_p3, m_p4, m_p5, m_p6,m_p7,m_posNext,m_p8;
    
    // Update is called once per frame
    void Update()
    {
        DrawLine();
        if (!isActive) return;
        OnMove();

    }

    private void DrawLine()
    {
        Debug.DrawLine(m_p1,m_p2,Color.red);
        Debug.DrawLine(m_p2,m_p3,Color.red);
        Debug.DrawLine(m_p3,m_p4,Color.red);
        Debug.DrawLine(m_p4,m_p5,Color.red);
        Debug.DrawLine(m_p5,m_p6,Color.red);
        Debug.DrawLine(m_p6,m_p1,Color.red);
        Debug.DrawLine(m_p1,m_p7,Color.red);
        Debug.DrawLine(m_p7,m_p8,Color.green);
    }

    private void OnMove()
    {
        if (isRunning)
        {
            transform.position = Vector2.MoveTowards(transform.position,m_posNext,Time.deltaTime * _speed);
            if (m_posNext == (Vector2)transform.position)
            {
                isRunning = false;
                
            }
            return;
        }

        if (canPlayNextStep)
        {
            if (!GameManager.Ins.CanMoveUpAndDown)
            {
                if (order == 1) GameManager.Ins.CanMoveUpAndDown = true;
                return;
            }
            if (!canMove && m_p7 == (Vector2)transform.position)
            {
                canMove = true;
                StartCoroutine(DelayMoveDown());
            }

            if (!canMove) return;
            if(canMoveDown) OnMoveDown();
            else if (canMoveUp) OnMoveUp();
            return;
        }

        if (Vector2.Equals(m_p1, m_posNext))
        {
            m_posNext = m_p7;
            canPlayNextStep = true;
        }
        else if (Vector2.Equals(m_p2, m_posNext))m_posNext = m_p3;
        else if (Vector2.Equals(m_p3, m_posNext))m_posNext = m_p4;
        else if (Vector2.Equals(m_p4, m_posNext))m_posNext = m_p5;
        else if (Vector2.Equals(m_p5, m_posNext))m_posNext = m_p6;
        else if (Vector2.Equals(m_p6, m_posNext)) m_posNext = m_p1;
        else isActive = false;
        isRunning = true;

    }

    public void Active(int order)
    {
        this.order = order;
        LoadPoint();
        transform.position = m_p1;
        m_posNext = m_p2;
        isRunning = true;
        isActive = true;
    }
    
    private void LoadPoint()
    {
        float width = Screen.width;
        float height = Screen.height/2f;

        Vector2 spriteHalfSize = GetComponent<SpriteRenderer>().size / 2f;
        Vector2 addPos = spriteHalfSize * new Vector2(1, -1);;
        Vector2 addPos3 = addPos * new Vector2(-1, 1);

        row = Mathf.Clamp(Mathf.Floor(order / 4f - 0.001f), 0, order);
        col = Mathf.Clamp(order - row * 4 - 1, 0, order);

        m_p1 = new Vector2(width/2f,height * 2f);
        m_p2 = m_p1 - new Vector2(-width / 2f, height / 3f);
        m_p3 = m_p2 - new Vector2(0, height / 3f);
        m_p4 = new Vector2(width/2f,height);
        m_p5 = m_p4 + new Vector2(-width / 2f, height / 3f);
        m_p6 = m_p5 + new Vector2(0, height / 3f);
        m_p7 = new Vector2(col * width/3f,height * 2f - (row * height / 3f));
        m_p8 = m_p7 - new Vector2(0, height/2f);

        m_p1 = Camera.main.ScreenToWorldPoint(m_p1);
        m_p2 = Camera.main.ScreenToWorldPoint(m_p2) + new Vector3(addPos3.x,0,0);
        m_p3 = Camera.main.ScreenToWorldPoint(m_p3)  + (Vector3)addPos3;
        m_p4 = Camera.main.ScreenToWorldPoint(m_p4);
        m_p5 = Camera.main.ScreenToWorldPoint(m_p5)  + (Vector3)addPos;
        m_p6 = Camera.main.ScreenToWorldPoint(m_p6) + new Vector3(addPos.x, 0, 0);
        m_p7 = Camera.main.ScreenToWorldPoint(m_p7);
        m_p8 = Camera.main.ScreenToWorldPoint(m_p8);

        switch (col)
        {
            case 1:
                addPos *= new Vector2(0.5f, 1);
                break;
            case 2:
                addPos *= new Vector2(-0.5f, 1);
                break;
            case 3:
                addPos = addPos3;
                break;
        }

        m_p7 += addPos;
        m_p8 += addPos;

    }

    private void OnMoveDown()
    {
        transform.position = Vector2.MoveTowards(transform.position,m_p8,Time.deltaTime * _speed);
        if (m_p8 == (Vector2)transform.position)
        {
            canMoveDown = false;
            canMoveUp = true;
        }
    }

    private void OnMoveUp()
    {
        transform.position = Vector2.MoveTowards(transform.position,m_p7,Time.deltaTime * _speed);
        if (m_p7 == (Vector2)transform.position)
        {
            canMove = false;
            canMoveUp = false;
        }
    }

    IEnumerator DelayMoveDown()
    {
        yield return new WaitForSeconds(delayMoveDown);
        canMoveDown = true;
    }
}
