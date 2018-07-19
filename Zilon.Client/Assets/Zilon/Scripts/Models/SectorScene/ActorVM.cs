﻿using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Zilon.Core.Common;
using Zilon.Core.Tactics;
using Zilon.Core.Tactics.Spatial;

public class ActorVM : MonoBehaviour, IPointerClickHandler
{
    private TaskCompletionSource<bool> _moveTaskSource;
    private const float MOVE_SPEED_Q = 1;
    private const float END_MOVE_COUNTER = 1;


    public bool IsEnemy;
    public GameObject GraphicRoot;

    private Vector3 _targetPosition;
    private float? _moveCounter;
    private Task _moveTask;
    private float _rotationCounter;

    public event EventHandler OnSelected;
    public IActor Actor { get; set; }


    public void Start()
    {
        Actor.OnMoved += Actor_OnMoved;
        Actor.OnDead += Actor_OnDead;

        if (IsEnemy)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    private void Actor_OnDead(object sender, EventArgs e)
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    private void Actor_OnMoved(object sender, EventArgs e)
    {
        _moveCounter = 0;
        var actorNode = (HexNode) Actor.Node;
        var worldPositionParts = HexHelper.ConvertToWorld(actorNode.OffsetX, actorNode.OffsetY);
        _targetPosition = new Vector3(worldPositionParts[0], worldPositionParts[1], -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveCounter != null)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveCounter.Value);
            _moveCounter += Time.deltaTime * MOVE_SPEED_Q;

            if (_moveCounter >= END_MOVE_COUNTER)
            {
                _moveCounter = null;

                _moveTaskSource?.TrySetResult(true);
            }
        }

        _rotationCounter += Time.deltaTime * 3;
        var angle = (float)Math.Sin(_rotationCounter);
        
        GraphicRoot.transform.Rotate(Vector3.back, angle * 0.3f);
    }

    public Task<bool> MoveToPointAsync(Vector3 targetPosition)
    {
        _moveTaskSource = new TaskCompletionSource<bool>();
        _targetPosition = targetPosition;
        _moveCounter = 0;
        return _moveTaskSource.Task;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelected?.Invoke(this, new EventArgs());
    }
}