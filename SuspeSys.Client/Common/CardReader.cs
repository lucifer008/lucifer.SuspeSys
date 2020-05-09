using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Reflection;
using System.ComponentModel;

namespace FangteCommon
{
	/// <summary>
	/// 串口事件
	/// </summary>
	public class ReceiveDataEventArgs : EventArgs
	{
		private string commdata;

		public ReceiveDataEventArgs(string data)
		{
			commdata = data;
		}

		public string CommData
		{
			get
			{
				return commdata;
			}
		}
	}

	public delegate void ReceiveDataEventHandle(object sender, ReceiveDataEventArgs e);

	public delegate void WirteDataEventHandle(object sender, EventArgs e);

	/// <summary>
	/// 功能：			串口、USB通信类（消息方式）
	/// 调用方法：		private FangteCommon.FMCardReader comm;
	///					comm = FangteCommon.FMCardReader.GetPortInstance(FangteCommon.CardReaderTypeEnum.SerialPort , port);
	///					comm.ReceiveData += new FangteCommon.ReceiveDataEventHandle(comm_ReceiveData);
	///					if (comm.IsClosed) {
	///						comm.Open();
	///					}
	/// </summary>
	public class FMCardReader : NativeWindow
	{
		#region 定义
		/// <summary>
		/// 串口号
		/// </summary>
		private int nCommNo;
		/// <summary>
		/// USB钩子事件类
		/// </summary>
		private static KeyHook hk;
		/// <summary>
		/// 读卡器类型
		/// </summary>
		private CardReaderTypeEnum _cardreadertype;
		/// <summary>
		/// 波特率
		/// </summary>
		private BAUD nBaud;
		/// <summary>
		/// 数据位
		/// </summary>
		private BYTESIZE nByteSize;
		/// <summary>
		/// 串口状态
		/// </summary>
		private PORTSTATUS portstatus;
		/// <summary>
		/// USB读卡器输出长度
		/// </summary>
		private static int _usb_ref_length = 10;

		private static ArrayList ports;

		public event ReceiveDataEventHandle ReceiveData;

		//public event WirteDataEventHandle WritePort;

		/* Message */
		private System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();
		private const int WM_USER = 0x0400;
		private const int WM_COMM_DATA = WM_USER + 1000;
		private const int WM_WRITE_DATA = WM_USER + 1001;
		private const int WM_USB_DATA = WM_USER + 1002;
		#endregion

		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="cardtype">刷卡器类型：USB,串口</param>
		/// <param name="serialportno">串口号</param>
		private FMCardReader(CardReaderTypeEnum cardtype, int serialportno)
		{
			_cardreadertype = cardtype;

			nCommNo = serialportno;
			nBaud = BAUD._9600;
			nByteSize = BYTESIZE._8;
			portstatus = PORTSTATUS.Closed;
			//hk = new KeyHook();

			CreateParams cp = new CreateParams();
			this.CreateHandle(cp);
		}
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="commno">串口号</param>
		private FMCardReader(int commno)
		{
			_cardreadertype = CardReaderTypeEnum.SerialPort;
			nCommNo = commno;
			nBaud = BAUD._9600;
			nByteSize = BYTESIZE._8;
			portstatus = PORTSTATUS.Closed;

			// Create Handle
			CreateParams cp = new CreateParams();
			this.CreateHandle(cp);
		}

		static FMCardReader()
		{
			ports = new ArrayList();
		}
		#endregion

		/// <summary>
		/// 获取实例
		/// </summary>
		/// <param name="cardreadertype">刷卡器类型：USB,串口</param>
		/// <param name="serialportno">串口号</param>
		/// <returns></returns>
		public static FMCardReader GetPortInstance(CardReaderTypeEnum cardreadertype, int serialportno)
		{
			FMCardReader port = new FMCardReader(cardreadertype, serialportno);
			return port;
		}

