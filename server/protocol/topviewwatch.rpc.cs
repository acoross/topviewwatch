// This file is auto-generated by rpc.compiler

using System;
using System.Collections.Generic;
//using System.Threading.Tasks;

namespace Acoross.tw.Rpc
{
    public enum RpcTypes : int
    {
        Login, 
        Dash
    }

    public enum NotiTypes : int
    {
        NotiPosSync, 
        NotiDashEnd
    }


    public class vec2
    {
        public float x { get; set; }
        public float y { get; set; }
    }

    public class LoginReq
    {
        public string id { get; set; }
        public string pwd { get; set; }
    }

    public class DashReq
    {
        public vec2 orgPos { get; set; }
        public vec2 dir { get; set; }
        public float speed { get; set; }
        public float actionTime { get; set; }
        public int effectCount { get; set; }
    }

    /*
    interface Rpc
    {
        Task<bool> Login(LoginReq req);
        Task<bool> Dash(DashReq req);
    }

    interface Noti
    {
        Task NotiPosSync(vec2 message);
        Task NotiDashEnd(vec2 message);
    }
    */
}
