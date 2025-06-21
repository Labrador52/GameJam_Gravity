using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GravityController : MonoBehaviour
{
    [SerializeField] private float _gravity = 8;
    private Dictionary<int, PlanetGravity> currentPlanetDic = new();
    private PlanetGravity _currentPlanet;
    private Rigidbody2D _rb;
    private CircleCollider2D _circleCollider;
    public bool IsGrounded { get; private set; } = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        if (currentPlanetDic.Count == 0) return;
        
        Vector2 totalForce = Vector2.zero;
        bool isGroundedInner = false;
        
        foreach (var planet in currentPlanetDic.Values)
        {
            totalForce += CalculateGravity(planet.transform.position, planet.mass);
            
            if (!isGroundedInner && Physics2D.Raycast(
                transform.position, -transform.up, 
                _circleCollider.radius + 0.1f))
            {
                isGroundedInner = true;
            }
        }
        
        IsGrounded = isGroundedInner;
        _rb.AddForce(totalForce);
    }

    Vector2 CalculateGravity(Vector2 planetCenter, float planetMass)
    {
        Vector2 direction = ((Vector2)planetCenter - (Vector2)transform.position).normalized;
        float distance = Vector2.Distance(planetCenter, transform.position);
        float forceMagnitude = _gravity * planetMass * _rb.mass / Mathf.Pow(distance, 2);
        return direction * forceMagnitude;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.TryGetComponent(out PlanetGravity planet))
        {
            currentPlanetDic[planet.GetHashCode()] = planet;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent.TryGetComponent(out PlanetGravity planet) && 
            currentPlanetDic.ContainsKey(planet.GetHashCode()))
        {
            currentPlanetDic.Remove(planet.GetHashCode());
        }
    }
}
