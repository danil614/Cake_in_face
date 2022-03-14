using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
	[HideInInspector] public Vector3 Position { get => transform.position; }
	[SerializeField] private GameObject cake;
	[SerializeField] private int allowedNumberCakes; // ���������� ���������� ������ �� �����
	private List<GameObject> objectsOnScene = new List<GameObject>();

	/// <summary>
	/// ������� ���� � ������ ��� ���� �������
	/// </summary>
	/// <param name="force">���� �������</param>
	public void Push(Vector2 force)
	{
		GameObject cakeClone = Instantiate(cake, transform.position, Quaternion.identity); // ������� ����� ����
		cakeClone.transform.rotation = transform.rotation; // �������� ���� ��������
		Rigidbody2D body2d = cakeClone.GetComponent<Rigidbody2D>(); // �������� Rigidbody2D
		body2d.AddForce(force, ForceMode2D.Impulse); // ������������� �������� ������ �����
		objectsOnScene.Add(cakeClone); // ��������� � ������ ��������
		DeleteObjects(); // ������� ������ �����
	}

	/// <summary>
	/// ������� ������� �� ����� ��� ���������� ����������� ����������
	/// </summary>
	public void DeleteObjects()
    {
		if (objectsOnScene.Count > allowedNumberCakes)
        {
			Destroy(objectsOnScene[0]); // ������� �� �����
			objectsOnScene.RemoveAt(0); // ������� �� ������
        }
    }
}
