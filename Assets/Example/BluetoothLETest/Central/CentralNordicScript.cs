using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Text;

public class CentralNordicScript : MonoBehaviour
{
	public Transform PanelCentral;
	public Text Name;
	public Text Address;
	public Text Receive;
	public InputField Send;
	public Text TextConnectButton;
	public GameObject SendButton;
	public Canvas canvas;
	public Transform cubeTransform;
	public Transform laserTransform;

	private bool building_string = false;
	private string s_built = "";
	private bool _connecting = false;
	private string _connectedID = null;
	private string _serviceUUID = "0001";
	private string _readCharacteristicUUID = "0003";
	private string _writeCharacteristicUUID = "0002";
	private float _subscribingTimeout = 0f;
	private bool _readFound = false;
	private bool _writeFound = false;

	bool calib;

	bool _connected = false;
	bool Connected
	{
		get { return _connected; }
		set
		{
			_connected = value;
			
			if (_connected)
			{
				TextConnectButton.text = "Disconnect";
				_connecting = false;
			}
			else
			{
				TextConnectButton.text = "Connect";
				_connectedID = null;
				Receive.text = "";
				Send.text = "";
				SendButton.SetActive (false);
			}
		}
	}
	
	public void Initialize (CentralPeripheralButtonScript centralPeripheralButtonScript)
	{
		Connected = false;
		Name.text = centralPeripheralButtonScript.TextName.text;
		Address.text = centralPeripheralButtonScript.TextAddress.text;
		Receive.text = "";
		Send.text = "";
	}
	
	void disconnect (Action<string> action)
	{
		BluetoothLEHardwareInterface.DisconnectPeripheral (Address.text, action);
	}

	public void OnSend ()
	{
		if (Send.text.Length > 0)
		{
			byte[] bytes = ASCIIEncoding.UTF8.GetBytes (Send.text);
			if (bytes.Length > 0)
				SendBytes (bytes);

			//Send.text = "";
		}
	}

	public void OnBack ()
	{
		if (Connected)
		{
			disconnect ((Address) => {
				
				Connected = false;
				BLETestScript.Show (PanelCentral.transform);
			});
		}
		else
			BLETestScript.Show (PanelCentral.transform);
	}
	
