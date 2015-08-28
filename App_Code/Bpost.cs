using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Bpost 的摘要说明
/// </summary>
public class Bpost
{	
    public static void GenerateLciFile(string contact, string parcelNumber, Order order)
    {
        string senderId, contactNumber, subContactNumber;
        ParseContact(contact, out senderId, out contactNumber, out subContactNumber);
        string sequenceId = GenerateSequenceId();
        string file = AppDomain.CurrentDomain.BaseDirectory + senderId + "_" + GenerateSequenceId() + ".txt";
        StreamWriter sw = new StreamWriter(file);
        string header = string.Format("*BPI_IN*            {0}*V 3.1 *{1}", senderId, sequenceId);

        string barcode = GenerateBarcode(contactNumber, subContactNumber, parcelNumber);
        string a01 = string.Format("{0}{1}{2}{3}{4}{5}              FR                                                                                           SOKOLOVA EKATERINA                                                                                                                                              MITINSKAYA 43 92                        MITINSKA        125368  MOSCOW                                  RU                     e.sokolova@bebeo.com                                                  0004840010",
            string.Empty,
            "A01",
            barcode,
            contactNumber,
            order.SenderName.PadRight(40),
            string.Empty.PadRight(40),
            string.Empty.PadRight(40)
            );
        sw.WriteLine(header);
        sw.Close();
    }

    public static void ParseContact(string contact, out string senderId, out string contactNumber, out string subContactNumber)
    {
        //BPI/2009/1234/567
        string[] temps = contact.Split('/');
        if (temps.Length == 3)
        {
            subContactNumber = "000";
        }
        else if (temps.Length == 4)
        {
            subContactNumber = temps[3];
        }
        else
        {
            throw new Exception("Invalid contact.");
        }
        contactNumber = temps[1] + temps[2];
        senderId = "0" + temps[2] + subContactNumber;
    }

    public static string GenerateSequenceId()
    {
        return "00001";
    }

    public static string GenerateBarcode(string contactNumber, string subContactNumber, string parcelNumber)
    {
        string temp = "41" + contactNumber + subContactNumber + parcelNumber.PadLeft(15, '0');
        decimal d = decimal.Parse(temp);
        decimal xx = d % 97;
        if (xx == 0m)
        {
            xx = 97;
        }
        return (temp + xx.ToString()).PadRight(40);
    }
}