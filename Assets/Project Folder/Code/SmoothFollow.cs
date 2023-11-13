using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    #region Consts
    private const float SMOOTH_TIME = 0.1f;
    #endregion

    #region Public Properties
    public float cameraSpeed = 0.0075f;

    public bool LockX;
    public float offSetX;
    public float offSetY;
    public float offSetZ;
    private Vector3 offSetTotal;
    private Vector3 offSetTotal_Free;
    private Vector3 offSetTotal_Lock;
    public bool LockY;
    public bool LockZ;
    public bool useSmoothing;
    public bool Lock_On = true;
    private Transform target;
    public Transform target1;
    public Transform target2;
    //public Transform Lock_on_Target;
    public GameObject Lock_on_Target_Object;
    private Vector3 Lock_on_Transform;
    public GameObject hudElements;

    public bool Lock_on_Target_Avaliable;
    #endregion

    #region Private Properties
    private Transform thisTransform;
    private Vector3 velocity;
    #endregion

    bool hudActive = true;

    private void Awake()
    {
        if (Battle_Controller.game_Mode[0] == 1) target = target1;
        else target = target2;

        thisTransform = transform;
        velocity = new Vector3(0.7f, 0.7f, 0.7f);
        offSetTotal = new Vector3(offSetX, offSetY, offSetZ);
        offSetTotal_Lock = new Vector3(-offSetX
                                       , offSetY
                                       , offSetZ);
        Lock_On = true;
    }

    void Update()

    {
        Lock_on_Target_Avaliable = Lock_on_Target_Object.activeSelf;


        if (Battle_Controller.game_Mode[0] == 1) target = target1;
        else target = target2;
        Lock_on_Transform = target.position - Lock_on_Target_Object.transform.position;
        Lock_on_Transform.y = 0f;
        Lock_on_Transform = Lock_on_Transform.normalized;
        if (hudActive)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                hudElements.SetActive(false);
                hudActive = false;
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                hudElements.SetActive(true);
                hudActive = true;
            }
        }

        //offSetTotal = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * cameraSpeed, Vector3.up) * offSetTotal;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Lock_On = !Lock_On;
            if (Lock_On)
            {
                offSetTotal_Lock = offSetTotal_Free;
            }
        }
        if (Lock_On)
        {
            offSetTotal_Free = new Vector3(-offSetZ * Lock_on_Transform.x
                                        //* Quaternion.FromToRotation(Vector3.up, Lock_on_Transform.normalized).x
                                        , offSetY
                                        , -offSetZ * Lock_on_Transform.z);
            //transform.position = target.position + offSetTotal;
            //transform.LookAt(new Vector3(Lock_on_Target_Object.transform.position.x, Lock_on_Target_Object.transform.position.y, Lock_on_Target_Object.transform.position.z));
            offSetTotal = offSetTotal_Free;

            //print(Lock_on_Transform.x + "    " + Lock_on_Transform.z);
        }
        else
        {
            //print(Input.GetAxisRaw("Vertical2") + " " + Input.GetAxisRaw("Horizontal2"));
            if (Input.GetKey(KeyCode.UpArrow) )
                offSetTotal_Lock = Quaternion.AngleAxis(cameraSpeed * 0.2f, Vector3.right) * offSetTotal_Lock;

            if (Input.GetKey(KeyCode.DownArrow) )
                offSetTotal_Lock = Quaternion.AngleAxis(cameraSpeed * 0.2f, Vector3.left) * offSetTotal_Lock;

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal2") > 0.6f)
                offSetTotal_Lock = Quaternion.AngleAxis(cameraSpeed * 0.2f, Vector3.down) * offSetTotal_Lock;

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal2") < -0.6f)
                offSetTotal_Lock = Quaternion.AngleAxis(cameraSpeed * 0.2f, Vector3.up) * offSetTotal_Lock;
            offSetTotal = offSetTotal_Lock;
        }



        /*
        transform.position = target.position + offSetTotal;
        if (Lock_On) transform.LookAt(Lock_on_Target_Object.transform.position);
        else
            transform.LookAt(target.position);
            */
        // transform.position = target.position + offSetTotal;


    }

    // ReSharper disable UnusedMember.Local
    private void FixedUpdate()
    // ReSharper restore UnusedMember.Local
    {
        var newPos = Vector3.zero;

        if (useSmoothing)
        {
            Vector3 vec = new Vector3(0, 0, offSetZ );
            newPos = Vector3.SmoothDamp(thisTransform.position, target.position + vec
                , ref velocity, SMOOTH_TIME);
            //newPos.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
            ///newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
            //newPos.z = Mathf.SmoothDamp(thisTransform.position.z, target.position.z + offSetZ, ref velocity.z, SMOOTH_TIME);
            //newPos.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
            //newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y + offSetY, ref velocity.y, SMOOTH_TIME);
            //newPos.z = Mathf.SmoothDamp(thisTransform.position.z, target.position.z + offSetZ, ref velocity.z, SMOOTH_TIME);
        }
        else
        {
            newPos.x = target.position.x + offSetX;
            newPos.y = target.position.y + offSetY;
            newPos.z = target.position.z + offSetZ;
        }

        #region Locks
        if (LockX)
        {
            newPos.x = thisTransform.position.x;
        }

        if (LockY)
        {
            newPos.y = thisTransform.position.y;
        }

        if (LockZ)
        {
            newPos.z = thisTransform.position.z;
        }
        #endregion

        //transform.position = Vector3.Slerp(transform.position, newPos, Time.time);
        //transform.position = Vector3.Lerp(transform.position, newPos, 0.125f);
        float VecZ = offSetZ;
        //print(offSetTotal);
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, target.position.x + offSetTotal.x, ref velocity.x, SMOOTH_TIME)
                                    , Mathf.SmoothDamp(transform.position.y, target.position.y + offSetTotal.y, ref velocity.y, SMOOTH_TIME)
                                    , Mathf.SmoothDamp(transform.position.z, target.position.z + offSetTotal.z - VecZ, ref velocity.z, SMOOTH_TIME)
                            );
        if (Lock_On)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Lock_on_Target_Object.transform.position - transform.position), Time.time * cameraSpeed / 25);
        }
        else
        {


            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), Time.time * cameraSpeed / 15);
        }
    }
}

