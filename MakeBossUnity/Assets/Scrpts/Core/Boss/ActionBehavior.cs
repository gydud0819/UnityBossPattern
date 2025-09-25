using UnityEngine;

public interface IStopableActionBehavior
{
    void OnStop();
}
public abstract class ActionBehavior : MonoBehaviour
{
    // 상속할건지, 인터페이스로 만들건지 생각해서 하기
    // ex) A is B (사람은 동물이다) => 클래스 상속, 유닛은 반드시 공격한다 => A is B가 아닌 경우가 존재함 => 인터페이스 사용

    // 멈출

    public bool IsPatternEnd;
    public abstract void OnStart();
    public abstract void OnUpdate();
    public virtual void OnStop()
    {
        IsPatternEnd = false;
    }
    public abstract void OnEnd();
    
}