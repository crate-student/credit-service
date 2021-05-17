using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Transaction
/// </summary>
public class Transaction
{
    public Transaction()
    {
        
    }

    public string PAN { get; set; }
    public int ExpirationMonth { get; set; }
    public int ExpirationYear { get; set; }
    public string SecurityCode { get; set; }
    public string CardBrand { get; set; }
    public string Password { get; set; }
    public int TransactionAmount { get; set; }
}