	public void OnConnect ()
	{
		if (!_connecting)
		{
			if (Connected)
			{
				disconnect ((Address) => {
					Connected = false;
				});
			}
			else
			{
				_readFound = false;
				_writeFound = false;

				BluetoothLEHardwareInterface.ConnectToPeripheral (Address.text, (address) => {
				},
				(address, serviceUUID) => {
				},
				(address, serviceUUID, characteristicUUID) => {
					
					// discovered characteristic
					if (IsEqual(serviceUUID, _serviceUUID))
					{
						_connectedID = address;
						
						Connected = true;
						canvas.gameObject.GetComponent<CanvasGroup>().alpha = 0;

						if (IsEqual (characteristicUUID, _readCharacteristicUUID))
						{
							_readFound = true;
						}
						else if (IsEqual (characteristicUUID, _writeCharacteristicUUID))
						{
							_writeFound = true;
						}
					}
				}, (address) => {
					
					// this will get called when the device disconnects
					// be aware that this will also get called when the disconnect
					// is called above. both methods get call for the same action
					// this is for backwards compatibility
					Connected = false;
				});
				
				_connecting = true;
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (_readFound && _writeFound)
		{
			_readFound = false;
			_writeFound = false;

			_subscribingTimeout = 1f;
		}

		if (_subscribingTimeout > 0f)
		{
			_subscribingTimeout -= Time.deltaTime;
			if (_subscribingTimeout <= 0f)
			{
				_subscribingTimeout = 0f;

				BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress (_connectedID, FullUUID (_serviceUUID), FullUUID (_readCharacteristicUUID), (deviceAddress, notification) => {
					
				}, (deviceAddress2, characteristic, data) => {

					//BluetoothLEHardwareInterface.Log ("id: " + _connectedID);
					if (deviceAddress2.CompareTo (_connectedID) == 0)
					{
						//BluetoothLEHardwareInterface.Log (string.Format ("data length: {0}", data.Length));
						if (data.Length == 0)
						{
						}
						else
						{
							string s = ASCIIEncoding.UTF8.GetString (data);
							if (s.Contains(">")) {
								s_built = "";
								building_string = true;
								s = s.Substring(s.IndexOf(">") + 1);
							} else if (s.Contains("<")) {
								building_string = false;
								s = s.Substring(0,s.IndexOf("<"));
								s_built += s;
							}

							if (building_string) {
								s_built += s;
							}

							float[] sensor_data = Array.ConvertAll(s_built.Substring(1).Split(','), Single.Parse);

							//rotates the controlleer
							/*
							float cur_x = transform.rotation.eulerAngles.x; float cur_y = transform.rotation.eulerAngles.y; float cur_z = transform.rotation.eulerAngles.z;
							Vector3 new_angle = new Vector3(cur_x,cur_y,cur_z);
							float sensor_x = sensor_data[2]; float sensor_y = sensor_data[0]; float sensor_z = sensor_data[1];
							// float sensor_x = sensor_data[0]; float sensor_y = sensor_data[1]; float sensor_z = sensor_data[2];

							cubeTransform.eulerAngles = new Vector3(sensor_x, sensor_y, sensor_z);
							*/
							
							//Vector3 target = new Vector3(parsedValues[0],parsedValues[1],parsedValues[2]);
							//cubeController.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, target, Time.deltaTime);

							//translate the controller
							//cubeTransform.Translate(sensor_data[3], sensor_data[4], sensor_data[5]);

							float sensor_w = sensor_data[0]; float sensor_x = sensor_data[1]; float sensor_y = sensor_data[2]; float sensor_z = sensor_data[3];
							//cubeTransform.rotation = Quaternion.Inverse(new Quaternion(sensor_x, sensor_z, sensor_y, -sensor_w)); //sort of works
							//cubeTransform.rotation = new Quaternion(sensor_x, sensor_z, sensor_y, sensor_w);
							cubeTransform.rotation = new Quaternion(-sensor_y, -sensor_z, sensor_x, sensor_w);


							//moves the laser
							float l_input = sensor_data[8];//- 628f;
							// if (Mathf.Abs(l_input) < 10f) {
							// 	l_input = 0f;
							// }

							Vector3 temp = laserTransform.localScale;
							// Vector3 temp = transform.localScale;
							float laserSpeed = 5f;
							if (l_input > 800) {
								l_input = System.Convert.ToSingle(true);

							}
							else if (l_input < 20) {
								l_input = (-1)*System.Convert.ToSingle(true);
							}
							else {
								l_input = 0f;
							}


							temp.z += l_input * laserSpeed * Time.deltaTime;

							if (temp.z < 0.2f) {
								temp.z = 0.2f;
							}

							if (temp.z > 4) {
								temp.z = 4;
							}
							laserTransform.localScale = temp;

							if (sensor_data[10]==0f) {
								cubeTransform.position = new Vector3(0.0f,0.0f,0.0f);
								cubeTransform.rotation = Quaternion.identity;
							}
						}
					}
					
				});
			}
		}
	}
	
	string FullUUID (string uuid)
	{
		return "6E40" + uuid + "-B5A3-F393-E0A9-E50E24DCCA9E";
	}
	
	bool IsEqual(string uuid1, string uuid2)
	{
		if (uuid1.Length == 4)
			uuid1 = FullUUID (uuid1);
		if (uuid2.Length == 4)
			uuid2 = FullUUID (uuid2);

		return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
	}
	
	void SendByte (byte value)
	{
		byte[] data = new byte[] { value };
		BluetoothLEHardwareInterface.WriteCharacteristic (_connectedID, FullUUID (_serviceUUID), FullUUID (_writeCharacteristicUUID), data, data.Length, true, (characteristicUUID) => {
			
			BluetoothLEHardwareInterface.Log ("Write Succeeded");
		});
	}
	
	void SendBytes (byte[] data)
	{
		BluetoothLEHardwareInterface.Log (string.Format ("data length: {0} uuid: {1}", data.Length.ToString (), FullUUID (_writeCharacteristicUUID)));
		BluetoothLEHardwareInterface.WriteCharacteristic (_connectedID, FullUUID (_serviceUUID), FullUUID (_writeCharacteristicUUID), data, data.Length, true, (characteristicUUID) => {

			BluetoothLEHardwareInterface.Log ("Write Succeeded");
		});
	}
}
