using UnityEngine;
using UnityEngine.EventSystems;

public class Hero : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform heroCenter;

    [SerializeField]
    private float speed;

    [SerializeField]
    private CannonArea cannonArea;

    private bool isDragging = false; // �������������� Hero

    private Rigidbody2D heroCenterRigidbody;

    private Vector2 startClick;
    private Vector2 endClick;

    /// <summary>
    /// ������ �� ��������������?
    /// </summary>
    [HideInInspector]
    public bool IsDragging { get => isDragging; }

    private void Start()
    {
        heroCenterRigidbody = heroCenter.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDragging)
        {
            if (!cannonArea.IsPressing)
            {
                endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // �������� ���������� �������
            }

            //heroCenterRigidbody.MovePosition(endClick); // ���������� hero

            //heroCenterRigidbody.AddForce((endClick - startClick) * speed);
            //heroCenter.transform.Translate(speed * Time.fixedDeltaTime * (endClick - startClick));
            //var targetPosition = endClick;
            //heroCenter.position = Vector3.Lerp(startClick, targetPosition, speed);

            //heroCenter.position = Vector3.Lerp(heroCenter.position, endClick, speed * Time.deltaTime);

            //endClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // �������� ���������� �������
            heroCenterRigidbody.MovePosition(Vector2.Lerp(heroCenter.position, endClick, speed * Time.deltaTime));
        }
    }

    //private void FixedUpdate()
    //{

    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Hero"))
    //    {

    //    }
    //}

    /// <summary>
    /// ����������� ��� ������� �� ������.
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true; // �������������� ������

        transform.parent = null; // ��� ��������� ������� ������� ����������� �� �������

        startClick = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ������� �������
        heroCenter.SetPositionAndRotation(startClick, transform.rotation); // ������������� ������� � ���� ��������

        transform.parent = heroCenter; // ������ ����������� ��� �������
    }

    /// <summary>
    /// ����������� ��� ���������� ������� �� ������.
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false; // �������������� ���������
    }
}
