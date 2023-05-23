using Mixin.Utils;
using System.Collections;
using UnityEngine;

public class FloatingMover : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody;

    public float _xBounds;
    public float _YBounds;
    [Min(0)]
    public float _wallPushForce;
    [Min(0)]
    public float _mousePushForce;
    [Min(0)]
    public float _mousePushDistance;
    [Range(0, 1)]
    public float _velocityDecay;

    [Min(0)]
    public float _baseVelocity;
    [Min(0)]
    public float _baseVelocityCatchup;

    private bool _moving;

    IEnumerator Move()
    {
        while (true)
        {
            float time = Time.deltaTime;
            Vector2 position = _rigidbody.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 relativePosition = position - mousePosition;

            Vector2 velocity = _rigidbody.velocity;

            float overflowVelocity = velocity.magnitude - _baseVelocity;

            if (velocity.magnitude != 0 && overflowVelocity > 0)
            {
                overflowVelocity *= (1 - _velocityDecay * time);
                velocity = velocity.normalized * (_baseVelocity + overflowVelocity);
            }

            Vector2 overshot = Vector2.zero;

            overshot += Vector2.right * (position.x - _xBounds).LowerBound(0);
            overshot += Vector2.right * (position.x + _xBounds).UpperBound(0);
            overshot += Vector2.up * (position.y - _YBounds).LowerBound(0);
            overshot += Vector2.up * (position.y + _YBounds).UpperBound(0);

            velocity += -overshot * _wallPushForce * time;

            if (relativePosition.magnitude != 0 && _mousePushDistance > relativePosition.magnitude)
                velocity = relativePosition.normalized * _mousePushForce;

            if (_baseVelocity != 0 && velocity.magnitude == 0)
                velocity += 0.01f * ((float)Randomness.Range(new System.Random(), 0f, 360f)).DegreeToVector2();

            if (velocity.magnitude < _baseVelocity)
                velocity = velocity.normalized * Mathf.Lerp(velocity.magnitude, _baseVelocity, _baseVelocityCatchup * time);

            _rigidbody.velocity = velocity;

            yield return null;
        }
    }

    public void StartMoving()
    {
        StopMoving();
        _moving = true;
        StartCoroutine(Move());
    }

    public void StopMoving()
    {
        StopAllCoroutines();
        _moving = false;
    }
}
