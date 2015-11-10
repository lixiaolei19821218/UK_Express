using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;

/// <summary>
/// Bpost 的摘要说明
/// </summary>
public class Bpost
{	
    public static string GenerateLciFile(string contact, Order o)
    {
        string senderId, contactNumber, subContactNumber;
        ParseContact(contact, out senderId, out contactNumber, out subContactNumber);
        string sequenceId = o.Id.ToString().PadLeft(5, '0');

        string dir = AppDomain.CurrentDomain.BaseDirectory + "bpost_files/" + o.User + "/";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        string file = dir + senderId + "_" + sequenceId + ".txt";
        StreamWriter sw = new StreamWriter(file);
        string header = string.Format("*BPI_IN*            {0}*V 3.1 *{1}", senderId, sequenceId);
        sw.WriteLine(header);

        int lineCount = 0;
        foreach (Recipient r in o.Recipients)
        {
            foreach (Package p in r.Packages)
            {
                string barcode = GenerateBarcode(contactNumber, subContactNumber, p.Id.ToString());
                
                string a01 = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}",
                    "A01",
                    "01",
                    barcode,
                    senderId,
                    "139",
                    p.Recipient.Order.SenderName.PadLeft(40),//
                    string.Empty.PadLeft(40),//Sender – Department
                    p.Recipient.Order.SenderName.PadLeft(40),
                    string.Empty.PadLeft(40),//Sender – Place
                    (p.Recipient.Order.SenderAddress1 + p.Recipient.Order.SenderAddress2 + p.Recipient.Order.SenderAddress3).PadLeft(40),
                    ".".PadLeft(8),//Sender – House number
                    string.Empty.PadLeft(8),//Sender – Box number
                    p.Recipient.Order.SenderZipCode.PadLeft(8),//Sender – Zip code
                    p.Recipient.Order.SenderCity.PadLeft(40),
                    " GB",
                    p.Recipient.Order.SenderPhone.PadLeft(20),
                    string.Empty.PadLeft(50),//Sender – E-mail
                    string.Empty.PadLeft(20),//Sender – Mobile
                    p.Recipient.PyName.PadLeft(40),
                    string.Empty.PadLeft(40),//Addressee – Department
                    p.Recipient.PyName.PadLeft(40),
                    string.Empty.PadLeft(40),//Addressee – Place
                    p.Recipient.PyAddress.PadLeft(40),
                    string.Empty.PadLeft(8),//Addressee – House number
                    string.Empty.PadLeft(8),//Addressee – Box number
                    p.Recipient.ZipCode.PadLeft(8),
                    p.Recipient.PyCity.PadLeft(40),
                    " CN",
                    p.Recipient.PhoneNumber.PadLeft(20),
                    string.Empty.PadLeft(50),//Addressee – E-mail
                    p.Recipient.PhoneNumber.PadLeft(20),
                    ((int)(p.Weight * 1000)).ToString().PadLeft(7),
                    (3 + 7 * p.PackageItems.Count).ToString().PadLeft(3, '0')
                    );                

                string d01_0 = string.Format("D01461EA953400375BE");
                string d01_1 = string.Format("D01900GOODS");
                string d01_2 = string.Format("D01901RTS");

                sw.WriteLine(a01);
                sw.WriteLine(d01_0);
                sw.WriteLine(d01_1);
                sw.WriteLine(d01_2);
                lineCount += 4;

                for (int i = 0; i < p.PackageItems.Count; i++)
                {
                    string number = (i + 1).ToString().PadLeft(3, '0');
                    PackageItem item = p.PackageItems.ElementAt(i);
                    string d02_0 = string.Format("D02960{0}{1}", number, item.Count.Value.ToString().PadLeft(6, '0'));
                    string d02_1 = string.Format("D02961{0}{1}", number, item.Value.Value.ToString().PadLeft(12, '0')).Replace('.', ',');
                    string d02_2 = string.Format("D02962{0}GBP", number);
                    string d02_3 = string.Format("D02963{0}{1}", number, item.Description);
                    string d02_4 = string.Format("D02964{0}{1}", number, ((int)(item.NettoWeight * 1000)).ToString().PadLeft(4, '0'));
                    string d02_5 = string.Format("D02965{0}999999", number);
                    string d02_6 = string.Format("D02966{0}GB", number);             
                    
                    sw.WriteLine(d02_0);
                    sw.WriteLine(d02_1);
                    sw.WriteLine(d02_2);
                    sw.WriteLine(d02_3);
                    sw.WriteLine(d02_4);
                    sw.WriteLine(d02_5);
                    sw.WriteLine(d02_6);
                    lineCount += 7;
                }
            }
        }        
        
        
        string footer = string.Format("*END*{0}", lineCount.ToString().PadLeft(8, '0'));
        sw.Write(footer);
        sw.Close();

        FtpWeb ftp = new FtpWeb("ftp://transfert.post.be", "999_parcels", "dkfoec36");
        ftp.Upload(file);

        return file;
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

    public static void GenerateLabel(Package p, string barcode)
    {
        Document document = new Document();
        string fileName = HttpRuntime.AppDomainAppPath + "bpost_files/" + p.Recipient.Order.User + "/CollectionReceiptParcel" + p.Id.ToString().PadLeft(7, '0') + ".pdf";
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
        document.Open();

        PdfPCell cell;

        PdfPTable from = new PdfPTable(3);
        cell = new PdfPCell(new Phrase("From"));

        from.AddCell(cell);
        cell = new PdfPCell(new Phrase(string.Format("Name:999 PARCEL LTD\nBussiness:PO BOX 9320\nStreet:EMC BRU Cargo\nZip Code:1934\nCountry:BE\nPhonenr:")));
        cell.Colspan = 2;
        from.AddCell(cell);

        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HttpRuntime.AppDomainAppPath + "static/img/bpost_logo.png");
        PdfPTable image = new PdfPTable(1);
        image.AddCell(img);

        PdfPTable to = new PdfPTable(3);
        cell = new PdfPCell(new Phrase("To"));
        to.AddCell(cell);
        cell = new PdfPCell(new Phrase(string.Format("Name:\t{0}\nStreet:\t{1}\n{2}\n{3}", p.Recipient.PyName, p.Recipient.PyAddress, "CHINA", p.Recipient.PyCity)));
        cell.Colspan = 2;
        to.AddCell(cell);

        PdfPTable details = new PdfPTable(1);
        cell = new PdfPCell(new Phrase("Service Level PRIO"));
        details.AddCell(cell);
        cell = new PdfPCell(new Phrase("Importer's reference"));
        details.AddCell(cell);
        cell = new PdfPCell(new Phrase("Importer's contact"));
        details.AddCell(cell);
        cell = new PdfPCell(new Phrase("Seneder's instruction in case of non-delivery"));
        details.AddCell(cell);
        cell = new PdfPCell(new Phrase("RETURN TO SENDER"));
        details.AddCell(cell);
        cell = new PdfPCell(new Phrase("Creation Date"));
        details.AddCell(cell);

        BarcodeWriter barcodeWriter = new BarcodeWriter//312010605000100000000000000447
        {
            Format = BarcodeFormat.CODE_128,
            Options = new EncodingOptions
            {
                Height = 100,
                Width = 600
            }
        };

        Bitmap bitmap = barcodeWriter.Write(barcode);
        string pngFile = HttpRuntime.AppDomainAppPath + "bpost_files/" + p.Recipient.Order.User + "/Parcel" + p.Id.ToString().PadLeft(7, '0') + ".png";
        bitmap.Save(pngFile, System.Drawing.Imaging.ImageFormat.Png);
        iTextSharp.text.Image barceode = iTextSharp.text.Image.GetInstance(pngFile);
        barceode.SetAbsolutePosition(0, 350);

        document.Add(from);
        document.Add(image);
        document.Add(to);
        document.Add(details);
        document.Add(barceode);

        document.Close();
    }
}