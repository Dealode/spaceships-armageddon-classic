using System;
using UnityEngine;

namespace CodeBase.Units
{
    [RequireComponent(typeof(Animator))]
    public class UnitAnimator : MonoBehaviour
    {
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        private static readonly int Speed = Animator.StringToHash("Speed");

        [SerializeField] private Animator _animator;

        private void Awake() => 
            _animator ??= GetComponent<Animator>();

        public void Move(float speed)
        {
            _animator.SetBool(IsMove, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMove() => _animator.SetBool(IsMove, false);
    }
}