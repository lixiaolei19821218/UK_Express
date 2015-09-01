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
    public static void GenerateLciFile(string contact, string parcelNumber, Package p)
    {
        string senderId, contactNumber, subContactNumber;
        ParseContact(contact, out senderId, out contactNumber, out subContactNumber);
        string sequenceId = GenerateSequenceId();
        string file = AppDomain.CurrentDomain.BaseDirectory + senderId + "_" + GenerateSequenceId() + ".txt";
        StreamWriter sw = new StreamWriter(file);
        string header = string.Format("*BPI_IN*            {0}*V 3.1 *{1}", senderId, sequenceId);

        string barcode = GenerateBarcode(contactNumber, subContactNumber, parcelNumber);
        string a01 = string.Format("{0}{1}{2}{3}{4}{5}{6}",            
            "A01",
            "01",
            barcode,
            contactNumber,
            "139",
            p.Recipient.Order.SenderName.PadRight(40),
            string.Empty.PadRight(40),//Sender – Department
            p.Recipient.Order.SenderName.PadRight(40),
            string.Empty.PadRight(40),//Sender – Place
            (p.Recipient.Order.SenderAddress1 + p.Recipient.Order.SenderAddress2 + p.Recipient.Order.SenderAddress3).PadRight(40),
            p.Recipient.Order.SenderName.PadRight(8),//Sender – House number
            string.Empty.PadRight(8),//Sender – Box number
            p.Recipient.Order.SenderZipCode.PadRight(8),
            p.Recipient.Order.SenderCity.PadRight(40),
            "UK ",
            p.Recipient.Order.SenderPhone.PadRight(20),
            string.Empty.PadRight(50),//Sender – E-mail
            string.Empty.PadRight(20),//Sender – Mobile
            p.Recipient.Name.PadRight(40),
            string.Empty.PadRight(40),//Addressee – Department
            p.Recipient.Name.PadRight(40),
            string.Empty.PadRight(40),//Addressee – Place
            p.Recipient.Address.PadRight(40),
            p.Recipient.Address.PadRight(8),
            string.Empty.PadRight(8),//Addressee – Box number
            p.Recipient.ZipCode.PadRight(8),
            p.Recipient.City.PadRight(40),
            "CN ",
            p.Recipient.PhoneNumber.PadRight(20),
            string.Empty.PadRight(50),//Addressee – E-mail
            p.Recipient.PhoneNumber.PadRight(20),
            p.Weight.ToString().PadRight(7),
            "  3"
            );
        string d01_0 = string.Format("D01461EA953400375BE");
        string d01_1 = string.Format("D01900GOODS");
        string d01_2 = string.Format("D01901RTS");
        string d02_0 = string.Format("D02960001000001");
        string d02_1 = string.Format("D02961001{0}", p.Value.ToString().PadLeft(12, '0'));
        string d02_2 = string.Format("D02962001GBP");
        string d02_3 = string.Format("D02963001{0}", p.Detail);
        string d02_4 = string.Format("D029640014840");
        string d02_5 = string.Format("D02965001999999");
        string d02_6 = string.Format("D02966001UK");
        sw.WriteLine(header);
        sw.WriteLine(a01);
        sw.WriteLine(d01_0);
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