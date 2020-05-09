using log4net;
using SusNet.Common.Message;
using SusNet.Common.Utils;
using SuspeSys.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SusNet.Common.SusBusMessage
{
    public class MessageProcesser:SusLog
    {
        private ILog log = LogManager.GetLogger("LogLogger");
        private MessageProcesser()
        {
        }

        public static MessageProcesser Instance { get { return Nested._instance; } }

        private readonly SortedList<string, List<byte>> receivedDataList = new SortedList<string, List<byte>>();
        private MessageProcesser RemoveCache(string key)
        {
            lock (receivedDataList)
            {
                receivedDataList.Remove(key);
                return this;
            }
        }

        public List<MessageBody> ProcessRecvData(string key, byte[] data)
        {
            try
            {
                lock (receivedDataList)
                {
                    List<MessageBody> messageList = new List<MessageBody>();
                    var packageData = new List<byte>(data);
                    //整包
                    if (data.Length % MessageBody.MinSizeOfMessage == 0)
                    {
                        var packSizeNum = data.Length / MessageBody.MinSizeOfMessage;
                        var wholePackage = new List<byte>();
                        for (var index = 0; index < packageData.Count; index++)
                        {
                            //packageData.CopyTo()
                            wholePackage.Add(packageData[index]);
                            if (wholePackage.Count == MessageBody.MinSizeOfMessage)
                            {
                                messageList.Add(new MessageBody(wholePackage.ToArray()));
                                wholePackage = new List<byte>();
                            }
                        }
                        return messageList; //Message.DecodeMessaage2(byteList);// Message.DecodeMessaage(byteList,out offset);
                    }
                    else
                    {
                        log.Error(string.Format("包不完整!..包为:{0}", HexHelper.BytesToHexString(data)));

                    }

                    //List<byte> byteList;
                    //if (receivedDataList.ContainsKey(key))
                    //{
                    //    byteList = receivedDataList[key];
                    //}
                    //else
                    //{
                    //    byteList = new List<byte>();
                    //    receivedDataList.Add(key, byteList);
                    //}
                    //byteList.AddRange(data);
                    //int offset;
                    //try
                    //{
                    //    messageList = new List<MessageBody>() { new MessageBody(data) }; //Message.DecodeMessaage2(byteList);// Message.DecodeMessaage(byteList,out offset);
                    //    byteList.RemoveRange(0, byteList.Count);
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                    //    log.Error(string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString()));
                    //    byteList.Clear();
                    //}
                    return messageList;
                }
            }
            catch (Exception ex)
            {
                // Log.Fatal("Class:{0}\nMethod:{1}\nException:{2}",this.GetType().Name,MethodBase.GetCurrentMethod().Name,ex.ToString());
                log.Error(ex);
                var errMsg = string.Format("Class:{0}\nMethod:{1}\nException:{2}", this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                log.Error(errMsg);
                tcpLogInfo.Error(errMsg);
                tcpLogHardware.Error(errMsg);
                tcpLogHardware.Info(errMsg);
                return new List<MessageBody>();
            }
        }

        /// <summary>
        /// 单类懒加载
        /// </summary>
        private static class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly MessageProcesser _instance = new MessageProcesser();
        }
    }
}
