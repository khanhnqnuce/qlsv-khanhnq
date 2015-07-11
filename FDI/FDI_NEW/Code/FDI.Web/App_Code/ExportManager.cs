using FDI.Simple;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FDI.Common
{
    public class ExportManager
    {
        //public virtual void ExportOrdersToXlsx(string filePath, IList<Order> orders)
        //{
        //    var newFile = new FileInfo(filePath);
        //    // ok, we can run the real code of the sample now
        //    using (var xlPackage = new ExcelPackage(newFile))
        //    {
        //        // uncomment this line if you want the XML written out to the outputDir
        //        //xlPackage.DebugMode = true; 

        //        // get handle to the existing worksheet
        //        var worksheet = xlPackage.Workbook.Worksheets.Add("Orders");
        //        //Create Headers and format them
        //        var properties = new string[]
        //            {
        //                //order properties
        //                "OrderId",
        //                "OrderGuid",
        //                "CustomerId",
        //                "OrderSubtotalInclTax",
        //                "OrderSubtotalExclTax",
        //                "OrderSubTotalDiscountInclTax",
        //                "OrderSubTotalDiscountExclTax",
        //                "OrderShippingInclTax",
        //                "OrderShippingExclTax",
        //                "PaymentMethodAdditionalFeeInclTax",
        //                "PaymentMethodAdditionalFeeExclTax",
        //                "TaxRates",
        //                "OrderTax",
        //                "OrderTotal",
        //                "RefundedAmount",
        //                "OrderDiscount",
        //                "CurrencyRate",
        //                "CustomerCurrencyCode",
        //                "AffiliateId",
        //                "OrderStatusId",
        //                "PaymentMethodSystemName",
        //                "PurchaseOrderNumber",
        //                "PaymentStatusId",
        //                "ShippingStatusId",
        //                "ShippingMethod",
        //                "ShippingRateComputationMethodSystemName",
        //                "VatNumber",
        //                "CreatedOnUtc",
        //                //billing address
        //                "BillingFirstName",
        //                "BillingLastName",
        //                "BillingEmail",
        //                "BillingCompany",
        //                "BillingCountry",
        //                "BillingStateProvince",
        //                "BillingCity",
        //                "BillingAddress1",
        //                "BillingAddress2",
        //                "BillingZipPostalCode",
        //                "BillingPhoneNumber",
        //                "BillingFaxNumber",
        //                //shipping address
        //                "ShippingFirstName",
        //                "ShippingLastName",
        //                "ShippingEmail",
        //                "ShippingCompany",
        //                "ShippingCountry",
        //                "ShippingStateProvince",
        //                "ShippingCity",
        //                "ShippingAddress1",
        //                "ShippingAddress2",
        //                "ShippingZipPostalCode",
        //                "ShippingPhoneNumber",
        //                "ShippingFaxNumber",
        //            };
        //        for (int i = 0; i < properties.Length; i++)
        //        {
        //            worksheet.Cells[1, i + 1].Value = properties[i];
        //            worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
        //            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
        //        }


        //        int row = 2;
        //        foreach (var order in orders)
        //        {
        //            int col = 1;

        //            //order properties
        //            worksheet.Cells[row, col].Value = order.Id;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderGuid;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.CustomerId;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderSubtotalInclTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderSubtotalExclTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderSubTotalDiscountInclTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderSubTotalDiscountExclTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderShippingInclTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderShippingExclTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.PaymentMethodAdditionalFeeInclTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.PaymentMethodAdditionalFeeExclTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.TaxRates;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderTax;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderTotal;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.RefundedAmount;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderDiscount;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.CurrencyRate;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.CustomerCurrencyCode;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.AffiliateId.HasValue ? order.AffiliateId.Value : 0;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.OrderStatusId;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.PaymentMethodSystemName;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.PurchaseOrderNumber;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.PaymentStatusId;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingStatusId;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingMethod;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingRateComputationMethodSystemName;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.VatNumber;
        //            col++;

        //            worksheet.Cells[row, col].Value = order.CreatedOnUtc.ToOADate();
        //            col++;


        //            //billing address
        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.FirstName : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.LastName : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.Email : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.Company : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null && order.BillingAddress.Country != null ? order.BillingAddress.Country.Name : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null && order.BillingAddress.StateProvince != null ? order.BillingAddress.StateProvince.Name : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.City : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.Address1 : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.Address2 : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.ZipPostalCode : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.PhoneNumber : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.BillingAddress != null ? order.BillingAddress.FaxNumber : "";
        //            col++;

        //            //shipping address
        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.FirstName : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.LastName : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.Email : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.Company : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null && order.ShippingAddress.Country != null ? order.ShippingAddress.Country.Name : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null && order.ShippingAddress.StateProvince != null ? order.ShippingAddress.StateProvince.Name : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.City : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.Address1 : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.Address2 : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.ZipPostalCode : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.PhoneNumber : "";
        //            col++;

        //            worksheet.Cells[row, col].Value = order.ShippingAddress != null ? order.ShippingAddress.FaxNumber : "";
        //            col++;

        //            //next row
        //            row++;
        //        }








        //        // we had better add some document properties to the spreadsheet 

        //        // set some core property values
        //        xlPackage.Workbook.Properties.Title = string.Format("{0} orders", _storeInformationSettings.StoreName);
        //        xlPackage.Workbook.Properties.Author = _storeInformationSettings.StoreName;
        //        xlPackage.Workbook.Properties.Subject = string.Format("{0} orders", _storeInformationSettings.StoreName);
        //        xlPackage.Workbook.Properties.Keywords = string.Format("{0} orders", _storeInformationSettings.StoreName);
        //        xlPackage.Workbook.Properties.Category = "Orders";
        //        xlPackage.Workbook.Properties.Comments = string.Format("{0} orders", _storeInformationSettings.StoreName);

        //        // set some extended property values
        //        xlPackage.Workbook.Properties.Company = _storeInformationSettings.StoreName;
        //        xlPackage.Workbook.Properties.HyperlinkBase = new Uri(_storeInformationSettings.StoreUrl);

        //        // save the new spreadsheet
        //        xlPackage.Save();
        //    }
        //}

            //public virtual string ExportOrdersToXml(IList<OrderItem> orders)
            //{
            //    var sb = new StringBuilder();
            //    var stringWriter = new StringWriter(sb);
            //    var xmlWriter = new XmlTextWriter(stringWriter);
            //    xmlWriter.WriteStartDocument();
            //    xmlWriter.WriteStartElement("Orders");
            //    //xmlWriter.WriteAttributeString("Version", NopVersion.CurrentVersion);


            //    foreach (var order in orders)
            //    {
            //        xmlWriter.WriteStartElement("Order");

            //        xmlWriter.WriteElementString("OrderId", null, order.Id.ToString());
            //        xmlWriter.WriteElementString("OrderGuid", null, order.OrderGuid.ToString());
            //        xmlWriter.WriteElementString("CustomerId", null, order.CustomerId.ToString());
            //        xmlWriter.WriteElementString("CustomerLanguageId", null, order.CustomerLanguageId.ToString());
            //        xmlWriter.WriteElementString("CustomerTaxDisplayTypeId", null, order.CustomerTaxDisplayTypeId.ToString());
            //        xmlWriter.WriteElementString("CustomerIp", null, order.CustomerIp);
            //        xmlWriter.WriteElementString("OrderSubtotalInclTax", null, order.OrderSubtotalInclTax.ToString());
            //        xmlWriter.WriteElementString("OrderSubtotalExclTax", null, order.OrderSubtotalExclTax.ToString());
            //        xmlWriter.WriteElementString("OrderSubTotalDiscountInclTax", null, order.OrderSubTotalDiscountInclTax.ToString());
            //        xmlWriter.WriteElementString("OrderSubTotalDiscountExclTax", null, order.OrderSubTotalDiscountExclTax.ToString());
            //        xmlWriter.WriteElementString("OrderShippingInclTax", null, order.OrderShippingInclTax.ToString());
            //        xmlWriter.WriteElementString("OrderShippingExclTax", null, order.OrderShippingExclTax.ToString());
            //        xmlWriter.WriteElementString("PaymentMethodAdditionalFeeInclTax", null, order.PaymentMethodAdditionalFeeInclTax.ToString());
            //        xmlWriter.WriteElementString("PaymentMethodAdditionalFeeExclTax", null, order.PaymentMethodAdditionalFeeExclTax.ToString());
            //        xmlWriter.WriteElementString("TaxRates", null, order.TaxRates);
            //        xmlWriter.WriteElementString("OrderTax", null, order.OrderTax.ToString());
            //        xmlWriter.WriteElementString("OrderTotal", null, order.OrderTotal.ToString());
            //        xmlWriter.WriteElementString("RefundedAmount", null, order.RefundedAmount.ToString());
            //        xmlWriter.WriteElementString("OrderDiscount", null, order.OrderDiscount.ToString());
            //        xmlWriter.WriteElementString("CurrencyRate", null, order.CurrencyRate.ToString());
            //        xmlWriter.WriteElementString("CustomerCurrencyCode", null, order.CustomerCurrencyCode);
            //        xmlWriter.WriteElementString("AffiliateId", null, order.AffiliateId.ToString());
            //        xmlWriter.WriteElementString("OrderStatusId", null, order.OrderStatusId.ToString());
            //        xmlWriter.WriteElementString("AllowStoringCreditCardNumber", null, order.AllowStoringCreditCardNumber.ToString());
            //        xmlWriter.WriteElementString("CardType", null, order.CardType);
            //        xmlWriter.WriteElementString("CardName", null, order.CardName);
            //        xmlWriter.WriteElementString("CardNumber", null, order.CardNumber);
            //        xmlWriter.WriteElementString("MaskedCreditCardNumber", null, order.MaskedCreditCardNumber);
            //        xmlWriter.WriteElementString("CardCvv2", null, order.CardCvv2);
            //        xmlWriter.WriteElementString("CardExpirationMonth", null, order.CardExpirationMonth);
            //        xmlWriter.WriteElementString("CardExpirationYear", null, order.CardExpirationYear);
            //        xmlWriter.WriteElementString("PaymentMethodSystemName", null, order.PaymentMethodSystemName);
            //        xmlWriter.WriteElementString("AuthorizationTransactionId", null, order.AuthorizationTransactionId);
            //        xmlWriter.WriteElementString("AuthorizationTransactionCode", null, order.AuthorizationTransactionCode);
            //        xmlWriter.WriteElementString("AuthorizationTransactionResult", null, order.AuthorizationTransactionResult);
            //        xmlWriter.WriteElementString("CaptureTransactionId", null, order.CaptureTransactionId);
            //        xmlWriter.WriteElementString("CaptureTransactionResult", null, order.CaptureTransactionResult);
            //        xmlWriter.WriteElementString("SubscriptionTransactionId", null, order.SubscriptionTransactionId);
            //        xmlWriter.WriteElementString("PurchaseOrderNumber", null, order.PurchaseOrderNumber);
            //        xmlWriter.WriteElementString("PaymentStatusId", null, order.PaymentStatusId.ToString());
            //        xmlWriter.WriteElementString("PaidDateUtc", null, (order.PaidDateUtc == null) ? string.Empty : order.PaidDateUtc.Value.ToString());
            //        xmlWriter.WriteElementString("ShippingStatusId", null, order.ShippingStatusId.ToString());
            //        xmlWriter.WriteElementString("ShippingMethod", null, order.ShippingMethod);
            //        xmlWriter.WriteElementString("ShippingRateComputationMethodSystemName", null, order.ShippingRateComputationMethodSystemName);
            //        xmlWriter.WriteElementString("VatNumber", null, order.VatNumber);
            //        xmlWriter.WriteElementString("Deleted", null, order.Deleted.ToString());
            //        xmlWriter.WriteElementString("CreatedOnUtc", null, order.CreatedOnUtc.ToString());

            //        //products
            //        var orderProductVariants = order.OrderProductVariants;
            //        if (orderProductVariants.Count > 0)
            //        {
            //            xmlWriter.WriteStartElement("OrderProductVariants");
            //            foreach (var orderProductVariant in orderProductVariants)
            //            {
            //                xmlWriter.WriteStartElement("OrderProductVariant");
            //                xmlWriter.WriteElementString("OrderProductVariantId", null, orderProductVariant.Id.ToString());
            //                xmlWriter.WriteElementString("ProductVariantId", null, orderProductVariant.ProductVariantId.ToString());

            //                var productVariant = orderProductVariant.ProductVariant;
            //                if (productVariant != null)
            //                    xmlWriter.WriteElementString("ProductVariantName", null, productVariant.FullProductName);


            //                xmlWriter.WriteElementString("UnitPriceInclTax", null, orderProductVariant.UnitPriceInclTax.ToString());
            //                xmlWriter.WriteElementString("UnitPriceExclTax", null, orderProductVariant.UnitPriceExclTax.ToString());
            //                xmlWriter.WriteElementString("PriceInclTax", null, orderProductVariant.PriceInclTax.ToString());
            //                xmlWriter.WriteElementString("PriceExclTax", null, orderProductVariant.PriceExclTax.ToString());
            //                xmlWriter.WriteElementString("AttributeDescription", null, orderProductVariant.AttributeDescription);
            //                xmlWriter.WriteElementString("AttributesXml", null, orderProductVariant.AttributesXml);
            //                xmlWriter.WriteElementString("Quantity", null, orderProductVariant.Quantity.ToString());
            //                xmlWriter.WriteElementString("DiscountAmountInclTax", null, orderProductVariant.DiscountAmountInclTax.ToString());
            //                xmlWriter.WriteElementString("DiscountAmountExclTax", null, orderProductVariant.DiscountAmountExclTax.ToString());
            //                xmlWriter.WriteElementString("DownloadCount", null, orderProductVariant.DownloadCount.ToString());
            //                xmlWriter.WriteElementString("IsDownloadActivated", null, orderProductVariant.IsDownloadActivated.ToString());
            //                xmlWriter.WriteElementString("LicenseDownloadId", null, orderProductVariant.LicenseDownloadId.ToString());
            //                xmlWriter.WriteEndElement();
            //            }
            //            xmlWriter.WriteEndElement();
            //        }

            //        //shipments
            //        var shipments = order.Shipments.OrderBy(x => x.CreatedOnUtc).ToList();
            //        if (shipments.Count > 0)
            //        {
            //            xmlWriter.WriteStartElement("Shipments");
            //            foreach (var shipment in shipments)
            //            {
            //                xmlWriter.WriteStartElement("Shipment");
            //                xmlWriter.WriteElementString("ShipmentId", null, shipment.Id.ToString());
            //                xmlWriter.WriteElementString("TrackingNumber", null, shipment.TrackingNumber);
            //                xmlWriter.WriteElementString("TotalWeight", null, shipment.TotalWeight.HasValue ? shipment.TotalWeight.Value.ToString() : "");

            //                xmlWriter.WriteElementString("ShippedDateUtc", null, shipment.ShippedDateUtc.HasValue ?
            //                    shipment.ShippedDateUtc.ToString() : "");
            //                xmlWriter.WriteElementString("DeliveryDateUtc", null, shipment.DeliveryDateUtc.HasValue ?
            //                    shipment.DeliveryDateUtc.Value.ToString() : "");
            //                xmlWriter.WriteElementString("CreatedOnUtc", null, shipment.CreatedOnUtc.ToString());
            //                xmlWriter.WriteEndElement();
            //            }
            //            xmlWriter.WriteEndElement();
            //        }
            //        xmlWriter.WriteEndElement();
            //    }

            //    xmlWriter.WriteEndElement();
            //    xmlWriter.WriteEndDocument();
            //    xmlWriter.Close();
            //    return stringWriter.ToString();
            //}
       
    }
}
