using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenPose;

public class AnimationTest : MonoBehaviour
{
    public Animation obj;
    private List<Transform> transformList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        Transform[] father = obj.GetComponentsInChildren<Transform>();
        List<Transform> temp = new List<Transform>();
        foreach (var child in father)
        {
            if (child.name.Substring(0, 5) == "dummy")
                temp.Add(child);
        }
        transformList = new List<Transform> { 
            // sort by openpose keypoints order
            temp.Find(element => { if (element.name == "dummy_headq") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_neck") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_lshoulder") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_lelbow") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_lwrist") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_rshoulder") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_relbow") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_rwrist") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_hips3") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_lpelvis") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_lknee") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_lankle") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_rpelvis") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_rknee") return true; else return false; }),
            temp.Find(element => { if (element.name == "dummy_rankle") return true; else return false; }),
        };
        Debug.Log(transformList.Count);
        foreach (var element in transformList)
            Debug.Log(element.name);

        Debug.Log(obj.ToString());
    }

    public void TestAnimation(ref OPDatum datum)
    {
        if (datum.poseKeypoints == null)
        {
            obj["Take 001"].speed = 0;
        }
        else {
            // test 4 keypoints
            if(datum.poseKeypoints.Get(0, 0, 2) < 0.05f || datum.poseKeypoints.Get(0, 5, 2) < 0.05f || datum.poseKeypoints.Get(0, 6, 2) < 0.05f || datum.poseKeypoints.Get(0, 7, 2) < 0.05f)
                obj["Take 001"].speed = 0;
            Vector2 hhead = new Vector2(datum.poseKeypoints.Get(0, 0, 0), datum.poseKeypoints.Get(0, 0, 1));
            Vector2 hlshoulder = new Vector2(datum.poseKeypoints.Get(0, 5, 0), datum.poseKeypoints.Get(0, 5, 1));
            Vector2 hlelbow = new Vector2(datum.poseKeypoints.Get(0, 6, 0), datum.poseKeypoints.Get(0, 6, 1));
            Vector2 hlWrist = new Vector2(datum.poseKeypoints.Get(0, 7, 0), datum.poseKeypoints.Get(0, 7, 1));

            Vector2 ohead = new Vector2(transformList[0].position.x, transformList[0].position.y);
            Vector2 olshoulder = new Vector2(transformList[5].position.x, transformList[5].position.y);
            Vector2 olelbow = new Vector2(transformList[6].position.x, transformList[6].position.y);
            Vector2 olWrist = new Vector2(transformList[7].position.x, transformList[7].position.y);

            float rangeDifference = Vector2.Angle(hhead - hlshoulder, hlWrist - hlshoulder) -
                Vector2.Angle(ohead - olshoulder, olWrist - olshoulder);
            Debug.Log(Vector2.Angle(ohead - olshoulder, olWrist - olshoulder));
            if (Mathf.Abs(rangeDifference) < 20.0f)
                obj["Take 001"].speed = 1;
            else
                obj["Take 001"].speed = 0;            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
