using UnityEngine;

namespace CodeBase.Units
{
    [RequireComponent(typeof(UnitMove))]
    [RequireComponent(typeof(UnitAnimator))]
    public class UnitAnimate : MonoBehaviour
    {
        private const float MinimalSpeed = 0.01f;
        
        [SerializeField] private UnitMove _unitMove;
        [SerializeField] private UnitAnimator _unitAnimator;
        
        private void Awake()
        {
            _unitMove ??= GetComponentInParent<UnitMove>();
            _unitAnimator ??= GetComponentInParent<UnitAnimator>();
        }

        private void Update()
        {
            if (MoveThenHalfSpeed())
                _unitAnimator.Move(_unitMove.State.Speed);
            else
                _unitAnimator.StopMove();
        }

        private bool MoveThenHalfSpeed()
        {
            var x = _unitMove.SpeedVector.x;
            var y = _unitMove.SpeedVector.y;
            var z = _unitMove.SpeedVector.z;
            
            var forward = transform.forward;
            
            return new Vector3(x * forward.x, y * forward.y, z * forward.z).magnitude >
                   _unitMove.State.Speed / 2;
        }
    }
}