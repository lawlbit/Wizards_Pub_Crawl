﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Encapsulates the hero character movement logic.
/// This class is needed to isolate the logic for unit testing without any Unity dependencies.
/// </summary>
public class CharacterMovement
{
    private float speed;

    public CharacterMovement(float speed)
    {
        if (speed > 0)
        {
            this.speed = speed;
        }
        else
        {
            this.speed = 0;
        }
    }

    // Mathematically speaking it is now correct.
    public Vector3 Calculate(bool forward, bool back, bool left, bool right)
    {
        Vector3 posX = new Vector3(1, 0, -1);
        Vector3 posZ = new Vector3(1, 0, 1);
        if (forward || back || left || right)
        {
            int x = 0;
            int z = 0;

            if (forward)
            {
                z++;
            }

            if (back)
            {
                z--;
            }

            if (left)
            {
                x--;
            }

            if (right)
            {
                x++;
            }

            Vector3 dir = (x * posX) + (z * posZ);

            // Normalize the direction vector then multiply it with the desired magnitude.
            return speed * dir.normalized;
        }

        return new Vector3(0, 0, 0);
    }

    public float GetSpeed()
    {
        return speed;
    }

	public void SetSpeed(float s)
	{
		speed = s;
	}
}