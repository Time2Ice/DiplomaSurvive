using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public class Requests 
{
    private readonly string token;
    private Coroutine _sighIn;
    private Coroutine _authorise;
    private Coroutine _setScores;
    private Coroutine _getPosition;

    private AsyncProcessor _asyncProcessor;
    public IPlayerInfoHolder PlayerInfoHolder { get; set; }

    public Requests(AsyncProcessor asyncProcessor)
    {
        _asyncProcessor = asyncProcessor;
        token =
            "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIyIiwibmJmIjoxNTc2Njg3NjY3LCJleHAiOjE1NzcyOTI0NjcsImlhdCI6MTU3NjY4NzY2N30.5k8tGmCohSvp_A9jbBbChzSnHDdynwWWVVHIvtmZpAc";
    }


    IEnumerator GetEvent()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(String.Format("http://194.32.79.63:8000/api/event")))
        {
            req.SetRequestHeader("Authorization", token);
            yield return req.Send();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<GetEventResponse> info = JsonUtility.FromJson<ApiResponse<GetEventResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");
            if (info.errors.Length == 0)
            {
                GetEventResponse evn = info.data;
                Debug.Log($"ID: {evn.id} Title: {evn.title} Start: {evn.start} Finish: {evn.finish}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }

    public void GetLeaderBoard()
    {
        if (_getPosition != null)
        {
            _asyncProcessor.StopCoroutine(_getPosition);
            _getPosition = null;
        }
        _getPosition=_asyncProcessor.StartCoroutine(GetLeaderBoardCoroutine());
    }


    IEnumerator GetLeaderBoardCoroutine()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(String.Format("http://194.32.79.63:8000/api/leaderboard")))
        {
            req.SetRequestHeader("Authorization", PlayerInfoHolder.Token);
            yield return req.Send();

            while (!req.isDone)
                yield return null;

            byte[] result = req.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<GetLeaderboardResponse> info =
                JsonUtility.FromJson<ApiResponse<GetLeaderboardResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");

            if (info.errors.Length == 0)
            {
                GetLeaderboardResponse evn = info.data;
                PlayerInfoHolder.Position = evn.place;
                Debug.Log($"Place: {evn.place} PlayersCount: {evn.players.Length}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }

    public void SetScore()
    {
        if (_setScores != null)
        {
            _asyncProcessor.StopCoroutine(_setScores);
            _setScores = null;
        }
        _setScores = _asyncProcessor.StartCoroutine(SetUserScoresCoroutine());
    }


    IEnumerator SetUserScoresCoroutine()
    {
        var body = new SetUserScoresRequest
        {
            scores = PlayerInfoHolder.MaxPoints
        };

        using (UnityWebRequest req =
            UnityWebRequest.Put("http://194.32.79.63:8000/api/Player/scores", JsonUtility.ToJson(body)))
        {
            req.SetRequestHeader("Authorization", PlayerInfoHolder.Token);
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.Send();

            while (!req.isDone)
                yield return null;

            byte[] result = req.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<SetUserScoresResponse> info =
                JsonUtility.FromJson<ApiResponse<SetUserScoresResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");

            if (info.errors.Length == 0)
            {
                SetUserScoresResponse evn = info.data;
                Debug.Log($"Scores: {evn.scores}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
            GetLeaderBoard();
        }
    }

    public void SignInRequest()
    {
        if(_sighIn!=null)
        {
            _asyncProcessor.StopCoroutine(_sighIn);
            _sighIn = null;
        }
        _sighIn=_asyncProcessor.StartCoroutine(SignIn());
    }

   public IEnumerator SignIn()
    {
        Debug.Log("hello");
        var body = new SignInUserRequest
        {
            name = PlayerInfoHolder.Name
        };

        var json = JsonUtility.ToJson(body);

        using (var request = new UnityWebRequest("http://194.32.79.63:8000/api/Player/sign-in", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.Send();

            while (!request.isDone)
                yield return null;

            byte[] result = request.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<SignInUserResponse> info = JsonUtility.FromJson<ApiResponse<SignInUserResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");

            if (info.errors.Length == 0)
            {
                SignInUserResponse evn = info.data;
                PlayerInfoHolder.Token = "Bearer " + evn.token;
                Debug.Log($"Scores: {evn.scores} Reward: {evn.reward} Token: {evn.token}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }

    public void Authenticate()
    {
        if (_authorise != null)
        {
            _asyncProcessor.StopCoroutine(_authorise);
            _authorise = null;
        }
        _authorise = _asyncProcessor.StartCoroutine(AuthenticateCoroutine());
    }


    IEnumerator AuthenticateCoroutine()
    {
        var body = new AuthenticateUserRequest()
        {
            name = PlayerInfoHolder.Name
        };

        var json = JsonUtility.ToJson(body);

        using (var request = new UnityWebRequest("http://194.32.79.63:8000/api/Player/authentication", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.Send();

            while (!request.isDone)
                yield return null;

            byte[] result = request.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<AuthenticateUserResponse> info = JsonUtility.FromJson<ApiResponse<AuthenticateUserResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");

            if (info.errors.Length == 0)
            {
                AuthenticateUserResponse evn = info.data;
                Debug.Log($"Scores: {evn.scores} Reward: {evn.reward} Token: {evn.token}");
                PlayerInfoHolder.Token = "Bearer " + evn.token;
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }
}