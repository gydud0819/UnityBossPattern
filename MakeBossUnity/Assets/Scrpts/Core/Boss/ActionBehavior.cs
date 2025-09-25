using UnityEngine;

public interface IStopableActionBehavior
{
    void OnStop();
}
public abstract class ActionBehavior : MonoBehaviour
{
    // ����Ұ���, �������̽��� ������� �����ؼ� �ϱ�
    // ex) A is B (����� �����̴�) => Ŭ���� ���, ������ �ݵ�� �����Ѵ� => A is B�� �ƴ� ��찡 ������ => �������̽� ���

    // ����

    public bool IsPatternEnd;
    public abstract void OnStart();
    public abstract void OnUpdate();
    public virtual void OnStop()
    {
        IsPatternEnd = false;
    }
    public abstract void OnEnd();
    
}