using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RahulTest.Core.ViewModels
{
    [XmlRoot(ElementName = "Transactions")]
    public class XmlImportModel
    {
        [XmlElement(ElementName = "Transaction")]
        public List<TransactionModel> XmlTransactionModes { get; set; } = new List<TransactionModel>();
        public XmlImportModel()
        {
                
        }
    }

    public class TransactionModel
    {
        [XmlAttribute("id")]
        public string TransactionId{ get; set; }

        [XmlElement]
        public string TransactionDate { get; set; }

        public PaymentDetails PaymentDetails { get; set; }
        [XmlElement]
        public string Status { get; set; }
    }

    public class PaymentDetails
    {
        [XmlElement]
        public decimal Amount { get; set; }
        [XmlElement]
        public string CurrencyCode { get; set; }
    }
}
