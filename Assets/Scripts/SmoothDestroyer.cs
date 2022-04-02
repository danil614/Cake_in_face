using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDestroyer : MonoBehaviour
{
    private Color color;
    private float alpha = 1.0f;
    private bool isDisappearing = false;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float stepColor;

    [SerializeField]
    private float delayColor; // ����� ��� ������������ �����

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color; // ���������� ����� � �������
        StartCoroutine(SlowDelete()); // ����� ��������
    }

    private void Update()
    {
        if (isDisappearing)
        {
            alpha -= Time.deltaTime * stepColor; // ��������� ����� ����� � ������ Update
            color.a = alpha; // ����������� ����� ������ ����� ��������
            spriteRenderer.color = color;

            if (alpha <= 0.0f) // ����� ����� �� �����, ���������� ������
            {
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator SlowDelete()
    {
        yield return new WaitForSeconds(delayColor);
        isDisappearing = true;
    }
}
