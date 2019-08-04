using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using SnSMovement.Character;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// A class to handle a platform that moves in 2D along a set of nodes
    /// </summary>
    public class MovingPlatform2D : MMPathMovement
    {
        public float movementSpeedToSet;
        public bool startMoving;
        
        public void StartMoving()
        {
            MovementSpeed = movementSpeedToSet;
        }
        public void StopMoving()
        {
            MovementSpeed = 0;
        }

        void Start()
        {
            base.Start();
            MovementSpeed = 0;
            if (startMoving)
            {
                MovementSpeed = movementSpeedToSet;
            }
        }
    }
}
