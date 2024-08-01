using System;
using CodeBase.StaticData.Units;
using UnityEngine;

namespace CodeBase.Units
{
    public interface IUnitBehaviour
    {
        UnitState State { get; }
        Guid Id { get; set; }
        UnitTypeId TypeId { get; set; }
        Transform Transform { get; }
    }

    [RequireComponent(typeof(BoxCollider))]
    public class UnitBehaviour : MonoBehaviour, IUnitBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        public UnitState State { get; private set; }
        public Guid Id { get; set; }
        public UnitTypeId TypeId { get; set; } = UnitTypeId.Asteroid;
        public Transform Transform => transform;
        
        public bool IsLocal => true;
        public bool IsDead => State.IsDead;

        private void Awake()
        {
            _collider ??= GetComponent<BoxCollider>();
        }

        public void InitializeWithState(UnitState unitState)
        {
            State = unitState;
        }

        public void OnDrawGizmos()
        {
            if (_collider == null)
                return;
            
            Gizmos.color = new Color(0.18f, 1f, 0f, 0.43f);
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero,_collider.size);
        }
    }
}