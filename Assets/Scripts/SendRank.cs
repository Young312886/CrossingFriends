using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;  // 리스트 사용을 위한 네임스페이스 추가
using System.IO;  // 파일 읽기/쓰기 관련 네임스페이스 추가

public class ScoreManager : MonoBehaviour
{
    private string serverUrl = "url" ; // "https://your-server.com/api/submit-score"; // 서버 URL을 입력하세요

    // 점수 데이터를 서버로 보내는 함수
    public IEnumerator SendScoreToServer(ScoreData scoreData)
    {
        // 점수 데이터를 JSON 형식으로 변환
        string jsonData = JsonUtility.ToJson(scoreData);

        // HTTP POST 요청 생성
        UnityWebRequest request = new UnityWebRequest(serverUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 서버에 요청 보내기
        yield return request.SendWebRequest();

        // 요청 결과 처리
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("점수가 서버에 성공적으로 전송되었습니다.");
        }
        else
        {
            Debug.LogError("점수 전송 실패: " + request.error);
        }
    }

    // 로컬 점수 파일을 서버와 동기화하는 함수
    void SyncLocalScores()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // 인터넷 연결이 가능한 경우
            string localDataPath = Application.persistentDataPath + "/localScores.txt";
            if (File.Exists(localDataPath))
            {
                string json = File.ReadAllText(localDataPath);
                // Wrapper 클래스를 사용해 로컬 데이터를 읽음
                ScoreDataList localScores = JsonUtility.FromJson<ScoreDataList>(json);

                foreach (ScoreData score in localScores.scores)
                {
                    // 서버에 각 점수를 전송하는 코드 (API 호출)
                    StartCoroutine(SendScoreToServer(score));
                }

                // 동기화 후 파일 삭제
                File.Delete(localDataPath);
            }
        }
    }
}

// 리스트 처리를 위한 Wrapper 클래스
[System.Serializable]
public class ScoreDataList
{
    public List<ScoreData> scores;
}

[System.Serializable]
public class ScoreData
{
    public string userId;
    public int score;
    public string timestamp;
}
