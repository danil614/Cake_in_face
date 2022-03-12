using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Camera mainCamera;
	public Cake cake;
	public Trajectory trajectory;
	[SerializeField] float pushForce; // ���� �������

	bool isClicking = false; // ������� �� �����
	Vector2 force; // ���� ������� �����

	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isClicking = true;
			trajectory.Show(); // ���������� ����������
		}

		if (Input.GetMouseButtonUp(0))
		{
			isClicking = false;
			cake.Push(force); // ������ ����
			trajectory.Hide(); // ������ ����������
		}

		if (isClicking)
		{
			OnClick(); // ���������� ���������� ��� �������
		}
	}

	/// <summary>
	/// ���������� ���������� ��� �������
	/// </summary>
	void OnClick()
	{
		// ����� ������ � ����� ������������ ����� ����������
		Vector2 startPoint = cake.Position;
		Vector2 endPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);

		// ������������ ����� ��� �������
		Debug.DrawLine(startPoint, endPoint);

		float distance = Vector2.Distance(endPoint, startPoint);
		Vector2 direction = (endPoint - startPoint).normalized;

		force = distance * pushForce * direction;

		// ���������� ��� ����� �� ���������� ������
		trajectory.UpdateDots(cake.Position, force);
	}
}
