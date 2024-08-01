using UnityEngine;
using UnityEngine.Serialization;

public class UnitRoll : MonoBehaviour
{
    [SerializeField] private Transform _reference;
    [SerializeField] private Transform _target;
    
    [Space(5)]
    [SerializeField] private float _rollSpeed = 1f;
    [SerializeField] private float _rollAngle = 60f;
    [FormerlySerializedAs("_timerBeforeRollBack")] [SerializeField] private float _timeBeforeRollBack = 1f;

    private Vector3 _lastRotation;
    private float _timer;

    private void Update()
    {
        var currentRotation = _reference.rotation.eulerAngles;
        
        var delta = currentRotation - _lastRotation;

        RollUnit(delta);

        _lastRotation = currentRotation;
    }

    private void RollUnit(Vector3 delta)
    {
        if (Mathf.Abs(delta.y) < 0.01f)
        {
            _timer -= Time.deltaTime;

            if (_timer > 0)
            {
                return;
            }

            _timer = 0;


            var roll = _target.localRotation.eulerAngles.z; 
            
            if (roll != 0)
            {
                _target.localRotation =
                    Quaternion.RotateTowards(
                        _target.localRotation,
                        Quaternion.Euler(0,0,0),
                        _rollSpeed * Time.deltaTime);
            }
        }
        else
        {
            _timer = _timeBeforeRollBack;
            
            var rollTarget = Quaternion.Euler(0, 0, delta.y < 0 ? _rollAngle : -_rollAngle);
            
            _target.localRotation =
                Quaternion.RotateTowards(
                    _target.localRotation,
                    rollTarget,
                    _rollSpeed * Time.deltaTime);
        }
    }
}
