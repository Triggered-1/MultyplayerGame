//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class VerticalPlatform : MonoBehaviour
//{
//    private PlatformEffector2D effector;

//    [SerializeField] private float waitTime;
//    // Start is called before the first frame update
//    void Start()
//    {
//        effector = GetComponent<PlatformEffector2D>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!InputManager.instance.DownButtonPressed)
//        {
//            waitTime = 0.1f;
//        }
//        if (InputManager.instance.DownButtonPressed)
//        {
//            if (waitTime <= 0)
//            {
//                effector.rotationalOffset = 180f;
//                waitTime = 0.1f;
//            }
//            else
//            {
//                waitTime -= Time.deltaTime;
//            }
//        }

//        if (InputManager.instance.JumpButtonPressed)
//        {
//            effector.rotationalOffset = 0;
//        }
//    }
//}
