using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
	[HideInInspector] public Vector3 Position { get => transform.position; }
	[SerializeField] private GameObject cake;

	/// <summary>
	/// ������� ���� � ������ ��� ���� �������
	/// </summary>
	/// <param name="force">���� �������</param>
	public void Push(Vector2 force)
	{
		// Quaternion.identity - ���� ����� �����?
		GameObject cakeClone = Instantiate(cake, transform.position, Quaternion.identity); // ������� ����� ����
		Rigidbody2D body2d = cakeClone.GetComponent<Rigidbody2D>(); // �������� Rigidbody2D
		body2d.AddForce(force, ForceMode2D.Impulse); // ������������� �������� ������ �����
	}
}
