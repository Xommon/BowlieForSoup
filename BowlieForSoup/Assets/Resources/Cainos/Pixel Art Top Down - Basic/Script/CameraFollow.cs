using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //let camera follow target
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float lerpSpeed = 1.0f;
        public GameManager gameManager;

        private Vector3 offset;

        private Vector3 targetPos;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            if (target == null) return;

            transform.position = new Vector3(gameManager.savedPlayerPosition.x, gameManager.savedPlayerPosition.y, -10);
            offset = transform.position - target.position;
        }

        private void Update()
        {
            if (target == null) return;

            targetPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }

    }
}
