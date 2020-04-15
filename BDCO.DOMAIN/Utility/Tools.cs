using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using QRCoder;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace BDCO.Domain.Utility
{
    public static class Tools
    {
        public static void CopyClass(this Object Destination, object Source)
        {
            var srcT = Source.GetType();
            var dstT = Destination.GetType();
            foreach (var f in srcT.GetFields())
            {
                var dstF = dstT.GetField(f.Name);
                if (dstF == null)
                    continue;
                dstF.SetValue(Destination, f.GetValue(Source));
            }

            foreach (var f in srcT.GetProperties())
            {
                var dstF = dstT.GetProperty(f.Name);
                if (dstF == null)
                    continue;

                
                if (!(f.Name.Contains("PacketList")) && !(f.Name.Contains("Packet")))
                    dstF.SetValue(Destination, f.GetValue(Source, null), null);
            }
        }

        public static void CopyJsonData(this Object Destination, string JsonString)
        {
            var requestSourceDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonString);
            foreach (var items in requestSourceDictionary)
            {
                var destinationType = Destination.GetType();
                var destinationField = destinationType.GetProperty(items.Key);
                if (destinationField != null)
                {
                    Type fieldType = Nullable.GetUnderlyingType(destinationField.PropertyType) ?? destinationField.PropertyType;
                    var fieldValue = (items.Value == null || items.Value == "") ? null : Convert.ChangeType(items.Value, fieldType);
                    destinationField.SetValue(Destination, fieldValue, null);
                }
            }
        }

        public static string EmailSignature()
        {
            string signature = "";
            signature += "<br /><span style=\"font-family:Arial;font-size:12px;\"><b>" + "ONDESK SERVICE" +
                "</b>|<span style=\"color: red;\"> Save the Children in Bangladesh</span> | " +
                "<br />House CWN (A) 35, Road 43, Gulshan 2, Dhaka 1212, Bangladesh <br />";
            signature += " https://bangladesh.savethechildren.net/<br />";
            signature += @"Tel: +88-02-882 8081, Ext. 1065 | Fax: +88-02-881 2523 <br /></span>";
            return signature;
        }
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public static Bitmap GenerateBitMapQR(string st)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(st, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }

        public static string GenerateBase64QR(string st)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(st, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20);
            return qrCodeImageAsBase64;
        }
        /// <summary>
        /// String is base64 type checking
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static bool IsBase64(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;
            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                return false;
                // Handle the exception
            }
            return false;
        }
    }
}
