using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float _lifetime = 2f;

    void Start()
    {
        Destroy(this.gameObject, _lifetime);
    }
}
