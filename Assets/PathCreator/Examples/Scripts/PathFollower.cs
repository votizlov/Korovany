using System;
using Core;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 10;
        public bool isRotating;
        [SerializeField] private GameProxy gp;
        private float m_DistanceTravelled;
        private Vector3 m_InitRotation; //добавил сохранение начального вращения и положения объекта
        private Vector3 m_InitPos;

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                m_InitRotation = transform.eulerAngles;
                m_InitPos = transform.position;
            }
        }

        void Update()
        {
            m_DistanceTravelled += speed * Time.deltaTime;
            if (pathCreator.path.length <= m_DistanceTravelled)
            {
                gp.gameManager.OnKorovanLeft();
                Destroy(gameObject);
            }

            transform.position = pathCreator.path.GetPointAtDistance(m_DistanceTravelled, endOfPathInstruction) +
                                 m_InitPos;
            if (isRotating)
                transform.eulerAngles =
                    pathCreator.path.GetRotationAtDistance(m_DistanceTravelled, endOfPathInstruction).eulerAngles +
                    m_InitRotation;
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            m_DistanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}