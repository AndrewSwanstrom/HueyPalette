using UnityEngine;
using UnityEngine.Events;

public class BoxBreak : MonoBehaviour
{
    [SerializeField] private UnityEvent _hit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<HueyController>();
        if (player != null && other.contacts[0].normal.y > 0)
        {
            _hit?.Invoke();
        }
    }
}
