using UnityEngine;

public class PlanetGravity : MonoBehaviour {
    public float mass = 100f; // 星球质量
    public float gravityRadius = 20f; // 引力范围
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
    }
}