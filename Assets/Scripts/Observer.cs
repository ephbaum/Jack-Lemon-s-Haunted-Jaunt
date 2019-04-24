using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public GameEnding gameEnding;
    public Transform player;

    bool m_IsPlayerInRange;

    private void OnTriggerEnter(Collider other)
    {
        m_IsPlayerInRange |= other.transform == player;
    }

    private void OnTriggerExit(Collider other)
    {
        m_IsPlayerInRange &= other.transform != player;
    }

    private void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}