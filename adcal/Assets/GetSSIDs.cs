using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ManagedNativeWifi.Simple;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class GetSSIDs : MonoBehaviour {
    [SerializeField] string[] ssids;
    [SerializeField] string[] ssidshash;

    [SerializeField] string[] strings;
    // Use this for initialization
    void Start()
    {
        ssids = NativeWifi.GetAvailableNetworkSsids().ToArray();
        // SSIDを取得
        // 接続可能なものを取得している

        ssids = ssids.Distinct().ToArray();
        // 環境により重複するので重複排除

        var sha = System.Security.Cryptography.SHA256.Create();
        // hash用オブジェクトを作成



        ssidshash = new string[ssids.Length];
        // hashの文字列（16進数）を格納するstring[]を初期化

        for(int i = 0;i<ssids.Length;i++)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(ssids[i]);
            // string -> byte[] 変換

            var result = sha.ComputeHash(data);
            // byte[] でハッシュ値を作成

            ssidshash[i] =  BitConverter.ToString(sha.Hash).ToLower().Replace("-", "");
            //16進数に変換

        }
        sha.Clear();
        // hashのオブジェクトを開放

        strings = Regex.Split(ssidshash[0], @"(?<=\G.{4})(?!$)");
        // 例えばこんな感じに4桁ずつ区切ったりして使うといいかも
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
