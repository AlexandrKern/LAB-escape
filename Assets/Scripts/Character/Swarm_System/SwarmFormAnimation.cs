using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmFormAnimation : SwarmFormBase
{
    [SerializeField]
    [Tooltip("duration of one frame per second")]
    private float frameDelay = 1f;
    [SerializeField]
    private List<Sprite> frames;
    [SerializeField]
    private SwarmForm swarmFormPrefab;

    private List<SwarmForm> _swarmForms = new();
    private int _currentFrameIndex;

    void Start()
    {
        foreach(var frame in frames)
        {
            SwarmForm swarmForm = Instantiate(swarmFormPrefab, transform);
            swarmForm.spriteRenderer.sprite = frame;
            _swarmForms.Add(swarmForm);
        }

        if(_swarmForms.Count == 0)
        {
            return;
        }
        _currentFrameIndex = 0;
        StartCoroutine(ChangeFrameCoroutine());
    }

    private IEnumerator ChangeFrameCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(frameDelay);
            _currentFrameIndex = (_currentFrameIndex + 1) % _swarmForms.Count;
        }
    }

    public override List<DestinationPoint> GetDestenationPoints()
    {
        return _swarmForms[_currentFrameIndex].GetDestenationPoints();
    }
}
