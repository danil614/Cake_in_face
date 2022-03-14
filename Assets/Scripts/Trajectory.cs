using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField] int dotsNumber; // ���������� ����� � ����������
	[SerializeField] GameObject dotsParent; // ����������� ���� �����
	[SerializeField] GameObject dotPrefab; // ������ - �����
	[SerializeField] float dotSpacing; // ���������� ����� �������
	[SerializeField][Range(0.01f, 0.3f)] float dotMinScale; // ����������� ������ �����
	[SerializeField][Range(0.1f, 1f)] float dotMaxScale; // ������������ ������ �����

	Transform[] dotsList; // ������ ����� ��� ����������

	void Start()
	{
		Hide(); // �������� ����������
		PrepareDots();
	}

	/// <summary>
	/// ������� ����� ��� ���������� � ������������ �� ������
	/// </summary>
	void PrepareDots()
	{
		dotsList = new Transform[dotsNumber];
		dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

		float scale = dotMaxScale;
		float scaleFactor = scale / dotsNumber;

		for (int i = 0; i < dotsNumber; i++)
		{
			dotsList[i] = Instantiate(dotPrefab, null).transform;
			dotsList[i].parent = dotsParent.transform;

			dotsList[i].localScale = Vector3.one * scale;
			
			if (scale > dotMinScale)
			{
				scale -= scaleFactor;
			}
		}
	}

	/// <summary>
	/// ������������� ������� ���� ����� ����������
	/// </summary>
	/// <param name="startPosition">��������� �������</param>
	/// <param name="forceApplied">����������� ����</param>
	public void UpdateDots(Vector3 startPosition, Vector2 forceApplied)
	{
		Vector2 position;
		float timeStamp = dotSpacing;

		for (int i = 0; i < dotsNumber; i++)
		{
			position.x = (startPosition.x + forceApplied.x * timeStamp);
			position.y = (startPosition.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

			dotsList[i].position = position;
			timeStamp += dotSpacing;
		}
	}

	/// <summary>
	/// ���������� ����������
	/// </summary>
	public void Show()
	{
		dotsParent.SetActive(true);
	}

	/// <summary>
	/// ������ ����������
	/// </summary>
	public void Hide()
	{
		dotsParent.SetActive(false);
	}
}
