﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// This Action will make the Character patrol along the defined path (see the MMPath inspector for that) until it hits a wall or a hole while following a path.
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/AI/Actions/AIActionMovePatrol3D")]
    [RequireComponent(typeof(MMPath))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterMovement))]
    public class AIActionMovePatrol3D : AIAction
    {        
        [Header("Obstacle Detection")]
        /// If set to true, the agent will change direction when hitting an obstacle
        [Tooltip("If set to true, the agent will change direction when hitting an obstacle")]
        public bool ChangeDirectionOnObstacle = true;
        /// the distance to look for obstacles at
        [Tooltip("the distance to look for obstacles at")]
        public float ObstacleDetectionDistance = 1f;
        /// the frequency (in seconds) at which to check for obstacles
        [Tooltip("the frequency (in seconds) at which to check for obstacles")]
        public float ObstaclesCheckFrequency = 1f;
        /// the layer(s) to look for obstacles on
        [Tooltip("the layer(s) to look for obstacles on")]
        public LayerMask ObstacleLayerMask;
        /// the coordinates of the last patrol point
        public Vector3 LastReachedPatrolPoint { get; set; }

        // private stuff
        protected TopDownController _controller;
        protected Character _character;
        protected CharacterMovement _characterMovement;
        protected Health _health;
        protected Vector3 _direction;
        protected Vector2 _startPosition;
        protected Vector2 _initialDirection;
        protected Vector3 _initialScale;
        protected float _distanceToTarget;
        protected Vector3 _initialPosition;
        protected MMPath _mmPath;
        protected Collider _collider;
        protected float _lastObstacleDetectionTimestamp = 0f;
        protected int _currentIndex = 0;
        protected int _indexLastFrame = -1;
        protected float _waitingDelay = 0f;
                
        /// <summary>
        /// On init we grab all the components we'll need
        /// </summary>
        protected override void Initialization()
        {
            _collider = this.gameObject.GetComponent<Collider>();
            _controller = this.gameObject.GetComponent<TopDownController>();
            _character = this.gameObject.GetComponent<Character>();
            _characterMovement = this.gameObject.GetComponent<CharacterMovement>();
            _health = this.gameObject.GetComponent<Health>();
            _mmPath = this.gameObject.GetComponent<MMPath>();
            // initialize the start position
            _startPosition = transform.position;
            _initialPosition = this.transform.position;
            _initialDirection = _direction;
            _initialScale = transform.localScale;
            _currentIndex = 0;
            _indexLastFrame = -1;
            LastReachedPatrolPoint = this.transform.position;
    }

        /// <summary>
        /// On PerformAction we patrol
        /// </summary>
        public override void PerformAction()
        {
            Patrol();
        }

        /// <summary>
        /// This method initiates all the required checks and moves the character
        /// </summary>
        protected virtual void Patrol()
        {
            _waitingDelay -= Time.deltaTime;

            if (_character == null)
            {
                return;
            }
            if ((_character.ConditionState.CurrentState == CharacterStates.CharacterConditions.Dead)
                || (_character.ConditionState.CurrentState == CharacterStates.CharacterConditions.Frozen))
            {
                return;
            }

            if (_waitingDelay > 0)
            {
                _characterMovement.SetHorizontalMovement(0f);
                _characterMovement.SetVerticalMovement(0f);
                return;
            }

            // moves the agent in its current direction
            CheckForObstacles();

            _currentIndex = _mmPath.CurrentIndex();
            if (_currentIndex != _indexLastFrame)
            {
                LastReachedPatrolPoint = _mmPath.CurrentPoint();
                _waitingDelay = _mmPath.PathElements[_currentIndex].Delay;
            }

            _direction = _mmPath.CurrentPoint() - this.transform.position;
            _direction = _direction.normalized;

            _characterMovement.SetHorizontalMovement(_direction.x);
            _characterMovement.SetVerticalMovement(_direction.z);

            _indexLastFrame = _currentIndex;
        }

        /// <summary>
        /// Draws bounds gizmos
        /// </summary>
        protected virtual void OnDrawGizmosSelected()
        {
            if (_mmPath == null)
            {
                return;
            }
            Gizmos.color = MMColors.IndianRed;
            Gizmos.DrawLine(this.transform.position, _mmPath.CurrentPoint());
        }

        /// <summary>
        /// When exiting the state we reset our movement
        /// </summary>
        public override void OnExitState()
        {
            base.OnExitState();
            _characterMovement.SetHorizontalMovement(0f);
            _characterMovement.SetVerticalMovement(0f);
        }

        /// <summary>
	    /// Checks for a wall and changes direction if it meets one
	    /// </summary>
	    protected virtual void CheckForObstacles()
        {
            if (!ChangeDirectionOnObstacle)
            {
                return;
            }

            if (Time.time - _lastObstacleDetectionTimestamp < ObstaclesCheckFrequency)
            {
                return;
            }
                        
            bool hit = Physics.BoxCast(_collider.bounds.center, _collider.bounds.extents, _controller.CurrentDirection.normalized, this.transform.rotation, ObstacleDetectionDistance, ObstacleLayerMask);
            if (hit)
            {
                ChangeDirection();
            }

            _lastObstacleDetectionTimestamp = Time.time;
        }

        /// <summary>
        /// Changes the current movement direction
        /// </summary>
        protected virtual void ChangeDirection()
        {
            _direction = -_direction;
            _mmPath.ChangeDirection();
        }
        
        /// <summary>
        /// When reviving we make sure our directions are properly setup
        /// </summary>
        protected virtual void OnRevive()
        {            
            transform.localScale = _initialScale;
            transform.position = _startPosition;
        }

        /// <summary>
        /// On enable we start listening for OnRevive events
        /// </summary>
        protected virtual void OnEnable()
        {
            if (_health == null)
            {
                _health = this.gameObject.GetComponent<Health>();
            }

            if (_health != null)
            {
                _health.OnRevive += OnRevive;
            }
        }

        /// <summary>
        /// On disable we stop listening for OnRevive events
        /// </summary>
        protected virtual void OnDisable()
        {
            if (_health != null)
            {
                _health.OnRevive -= OnRevive;
            }
        }
    }
}