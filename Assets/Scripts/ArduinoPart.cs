using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;


public class ArduinoPart : MonoBehaviour
{
    // Start is called before the first frame updateSerialPort sp
    float next_time; int ii = 0;
    // Use this for initialization
    void Start()
    {
        SerialPort sp;
        string the_com = "";
        next_time = Time.time;

        foreach (string mysps in SerialPort.GetPortNames())
        {
            print(mysps);
            if (mysps != "COM1") { the_com = mysps; break; }
        }
        sp = new SerialPort("\\\\.\\" + the_com, 9600);
        if (!sp.IsOpen)
        {
            print("Opening " + the_com + ", baud 9600");
            sp.Open();
            sp.ReadTimeout = 100;
            sp.Handshake = Handshake.None;
            if (sp.IsOpen) { print("Open"); }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > next_time)
        {
            if (!sp.IsOpen)
            {
                sp.Open();
                print("opened sp");
            }
            if (sp.IsOpen)
            {
                print("Writing " + ii);
                sp.Write((ii.ToString()));
            }
            next_time = Time.time + 5;
            if (++ii > 9) ii = 0;
        }
    }

}
