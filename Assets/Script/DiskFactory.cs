using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour {
    public GameObject disk;
    private List<GameObject> used = new List<GameObject>();   //正在被使用的飞碟列表
    private List<GameObject> free = new List<GameObject>();   //空闲的飞碟列表

	public static DiskFactory getInstance(){
		return Singleton<DiskFactory>.Instance;
	}
    public GameObject GetDisk() {
        if (free.Count != 0) {
	                disk = free[0].gameObject;
	                free.Remove(free[0]);
	            }
        if(disk == null) {
            disk = Instantiate(Resources.Load<GameObject>("Prefabs/disk"), new Vector3(0, -4, 0), Quaternion.identity);;
        }
        //添加到使用列表中
        used.Add(disk);
        return disk;
    }

    //回收飞碟
    public void resetDisk(GameObject disk) {
        for(int i = 0;i < used.Count; i++) {
            if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID()) {
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}
