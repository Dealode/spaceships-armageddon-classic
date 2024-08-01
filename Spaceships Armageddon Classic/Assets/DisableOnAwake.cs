using UnityEngine;

public class DisableOnAwake : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
}
