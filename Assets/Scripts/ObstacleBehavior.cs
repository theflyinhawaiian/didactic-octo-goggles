﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour {
   void Update () {
        var player = GameObject.Find("Player");
        if (transform.position.z < player.transform.position.z - 5)
        {
            Destroy(gameObject);
        }		
	}
}
