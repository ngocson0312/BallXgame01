using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{

	Vector2 startPos, endPos, direction; // chạm vào vị trí bắt đầu, chạm vào vị trí kết thúc, hướng vuốt
	float touchTimeStart, touchTimeFinish, timeInterval; // để tính thời gian vuốt để kiểm soát lực ném theo hướng Z

	[SerializeField]
	float throwForceInXandY = 1f; // để kiểm soát lực ném theo hướng X và Y

	[SerializeField]
	float throwForceInZ = 50f; // để kiểm soát lực ném theo hướng z

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}


	void Update()
	{


		TouchScreen();





	}

	public void TouchScreen()
	{
		// nếu bạn chạm vào màn hình
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{

			// nhận vị trí chạm và đánh dấu thời gian khi bạn chạm vào màn hình
			touchTimeStart = Time.time;
			startPos = Input.GetTouch(0).position;

		}


		// nếu bạn thả ngón tay ra
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{

			// marking time when you release it
			touchTimeFinish = Time.time;

			// tính khoảng thời gian vuốt
			timeInterval = touchTimeFinish - touchTimeStart;

			// getting release finger position
			endPos = Input.GetTouch(0).position;

			// tính toán hướng
			direction = startPos - endPos;


			//thêm lực vào bóng cứng trong không gian 3D tùy thuộc vào thời gian vuốt, hướng và lực ném
			rb.isKinematic = false;
			rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, throwForceInZ / timeInterval);

			// Destroy ball in 4 seconds
			Destroy(gameObject, 3f);

		}
	}
}