		/// <summary>
		/// 获取实例
		/// </summary>
		/// <param name="commno">串口号</param>
		/// <returns></returns>
		public static FMCardReader GetPortInstance(int commno)
		{
			for (int i = 0; i < ports.Count; ++i)
			{
				if (((FMCardReader)ports[i]).nCommNo == commno)
				{
					return (FMCardReader)ports[i];
				}
			}

			FMCardReader port = new FMCardReader(commno);
			port.nCommNo = commno;
			ports.Add(port);
			return port;
		}

		#region	 属性
		/// <summary>
		/// 串口号
		/// </summary>
		public int CommNo
		{
			get
			{
				return nCommNo;
			}
			set
			{
				nCommNo = value;
			}
		}

		/// <summary>
		/// 波特率
		/// </summary>
		public BAUD Baud
		{
			get
			{
				return nBaud;
			}
			set
			{
				nBaud = value;
			}
		}

		/// <summary>
		/// 数据位
		/// </summary>
		public BYTESIZE ByteSize
		{
			get
			{
				return nByteSize;
			}
			set
			{
				nByteSize = value;
			}
		}

		/// <summary>
		/// 串口状态
		/// </summary>
		public PORTSTATUS PortStatus
		{
			get
			{
				return portstatus;
			}
		}

		/// <summary>
		/// 是否打开
		/// </summary>
		public bool IsOpen
		{
			get
			{
				return portstatus == PORTSTATUS.Opened;
			}
		}

		/// <summary>
		/// 是否关闭
		/// </summary>
		public bool IsClosed
		{
			get
			{
				return portstatus == PORTSTATUS.Closed;
			}
		}

		/// <summary>
		/// 刷卡器类型
		/// </summary>
		public CardReaderTypeEnum CardReaderType
		{
			get { return _cardreadertype; }
			set { _cardreadertype = value; }
		}

		/// <summary>
		/// 获取或设置USB读卡器的输出长度
		/// </summary>
		public static int USB_REF_LENGTH
		{
			get { return _usb_ref_length; }
			set { _usb_ref_length = value; }
		}
		#endregion

		#region Import dll function
		[DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		[DllImport("user32.dll")]
		public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lPatam);

		/// <summary>
		/// 打开串口
		/// </summary>
		/// <param name="commno">串口号</param>
		/// <param name="baud">串行波特率</param>
		/// <param name="bytesize">数据位长度</param>
		/// <param name="handle">当前句柄</param>
		/// <returns></returns>
		private bool OpenComm(int commno, int baud, int bytesize, IntPtr handle)
		{
			try
			{
				sp.PortName = "COM" + commno.ToString();
				sp.BaudRate = baud;
				sp.DataBits = bytesize;
				//sp.StopBits = StopBits;
				handle = this.Handle;
				sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(sp_DataReceived);
				sp.Open();
			}
			catch
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// 关闭串口
		/// </summary>
		/// <param name="commno"></param>
		private void CloseComm(int commno)
		{
			try
			{
				if(sp.IsOpen)
					sp.Close();
			}
			catch
			{
				
			}
		}

		/// <summary>
		/// 串口读出的数据
		/// </summary>
		private string sp_icno = string.Empty;
		private string usb_icno = string.Empty;
		/// <summary>
		/// 处理SerialPort对象的数据接收事件的方法
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			string tmp_icno = sp.ReadLine();
			if (!string.IsNullOrEmpty(tmp_icno))
			{
				sp_icno = tmp_icno.Length > 14 ? tmp_icno.Substring(1, 14) : tmp_icno;
				ReleaseCapture();
				SendMessage(this.Handle, WM_COMM_DATA, 0, 0);//发送消息到窗口
			}
		}

