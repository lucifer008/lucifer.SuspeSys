﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sus.Net.Common.Entity
{
    public class ClientUserInfo
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="gid">组ID</param>
        /// <param name="uid">用户ID，为空或null则代表公司</param>
        public ClientUserInfo(string gid, string uid)
        {
            this.gid = gid;
            this.uid = uid;
            if (!MaintrackNumberList.Contains(uid))
            {
                MaintrackNumberList.Add(uid);
            }
        }
        /// <summary>
        /// 公司ID
        /// </summary>
        public string gid { get; set; } = "1";
        public readonly List<string> MaintrackNumberList = new List<string>();
        /// <summary>
        /// 用户ID，为空或null则代表公司
        /// </summary>
        public string uid { get; set; }

        public override string ToString()
        {
            return "gid:" + gid + " uid:" + (string.IsNullOrEmpty(uid) ? "所有用户" : uid) + "主轨:" + JsonConvert.SerializeObject(MaintrackNumberList); ;
        }
    }
}
