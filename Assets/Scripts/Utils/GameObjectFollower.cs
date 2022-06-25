using UnityEngine;

public class GameObjectFollower : MonoBehaviour {
    public Transform Target;
    public Vector3 Offset;
    
    private void Update() {
        transform.position = Target.position + Offset;
    }
}