		/// <summary>
		/// 把串口读出的数据传递到窗口
		/// </summary>
		/// <param name="commno"></param>
		/// <param name="data"></param>
		private void ReadData(int commno, out string data)
		{
			try
			{
				data = sp_icno;
			}
			catch (Exception ex)
			{
				data = "";
				MessageBox.Show(ex.Message, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 把USB读出的数据传递到窗口
		/// </summary>
		/// <param name="data"></param>
		private void ReadData(out string data)
		{
			try
			{
				data = usb_icno;
			}
			catch (Exception ex)
			{
				data = "";
				MessageBox.Show(ex.Message, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion

		/// <summary>
		/// 打开串口
		/// </summary>
		/// <param name="commno"></param>
		/// <param name="baud"></param>
		/// <param name="bytesize"></param>
		/// <param name="handle"></param>
		/// <returns></returns>
		public bool Open(int commno, BAUD baud, BYTESIZE bytesize)
		{
			nCommNo = commno;
			nBaud = baud;
			nByteSize = bytesize;
			return Open();
		}

		/// <summary>
		/// 打开串口 or USB
		/// </summary>
		/// <returns></returns>
		public bool Open()
		{
			if (CardReaderType == CardReaderTypeEnum.SerialPort)
			{
				try
				{
					if (OpenComm(nCommNo, (int)nBaud, (int)nByteSize, this.Handle))
					{
						portstatus = PORTSTATUS.Opened;
					}
					else
					{
						if (nCommNo != 0)
						{
							MessageBox.Show("Failed to Open Port !");
						}
						return false;
					}
				}
				catch
				{
					return false;
				}
			}
			else
			{
				try
				{
					hk = new KeyHook(USB_REF_LENGTH);
					hk.ReceiveDataEvent += new KeyHook.ReceiveData(hk_ReceiveDataEvent);
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 销毁资源
		/// </summary>
		public void Dispose()
		{
			Close();
		}

		/// <summary>
		/// 关闭串口 or USB
		/// </summary>
		public void Close()
		{
			if (CardReaderType == CardReaderTypeEnum.SerialPort)
			{
				if (portstatus == PORTSTATUS.Opened)
				{
					CloseComm(nCommNo);
					portstatus = PORTSTATUS.Closed;
				}
			}
			else
			{
				if (hk != null)
				{
					hk.ReceiveDataEvent -= new KeyHook.ReceiveData(hk_ReceiveDataEvent);
					hk.Stop();
				}
			}
		}

		/// <summary>
		/// 卡号输出
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnReceiveData(ReceiveDataEventArgs e)
		{
			if (ReceiveData != null)
			{
				ReceiveData(this, e);
			}
		}

		/// <summary>
		/// 内码转换(串口转USB)
		/// </summary>
		/// <param name="cardno"></param>
		/// <returns></returns>
		public string CommToUSB(string cardno)
		{
			try
			{
				string newcardno = string.Empty;
				if (cardno.Length > 10)
				{
					newcardno = cardno.Trim().Replace("~", "");
					newcardno = Int32.Parse(newcardno.Substring(6, 6), System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
					newcardno = newcardno.PadLeft(10, '0');
				}
				else
				{
					newcardno = cardno;
				}
				return newcardno;
			}
			catch
			{
				return "";
			}
		}

		/// <summary>
		/// USB输出
		/// </summary>
		/// <param name="str"></param>
		private void hk_ReceiveDataEvent(string str)
		{
			string tmp_icno = str;
			if (!string.IsNullOrEmpty(tmp_icno))
			{
				usb_icno = str.Trim();
				ReleaseCapture();
				SendMessage(this.Handle, WM_USB_DATA, 0, 0);//发送消息到窗口
			}
		}

		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		protected override void WndProc(ref Message m)
		{
			// Listen for messages that are sent to the button window. Some messages are sent
			// to the parent window instead of the button's window.

			switch (m.Msg)
			{
				case WM_COMM_DATA:
					string data = null;
					ReadData(nCommNo, out data);
					if (data == null) data = "No Data";
					ReceiveDataEventArgs e = new ReceiveDataEventArgs(data.Length > 14 ? data.Substring(1, 14) : data);
					OnReceiveData(e);
					break;

				case WM_WRITE_DATA:
					//OnStartWrite(new EventArgs());
					break;

				case WM_USB_DATA:
					string data1 = null;
					ReadData(out data1);
					if (data1 == null) data1 = "No Data";
					OnReceiveData(new ReceiveDataEventArgs(data1));
					break;
			}

			base.WndProc(ref m);
		}

		private class KeyHook
		{
			[StructLayout(LayoutKind.Sequential)]
			private class POINT
			{
				public int x;
				public int y;
			}

			[StructLayout(LayoutKind.Sequential)]
			private class MouseHookStruct
			{
				public POINT pt;
				public int hwnd;
				public int wHitTestCode;
				public int dwExtraInfo;
			}

			[StructLayout(LayoutKind.Sequential)]
			private class MouseLLHookStruct
			{
				public POINT pt;
				public int mouseData;
				public int flags;
				public int time;
				public int dwExtraInfo;
			}

			[StructLayout(LayoutKind.Sequential)]
			private class KeyboardHookStruct
			{
				public int vkCode;
				public int scanCode;
				public int flags;
				public int time;
				public int dwExtraInfo;
			}

			[DllImport("user32.dll", CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall, SetLastError = true)]
			private static extern int SetWindowsHookEx(
				int idHook,
				HookProc lpfn,
				IntPtr hMod,
				int dwThreadId);

			[DllImport("user32.dll", CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall, SetLastError = true)]
			private static extern int UnhookWindowsHookEx(int idHook);

			[DllImport("user32.dll", CharSet = CharSet.Auto,
			CallingConvention = CallingConvention.StdCall)]
			private static extern int CallNextHookEx(
				int idHook,
				int nCode,
				int wParam,
				IntPtr lParam);


			[DllImport("user32")]
			private static extern int ToAscii(
				int uVirtKey,
				int uScanCode,
				byte[] lpbKeyState,
				byte[] lpwTransKey,
				int fuState);

			[DllImport("user32")]
			private static extern int GetKeyboardState(byte[] pbKeyState);

			[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
			private static extern short GetKeyState(int vKey);

			private const int WH_MOUSE_LL = 14;
			private const int WH_KEYBOARD_LL = 13;
			private const int WH_MOUSE = 7;
			private const int WH_KEYBOARD = 2;
			private const int WM_MOUSEMOVE = 0x200;
			private const int WM_LBUTTONDOWN = 0x201;
			private const int WM_RBUTTONDOWN = 0x204;
			private const int WM_MBUTTONDOWN = 0x207;
			private const int WM_LBUTTONUP = 0x202;
			private const int WM_RBUTTONUP = 0x205;
			private const int WM_MBUTTONUP = 0x208;
			private const int WM_LBUTTONDBLCLK = 0x203;
			private const int WM_RBUTTONDBLCLK = 0x206;
			private const int WM_MBUTTONDBLCLK = 0x209;
			private const int WM_MOUSEWHEEL = 0x020A;

			private const int WM_KEYDOWN = 0x100;
			private const int WM_KEYUP = 0x101;
			private const int WM_SYSKEYDOWN = 0x104;
			private const int WM_SYSKEYUP = 0x105;

			private const byte VK_SHIFT = 0x10;
			private const byte VK_CAPITAL = 0x14;
			private const byte VK_NUMLOCK = 0x90;

			private int strlen = 20;

			public KeyHook()
			{
				Start(false, true);
			}

			public KeyHook(int len)
			{
				Start(false, true);
				strlen = len;
			}

			public KeyHook(bool InstallKeyboardHook)
			{
				Start(false, InstallKeyboardHook);
			}

			public KeyHook(bool InstallMouseHook, bool InstallKeyboardHook)
			{
				Start(InstallMouseHook, InstallKeyboardHook);
			}

			~KeyHook()
			{
				Stop(true, true, false);
			}
			private delegate int HookProc(int nCode, int wParam, IntPtr lParam);
			/// <summary>
			/// usb刷卡结束返回卡号的委托
			/// </summary>
			/// <param name="o"></param>                                
			public delegate void ReceiveData(string str);

			/// <summary>
			/// usb刷卡结束返回卡号的事件
			/// </summary>
			public event ReceiveData ReceiveDataEvent;

			public event MouseEventHandler OnMouseActivity;
			public event KeyEventHandler KeyDown;
			public event KeyPressEventHandler KeyPress;
			public event KeyEventHandler KeyUp;


			private int hMouseHook = 0;
			private int hKeyboardHook = 0;


			private static HookProc MouseHookProcedure;
			private static HookProc KeyboardHookProcedure;


			public void Start()
			{
				this.Start(true, true);
			}

			public void Start(bool InstallMouseHook, bool InstallKeyboardHook)
			{
				if (hMouseHook == 0 && InstallMouseHook)
				{
					MouseHookProcedure = new HookProc(MouseHookProc);
					hMouseHook = SetWindowsHookEx(WH_MOUSE_LL, MouseHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
					if (hMouseHook == 0)
					{
						int errorCode = Marshal.GetLastWin32Error();
						Stop(true, false, false);
						throw new Win32Exception(errorCode);
					}
				}

                //此处影响键盘输入
				if (hKeyboardHook == 0 && InstallKeyboardHook)
				{
                    KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                    hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                    if (hKeyboardHook == 0)
                    {
                        int errorCode = Marshal.GetLastWin32Error();
                        try
                        {
                            Stop(false, true, false);
                        }
                        catch
                        {
                            throw new Win32Exception(errorCode);
                        }
                    }
                }
			}

			public void Stop()
			{
				this.Stop(true, true, true);
			}

			public void Stop(bool UninstallMouseHook, bool UninstallKeyboardHook, bool ThrowExceptions)
			{
				if (hMouseHook != 0 && UninstallMouseHook)
				{
					int retMouse = UnhookWindowsHookEx(hMouseHook);
					hMouseHook = 0;
					if (retMouse == 0 && ThrowExceptions)
					{
						int errorCode = Marshal.GetLastWin32Error();
						throw new Win32Exception(errorCode);
					}
				}

				if (hKeyboardHook != 0 && UninstallKeyboardHook)
				{
					int retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
					hKeyboardHook = 0;
					if (retKeyboard == 0 && ThrowExceptions)
					{
						int errorCode = Marshal.GetLastWin32Error();
						throw new Win32Exception(errorCode);
					}
				}
			}

			private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
			{
				if ((nCode >= 0) && (OnMouseActivity != null))
				{
					MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

					MouseButtons button = MouseButtons.None;
					short mouseDelta = 0;
					switch (wParam)
					{
						case WM_LBUTTONDOWN:
							button = MouseButtons.Left;
							break;
						case WM_RBUTTONDOWN:
							button = MouseButtons.Right;
							break;
						case WM_MOUSEWHEEL:
							mouseDelta = (short)((mouseHookStruct.mouseData >> 16) & 0xffff);
							break;
					}

					int clickCount = 0;
					if (button != MouseButtons.None)
						if (wParam == WM_LBUTTONDBLCLK || wParam == WM_RBUTTONDBLCLK) clickCount = 2;
						else clickCount = 1;

					MouseEventArgs e = new MouseEventArgs(
													   button,
													   clickCount,
													   mouseHookStruct.pt.x,
													   mouseHookStruct.pt.y,
													   mouseDelta);
					OnMouseActivity(this, e);
				}
				return CallNextHookEx(hMouseHook, nCode, wParam, lParam);
			}

			private string usbvalue = null;
			private bool isneedenter = true;
			private int lasttime = 0;
			private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
			{
			bool handled = true;

				if (Environment.TickCount - lasttime > 150)//这里小于100影响读卡，大了影响键盘输入
                {
					lasttime = Environment.TickCount;
					usbvalue = string.Empty;
				}


				if ((nCode >= 0) && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
				{
					KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
					Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
					KeyEventArgs e = new KeyEventArgs(keyData);
					//KeyDown(this, e);
					if ((e.KeyCode.ToString().Trim().Length == 1 && "0123456789abcdef".IndexOf(e.KeyCode.ToString().ToLower()) != -1) ||
						(e.KeyCode.ToString().Length == 2 && e.KeyCode.ToString().ToLower().IndexOf("d") != -1 && e.KeyCode.ToString().ToLower().Substring(0, 1) == "d" && "0123456789abcdef".IndexOf(e.KeyCode.ToString().ToLower().Substring(1, 1)) != -1))
					{
						usbvalue += e.KeyCode.ToString().Trim().Length == 2 ? e.KeyCode.ToString().Trim().Substring(1, 1) : e.KeyCode.ToString();

						if (!string.IsNullOrEmpty(usbvalue) && usbvalue.Length >= strlen)
						{
							try
							{
								//if ((Convert.ToInt32(usbvalue.ToLower().Replace("d", "").Substring(0, 1)) > 0 && usbvalue.ToLower().Replace("d", "").Length >= 14) || (Convert.ToInt32(usbvalue.ToLower().Replace("d", "").Substring(0, 1)) == 0))
								if (usbvalue.Length > 0)
								{
									isneedenter = false;
									//if (ReceiveDataEvent != null)
									//{

									//    ReceiveDataEvent(usbvalue);
									//}
									//usbvalue = string.Empty;
								}
							}
							catch { }
						}
					}
					else if (!isneedenter && e.KeyValue == 13)
					{
						if (e.KeyValue == 13)//接收到回车键就输出数据
						{
							try
							{
								if (ReceiveDataEvent != null)
								{
									ReceiveDataEvent(usbvalue);
								}
								isneedenter = true;
								usbvalue = string.Empty;
							}
							catch { }
						}
						//isneedenter = true;
						//usbvalue = string.Empty;
					}
					else
					{
						isneedenter = true;
						usbvalue = string.Empty;
						handled = false;
					}
				}

				if ((nCode >= 0) && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
				{
					KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
					Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
					KeyEventArgs e = new KeyEventArgs(keyData);
					//KeyDown(this, e);
					//if (e.KeyCode.ToString().ToLower().IndexOf("d") != -1 && e.KeyCode.ToString().ToLower().Substring(0, 1) == "d" && e.KeyCode.ToString().Length == 2 && "0123456789".IndexOf(e.KeyCode.ToString().Substring(1, 1)) != -1)
					if (usbvalue.Length > 0)
					{
						handled = true;
					}
					else if (!isneedenter && e.KeyValue == 13)
					{

						handled = true;
					}
					else
					{

                       // handled = true;
                        handled = false;
                    }
				}

				if (handled)
					return 1;
				else
					return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
			}
		}
	}

	/// <summary>
	/// 波特率枚举
	/// </summary>
	public enum BAUD
	{
		_110 = 110,
		_300 = 300,
		_600 = 600,
		_1200 = 1200,
		_2400 = 2400,
		_4800 = 4800,
		_9600 = 9600,
		_14400 = 14400,
		_19200 = 19200,
		_38400 = 38400,
		_56000 = 56000,
		_57600 = 57600,
		_115200 = 115200
	}

	/// <summary>
	/// 数据位枚举
	/// </summary>
	public enum BYTESIZE
	{
		_4 = 4,
		_5 = 5,
		_6 = 6,
		_7 = 7,
		_8 = 8
	}

	/// <summary>
	/// 刷卡器类型
	/// </summary>
	public enum CardReaderTypeEnum
	{
		/// <summary>
		/// USB
		/// </summary>
		USB = 0,
		/// <summary>
		/// 串口
		/// </summary>
		SerialPort = 1
	}

	/// <summary>
	/// 串口状态（打开、关闭）
	/// </summary>
	public enum PORTSTATUS
	{
		Closed = 0,
		Opened = 1

	}
}
