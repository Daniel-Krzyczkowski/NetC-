using CakesFactory.Core.Model;

namespace CakesFactory.Core.Services
{
    public class CakeBoxOrderResult
    {
        public CakeBoxOrderCreationResultCode ResultCode { get; set; }

        public CakeBoxOrder CreatedCakeBoxOrder { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum CakeBoxOrderCreationResultCode
    {
        Success,
        NoSuccess
    }
}
