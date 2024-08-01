using System; // require keep for Windows Universal App
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableJointTrigger : ObservableTriggerBase
    {
        Subject<float> onJointBreak;

        void OnJointBreak(float breakForce)
        {
            if (onJointBreak != null) onJointBreak.OnNext(breakForce);
        }

        public IObservable<float> OnJointBreakAsObservable()
        {
            return onJointBreak ?? (onJointBreak = new Subject<float>());
        }
        
        

        protected override void RaiseOnCompletedOnDestroy()
        {
            if (onJointBreak != null)
            {
                onJointBreak.OnCompleted();
            }
        }
    }
}