using CodeBase.UI;
using UnityEngine;
using Zenject;

public class SetTextCase : MonoBehaviour
{
    [Inject]
    private void Construct(IBattleTextInformer battleTextInformer)
    {
        battleTextInformer.SetRoot(transform);
    }
}
