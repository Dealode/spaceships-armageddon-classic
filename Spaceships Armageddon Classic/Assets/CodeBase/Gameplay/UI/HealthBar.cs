using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _shieldSlider;
        [SerializeField] private Slider _hullSlider;
        
        public void SetShield(float value) =>
            _shieldSlider.value = value;
 
        public void SetHull(float value) =>
            _hullSlider.value = value;
    }
}