
using UnityEngine;
using DG.Tweening;


public class Hart : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private int _loopsCount;
    private SpriteRenderer render;
    void OnEnable()
    {
        render = GetComponent<SpriteRenderer>();
        render.color = Color.white;
    }
    public void Remove()
    {
        GetComponent<SpriteRenderer>().DOFade(0f,_duration).SetLoops(_loopsCount).OnComplete(Disable);

    }
    void Disable() {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        Tools.PrintLine("Сердечко минус");
    }
}
