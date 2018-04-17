using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {
	private IUserAction action;
    public DiskFactory diskFactory;
    public UserGUI userGui;
    public ScoreRecorder scoreRecorder;

    private Queue<GameObject> disk_queue = new Queue<GameObject>();          //游戏场景中的飞碟
    private List<GameObject> disk_alive = new List<GameObject>();          //没有被打中的飞碟
    private int round = 1;
    private bool playing = false;
    private int trial;
	private int throwNum;
	private float speed;
	private Vector3 direction;
	public Color[] setColor = {Color.white,Color.black,Color.yellow,Color.blue,Color.green,Color.red,};


    void Start () {
        SSDirector director = SSDirector.GetInstance();
		director.CurrentScenceController = this;
		diskFactory = Singleton<DiskFactory>.Instance;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        userGui = gameObject.AddComponent<UserGUI>() as UserGUI;
		trial = 0;
		round = 1;
		speed = 2f;
    }

	void Update () {
        if(playing) {
			Vector3 pos = Vector3.zero;
			if (Input.GetButtonDown("Fire1"))
	            {
	                pos = Input.mousePosition;
	            }
	        Ray ray = Camera.main.ScreenPointToRay(pos);
	        RaycastHit hit;
	            //射线打中物体
	            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag.Contains("Finish")) {
	                disk_alive.Remove(hit.collider.gameObject);
	                //记分员记录分数
	                scoreRecorder.Record();
					diskFactory.resetDisk(hit.collider.gameObject);
				}
            //游戏结束
            if (round == 4) {
                userGui._gameOver();
				playing = false;
            }
            //设定一个定时器，发送飞碟，游戏开始
            if (!playing) {
                InvokeRepeating("LoadResources", 1f, speed);
                playing = true;
            }
            //发送飞碟
			InvokeRepeating("LoadResources", 1f, speed);

            throwDisk();
        }
    }

    public void LoadResources()
    {
		if (round == 1) throwNum = 1;
		if (round == 2) throwNum = 2;
		if (round == 3) throwNum = 3;
        for (int i = 0;i < throwNum;i++) {
			disk_queue.Enqueue(diskFactory.GetDisk());
		}
    }

    private void throwDisk() {
		trial++;
        if (disk_queue.Count != 0) {
            GameObject disk = disk_queue.Dequeue();
            disk_alive.Add(disk);
            disk.SetActive(true);
			int chooseColor = Random.Range(0, 5);
            disk.GetComponent<Renderer>().sharedMaterial.color = setColor[chooseColor];
            disk.transform.position = new Vector3(Random.Range(-2.5f,2.5f), Random.Range(1f, 4f), 0);
            direction.x = direction.x * Random.Range(-1, 1);
            disk.GetComponent<Rigidbody>().AddForce(direction * Random.Range(speed * 5, speed * 15) / 10, ForceMode.Impulse);
        }

        for (int i = 0; i < disk_alive.Count; i++)
        {
            GameObject temp = disk_alive[i];
            //飞碟飞出摄像机视野也没被打中
            if (temp.transform.position.y < -10 && temp.gameObject.activeSelf == true)
            {
                diskFactory.resetDisk(disk_alive[i]);
                disk_alive.Remove(disk_alive[i]);
                userGui.MissUFO();
            }
        }
		if (trial == 9) {
			round++;
			trial = 0;
		}
    }
	public int ShowScore()	{
	   return scoreRecorder.score;
   }
   public void gameStart() {
	   playing = true;
   }
   public void gameOver() {
	   playing = false;
   }
}
