using CodeBase.Units;
using UniRx;
using UnityEngine;

namespace CodeBase.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
     
        private CompositeDisposable _disposable = new CompositeDisposable();
        
        private UnitState _playerState;

        public void Construct(UnitState playerState)
        {
            _playerState = playerState;
            Subscribe();
        }

        private void OnEnable()
        {
            if (_playerState == null)
            {
                return;
            }
            Subscribe();
        }

        private void Subscribe()
        {
            _playerState.CurrentShield
                .Subscribe(shield => _healthBar.SetShield(shield / _playerState.MaxShield))
                .AddTo(_disposable);

            _playerState.CurrentHull
                .Subscribe(hull => _healthBar.SetHull(hull / _playerState.MaxHull))
                .AddTo(_disposable);

            UpdateHealthBar();
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }
        
        private void UpdateHealthBar()
        {
            _healthBar.SetShield(_playerState.CurrentShield.Value / _playerState.MaxShield);
            _healthBar.SetHull(_playerState.CurrentHull.Value / _playerState.MaxHull);
        }
    }
}