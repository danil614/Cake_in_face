using UnityEngine;
using System.Collections;
public class MovementCannon : MonoBehaviour
{
    private WheelJoint2D[] wheels; // ������
    private Rigidbody2D cannonRigidbody; // Rigidbody ����� �����

    [SerializeField]
    private Rigidbody2D cannonWheelRigidbody; // Rigidbody ������ �� �����

    [SerializeField]
    private float leftSpeed; // �������� ����� �����
    [SerializeField]
    private float leftWait; // �������� ��� ����� ��������

    [SerializeField]
    private float rightSpeed; // �������� ����� ������
    [SerializeField]
    private float rightWait; // �������� ��� ������ ��������

    private void Start()
    {
        wheels = gameObject.GetComponents<WheelJoint2D>(); // �������� ��� ������ �� �����
        cannonRigidbody = GetComponent<Rigidbody2D>(); // �������� Rigidbody ����� �����
    }

    public IEnumerator DoCannonKickback()
    {
        // ������������� Dynamic �� Rigidbody ����� � ������
        SetRigidbodyType(cannonRigidbody, false);
        SetRigidbodyType(cannonWheelRigidbody, false);

        // ������� ������� ����� �����
        SetMotorSpeed(leftSpeed, true);
        yield return new WaitForSeconds(leftWait);
        SetMotorSpeed(0, false);

        // �����, ������� ������
        SetMotorSpeed(-rightSpeed, true);
        yield return new WaitForSeconds(rightWait);
        SetMotorSpeed(0, false);

        // ������������� Static �� Rigidbody ����� � ������
        SetRigidbodyType(cannonRigidbody, true);
        SetRigidbodyType(cannonWheelRigidbody, true);
    }

    /// <summary>
    /// ������������� �������� ������.
    /// </summary>
    /// <param name="speed">�������� ������</param>
    /// <param name="useMotor">������������ ������� ������?</param>
    private void SetMotorSpeed(float speed, bool useMotor)
    {
        foreach (WheelJoint2D wheel in wheels)
        {
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed = speed;
            wheel.motor = motor;
            wheel.useMotor = useMotor;
        }
    }

    /// <summary>
    /// ������������� ��� Rigidbody.
    /// </summary>
    /// <param name="rigidbody">Rigidbody</param>
    /// <param name="isStatic">���������� ��������� ���?</param>
    private void SetRigidbodyType(Rigidbody2D rigidbody, bool isStatic)
    {
        if (isStatic)
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}