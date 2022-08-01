namespace API_WEB.Models
{
    public class Pedido
    {
        public int idpedido { get; set; }
        public string pagamento { get; set; }
        public decimal total { get; set; }
        public string datapedido { get; set; }
        public int idusuario { get; set; }
    }
}