/*
 using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	#region Consts
	private const float SMOOTH_TIME = 0.3f;
	#endregion
	
	#region Public Properties
	public bool LockX;
	public float offSetZ;
	public bool LockY;
	public bool LockZ;
	public bool useSmoothing;
	public Transform target;
	public GameObject hudElements;
	#endregion
	
	#region Private Properties
	private Transform thisTransform;
	private Vector3 velocity;
	#endregion

	bool hudActive = true;
	
	private void Awake()
	{
		thisTransform = transform;
		velocity = new Vector3(0.5f, 0.5f, 0.5f);
	}

	void Update()
	{
		if(hudActive)
		{
			if (Input.GetKeyDown(KeyCode.H))
			{
				hudElements.SetActive (false);
				hudActive = false;
			}

		}
		else
		{
			if (Input.GetKeyDown(KeyCode.H))
			{
				hudElements.SetActive (true);
				hudActive = true;
			}
		}
	}

	// ReSharper disable UnusedMember.Local
	private void LateUpdate()
		// ReSharper restore UnusedMember.Local
	{
		var newPos = Vector3.zero;
		
		if (useSmoothing)
		{
			newPos.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
			newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
			newPos.z = Mathf.SmoothDamp(thisTransform.position.z, target.position.z + offSetZ, ref velocity.z, SMOOTH_TIME);
		}
		else
		{
			newPos.x = target.position.x;
			newPos.y = target.position.y;
			newPos.z = target.position.z;
		}
		
		#region Locks
		if (LockX)
		{
			newPos.x = thisTransform.position.x;
		}
		
		if (LockY)
		{
			newPos.y = thisTransform.position.y;
		}
		
		if (LockZ)
		{
			newPos.z = thisTransform.position.z;
		}
		#endregion
		
		transform.position = Vector3.Slerp(transform.position, newPos, Time.time);
	}
}
*/


