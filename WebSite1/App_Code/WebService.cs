using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/", Description = "Servicio web para validar transacciones.")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod(Description = "Retorna Hola Munda :D.")]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod(Description = "Retorna codigo de resultado de transacción.")]
    public string ValidateTransaction(Transaction transaction)
    {
        try
        {
            CreditEntities creditEntities = new CreditEntities();
            DateTime expirationDate = new DateTime(transaction.ExpirationYear, transaction.ExpirationMonth, 1);

            Card card = (from c in creditEntities.Card where c.PAN == transaction.PAN select c).FirstOrDefault();
            

            if (card == null) // Rechazada, tarjeta no existe
            {
                return "10";
            }

            DateTime cardExpirationDate = new DateTime(card.ExpirationYear, card.ExpirationMonth, 1);

            if (card.Quota < transaction.TransactionAmount) // Rechazada, falta de fondos
            {
                return "11";
            }

            if (!card.Active) // Rechazada, tarjeta bloqueada
            {
                return "12";
            }

            if (DateTime.Now > cardExpirationDate) // Rechazada, tarjeta vencida
            {
                return "13";
            }

            Client client = (from c in creditEntities.Client where c.Id == card.IdClient select c).FirstOrDefault();

            if (client.Password != transaction.Password) // Rechazada, contraseña incorrecta
            {
                return "14";
            }

            if (expirationDate != cardExpirationDate ||
                transaction.SecurityCode != card.SecurityCode ||
                transaction.CardBrand != card.Brand ||
                !client.Active) // Rechazada, datos erróneos
            {
                return "15";
            }

            if ((cardExpirationDate - DateTime.Now).TotalDays <= 30) // Aprobada pronta a vencer
            {
                return "01";
            }

            card.Quota = card.Quota - transaction.TransactionAmount;
            creditEntities.SaveChanges();

            return "00"; // Aprobada sin incidentes
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "16"; //error hermoso y desconocido.
        }
    }

}
