using UnityEngine;

public class LifeTime : MonoBehaviour
{
  [SerializeField] float _lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
